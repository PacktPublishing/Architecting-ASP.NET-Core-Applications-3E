using ApiClient;
using Refit;
using REPR.Baskets.Contracts;
using REPR.BFF;
using System.Collections.Concurrent;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.AddApiClient();
builder.Services.AddScoped<ICurrentCustomerService, FakeCurrentCustomerService>();

var app = builder.Build();

app.MapGet(
    "api/catalog",
    (IWebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductsAsync(cancellationToken)
);
app.MapGet(
    "api/catalog/{productId}",
    (int productId, IWebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductAsync(new(productId), cancellationToken)
);

app.MapGet(
    "api/cart",
    async (IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        var logger = app.Services
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GetCart")
        ;
        var basket = await client.Baskets.FetchCustomerBasketAsync(
            new(currentCustomer.Id),
            cancellationToken
        );
        var result = new ConcurrentBag<BasketProduct>();
        await Parallel.ForEachAsync(basket, cancellationToken, async (item, cancellationToken) =>
        {
            logger.LogTrace("Fetching product '{ProductId}'.", item.ProductId);
            var product = await client.Catalog.FetchProductAsync(
                new(item.ProductId),
                cancellationToken
            );
            logger.LogTrace("Found product '{ProductId}' ({ProductName}).", item.ProductId, product.Name);
            result.Add(new BasketProduct(
                product.Id,
                product.Name,
                product.UnitPrice,
                item.Quantity
            ));
        });
        return result;
    }
);
app.MapPost(
    "api/cart",
    async (UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        if (item.Quantity <= 0)
        {
            await RemoveItemFromCart(
                item,
                client,
                currentCustomer,
                cancellationToken
            );
        }
        else
        {
            await AddOrUpdateItem(
                item,
                client,
                currentCustomer,
                cancellationToken
            );
        }
        return Results.Ok();
    }
);

app.Run();

static async Task RemoveItemFromCart(UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        var result = await client.Baskets.RemoveProductFromCart(
            new RemoveItemCommand(
                currentCustomer.Id,
                item.ProductId
            ),
            cancellationToken
        );
    }
    catch (ValidationApiException ex)
    {
        // If the product is not in the cart, it does not matter. In this case
        // we don't want to display any error in the UI. If its another exception,
        // we let it propagate up the stack.
        if (ex.StatusCode != HttpStatusCode.NotFound)
        {
            throw;
        }
    }
}

static async Task AddOrUpdateItem(UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        // Add the product to the cart
        var result = await client.Baskets.AddProductToCart(
            new AddItemCommand(
                currentCustomer.Id,
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
            new UpdateQuantityCommand(
                currentCustomer.Id,
                item.ProductId,
                item.Quantity
            ),
            cancellationToken
        );
    }
}

public record class UpdateCartItem(int ProductId, int Quantity);
public record class BasketProduct(int Id, string Name, decimal UnitPrice, int Quantity)
{
    public decimal TotalPrice => UnitPrice * Quantity;
}
