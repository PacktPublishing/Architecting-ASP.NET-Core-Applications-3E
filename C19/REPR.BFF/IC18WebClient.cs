using Refit;
using Web.Features;

namespace REPR.BFF;

public interface IC18WebClient
{
    IC18WebBasketsClient Baskets { get; }
    IC18WebProductsClient Catalog { get; }
}

public class DefaultWebClient : IC18WebClient
{
    public DefaultWebClient(IC18WebBasketsClient baskets, IC18WebProductsClient catalog)
    {
        Baskets = baskets ?? throw new ArgumentNullException(nameof(baskets));
        Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    }

    public IC18WebBasketsClient Baskets { get; }
    public IC18WebProductsClient Catalog { get; }
}

public interface IC18WebBasketsClient
{
    [Get("/baskets/{customerId}")]
    Task<IEnumerable<Baskets.FetchItems.Item>> FetchCustomerBasketAsync(int customerId, CancellationToken cancellationToken);

    [Post("/baskets")]
    Task<Baskets.AddItem.Response> AddProductToCart(Baskets.AddItem.Command command, CancellationToken cancellationToken);

    [Delete("/baskets/{command.CustomerId}/{command.ProductId}")]
    Task<Baskets.RemoveItem.Response> RemoveProductFromCart(Baskets.RemoveItem.Command command, CancellationToken cancellationToken);

    [Put("/baskets")]
    Task<Baskets.UpdateQuantity.Response> UpdateProductQuantity(Baskets.UpdateQuantity.Command command, CancellationToken cancellationToken);
}

public interface IC18WebProductsClient
{
    [Get("/products/{productId}")]
    Task<Products.FetchOne.Response> FetchProductAsync(int productId, CancellationToken cancellationToken);

    [Get("/products")]
    Task<Products.FetchAll.Response> FetchProductsAsync(CancellationToken cancellationToken);
}