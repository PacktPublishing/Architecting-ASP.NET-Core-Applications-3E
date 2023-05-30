namespace InterfaceSegregation.Before;
public class ProductRepository : IProductRepository
{
    public ValueTask<IEnumerable<Product>> GetAllPrivateProductAsync()
        => throw new NotImplementedException();
    public ValueTask<IEnumerable<Product>> GetAllPublicProductAsync()
        => throw new NotImplementedException();
    public ValueTask<Product> GetOnePrivateProductAsync(int productId)
        => throw new NotImplementedException();
    public ValueTask<Product> GetOnePublicProductAsync(int productId)
        => throw new NotImplementedException();
    public ValueTask CreateAsync(Product product)
        => throw new NotImplementedException();
    public ValueTask UpdateAsync(Product product)
        => throw new NotImplementedException();
    public ValueTask DeleteAsync(Product product)
        => throw new NotImplementedException();
}
