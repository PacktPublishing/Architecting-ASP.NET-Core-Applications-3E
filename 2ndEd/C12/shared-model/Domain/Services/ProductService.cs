using Data.Abstract;
using Model;

namespace Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken)
        => _repository.AllAsync(cancellationToken);
}
