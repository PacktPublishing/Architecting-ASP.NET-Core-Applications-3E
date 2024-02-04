using Refit;
using Baskets.Contracts;

namespace API.HttpClient;

public interface IBasketsClient
{
    [Get("/baskets/{query.CustomerId}")]
    Task<IEnumerable<FetchItemsResponseItem>> FetchCustomerBasketAsync(
        FetchItemsQuery query,
        CancellationToken cancellationToken);

    [Post("/baskets")]
    Task<AddItemResponse> AddProductToCart(
        AddItemCommand command,
        CancellationToken cancellationToken);

    [Delete("/baskets/{command.CustomerId}/{command.ProductId}")]
    Task<RemoveItemResponse> RemoveProductFromCart(
        RemoveItemCommand command,
        CancellationToken cancellationToken);

    [Put("/baskets")]
    Task<UpdateQuantityResponse> UpdateProductQuantity(
        UpdateQuantityCommand command,
        CancellationToken cancellationToken);
}
