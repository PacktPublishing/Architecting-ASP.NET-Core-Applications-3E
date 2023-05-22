using Data.Abstract;

namespace Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken)
    {
        return (await _repository.AllAsync(cancellationToken)).Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            QuantityInStock = p.QuantityInStock
        });
    }
}
