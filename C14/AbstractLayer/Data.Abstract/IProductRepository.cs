namespace Data.Abstract;

public interface IProductRepository
{
    Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken);
    Task<Product?> FindByIdAsync(int productId, CancellationToken cancellationToken);
    Task CreateAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(int productId, CancellationToken cancellationToken);
}
