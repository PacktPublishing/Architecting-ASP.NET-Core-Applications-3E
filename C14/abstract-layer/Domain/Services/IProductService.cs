namespace Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken);
}
