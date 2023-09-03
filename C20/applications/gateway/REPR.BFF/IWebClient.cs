using Refit;
using REPR.Baskets.Contracts;
using REPR.Products.Contracts;

namespace REPR.BFF;

public interface IWebClient
{
    IBasketsClient Baskets { get; }
    IProductsClient Catalog { get; }
}

public class DefaultWebClient : IWebClient
{
    public DefaultWebClient(IBasketsClient baskets, IProductsClient catalog)
    {
        Baskets = baskets ?? throw new ArgumentNullException(nameof(baskets));
        Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    }

    public IBasketsClient Baskets { get; }
    public IProductsClient Catalog { get; }
}

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

public interface IProductsClient
{
    [Get("/products/{query.ProductId}")]
    Task<FetchOneProductResponse> FetchProductAsync(
        FetchOneProductQuery query,
        CancellationToken cancellationToken);

    [Get("/products")]
    Task<FetchAllProductsResponse> FetchProductsAsync(
        CancellationToken cancellationToken);

    [Post("/products")]
    Task<CreateProductResponse> CreateProductAsync(
        CreateProductCommand command,
        CancellationToken cancellationToken);

    [Delete("/products/{command.ProductId}")]
    Task<DeleteProductResponse> DeleteProductAsync(
        DeleteProductCommand command,
        CancellationToken cancellationToken);


}