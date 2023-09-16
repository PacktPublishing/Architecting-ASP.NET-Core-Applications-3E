namespace REPR.Products.Contracts;

public record class FetchAllProductsResponse(IEnumerable<FetchAllProductsResponseProduct> Products);
