using Refit;
using Web.Features;

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
    Task<IEnumerable<Baskets.FetchItems.Item>> FetchCustomerBasketAsync(
        Baskets.FetchItems.Query query,
        CancellationToken cancellationToken);

    [Post("/baskets")]
    Task<Baskets.AddItem.Response> AddProductToCart(
        Baskets.AddItem.Command command,
        CancellationToken cancellationToken);

    [Delete("/baskets/{command.CustomerId}/{command.ProductId}")]
    Task<Baskets.RemoveItem.Response> RemoveProductFromCart(
        Baskets.RemoveItem.Command command,
        CancellationToken cancellationToken);

    [Put("/baskets")]
    Task<Baskets.UpdateQuantity.Response> UpdateProductQuantity(
        Baskets.UpdateQuantity.Command command,
        CancellationToken cancellationToken);
}

public interface IProductsClient
{
    [Get("/products/{query.ProductId}")]
    Task<Products.FetchOne.Response> FetchProductAsync(
        Products.FetchOne.Query query,
        CancellationToken cancellationToken);

    [Get("/products")]
    Task<Products.FetchAll.Response> FetchProductsAsync(
        CancellationToken cancellationToken);
}