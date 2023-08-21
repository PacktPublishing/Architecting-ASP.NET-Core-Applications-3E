using Refit;
using REPR.BFF;
using System;
using System.Collections.Concurrent;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var baseAddress = builder.Configuration
    .GetValue<string>("WebAppBaseAddress") ?? throw new NotSupportedException();

builder.Services
    .AddRefitClient<IC18WebBasketsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress))
;
builder.Services
    .AddRefitClient<IC18WebProductsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress))
;
builder.Services.AddTransient<IC18WebClient, DefaultWebClient>();
builder.Services.AddScoped<ICurrentCustomerService, FakeCurrentCustomerService>();

var app = builder.Build();

app.MapGet(
    "api/catalog",
    (IC18WebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductsAsync(cancellationToken)
);
app.MapGet(
    "api/cart",
    async (IC18WebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        var logger = app.Services
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GetCart")
        ;
        var basket = await client.Baskets.FetchCustomerBasketAsync(
            currentCustomer.Id,
            cancellationToken
        );
        var result = new ConcurrentBag<BasketProduct>();
        await Parallel.ForEachAsync(basket, cancellationToken, async (item, cancellationToken) =>
        {
            logger.LogTrace("Fetching product '{ProductId}'.", item.ProductId);
            var product = await client.Catalog.FetchProductAsync(item.ProductId, cancellationToken);
            logger.LogTrace("Found product '{ProductId}' ({ProductName}).", item.ProductId, product.Name);
            result.Add(new BasketProduct(product.Id, product.Name, product.UnitPrice, item.Quantity));
        });
        // Log example
        // TODO: DELETE THIS
        //trce: GetCart[0]
        //      Fetching product '3'.
        //trce: GetCart[0]
        //      Fetching product '2'.
        //trce: GetCart[0]
        //      Found product '2'(Apple).
        //trce: GetCart[0]
        //      Found product '3'(Habanero Pepper).
        return result;
    }
);
app.MapPost(
    "api/cart",
    async (UpdateCartItem item, IC18WebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        if (item.Quantity <= 0)
        {
            await RemoveItemFromCart(item, client, currentCustomer, cancellationToken);
        }
        else
        {
            await AddOrUpdateItem(item, client, currentCustomer, cancellationToken);
        }
        return Results.Ok();
    }
);

app.Run();

static async Task RemoveItemFromCart(UpdateCartItem item, IC18WebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        var result = await client.Baskets.RemoveProductFromCart(
            new Web.Features.Baskets.RemoveItem.Command(
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

static async Task AddOrUpdateItem(UpdateCartItem item, IC18WebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        // Add the product to the cart
        var result = await client.Baskets.AddProductToCart(
            new Web.Features.Baskets.AddItem.Command(
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
            new Web.Features.Baskets.UpdateQuantity.Command(
                currentCustomer.Id,
                item.ProductId,
                item.Quantity
            ),
            cancellationToken
        );
    }
}

public record class UpdateCartItem(int ProductId, int Quantity);
public record class BasketProduct(int Id, string Name, decimal UnitPrice, int Quantity);
