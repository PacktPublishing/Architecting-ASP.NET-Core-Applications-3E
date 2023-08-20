using Refit;
using REPR.BFF;
using System.Collections.Concurrent;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRefitClient<IC18WebBasketsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7252"))
;
builder.Services
    .AddRefitClient<IC18WebProductsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7252"))
;
builder.Services.AddTransient<IC18WebClient, DefaultWebClient>();
builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();

var app = builder.Build();

app.MapGet(
    "api/catalog",
    (IC18WebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductsAsync(cancellationToken)
);
app.MapGet(
    "api/cart",
    async (IC18WebClient client, ICurrentUserService currentUserService, CancellationToken cancellationToken) =>
    {
        var basket = await client.Baskets.FetchCustomerBasketAsync(
            currentUserService.Id,
            cancellationToken
        );
        var result = new ConcurrentBag<BasketProduct>();
        await Parallel.ForEachAsync(basket, cancellationToken, async (item, cancellationToken) =>
        {
            var product = await client.Catalog.FetchProductAsync(item.ProductId, cancellationToken);
            result.Add(new BasketProduct(product.Id, product.Name, product.UnitPrice, item.Quantity));
        });
        return result;
    }
);
app.MapPost(
    "api/cart",
    async (UpdateCartItem item, IC18WebClient client, ICurrentUserService currentUserService, CancellationToken cancellationToken) =>
    {
        // Remove the item from the cart when the quantity is 0
        if (item.Quantity == 0)
        {
            try
            {
                var result = await client.Baskets.RemoveProductFromCart(
                    new Web.Features.Baskets.RemoveItem.Command(
                        currentUserService.Id,
                        item.ProductId
                    ),
                    cancellationToken
                );
                return Results.Ok();
            }
            catch (ValidationApiException ex)
            {
                // If the product is not in the cart, it does not matter.
                // If its another exception, we let it propagate up the stack.
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
            }
        }
        try
        {
            // Add the product to the cart
            var result = await client.Baskets.AddProductToCart(
                new Web.Features.Baskets.AddItem.Command(
                    currentUserService.Id,
                    item.ProductId,
                    item.Quantity
                ),
                cancellationToken
            );
        }
        catch (ValidationApiException ex)
        {
            // If the error is not because the product is already in the cart,
            // we let the exception propagate up the stack.
            if (ex.StatusCode != HttpStatusCode.Conflict)
            {
                throw;
            }

            // Update the cart
            var result = await client.Baskets.UpdateProductQuantity(
                new Web.Features.Baskets.UpdateQuantity.Command(
                    currentUserService.Id,
                    item.ProductId,
                    item.Quantity
                ),
                cancellationToken
            );
        }

        // If we reached this point, we return 200 OK
        return Results.Ok();
    }
);


app.Run();

public record class UpdateCartItem(int ProductId, int Quantity);

public record class BasketProduct(int Id, string Name, decimal UnitPrice, int Quantity);
