using Data.Abstract;

namespace Domain.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _repository;
    public StockService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> AddStockAsync(int productId, int amount, CancellationToken cancellationToken)
    {
        var product = await _repository.FindByIdAsync(productId, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(productId);
        }
        product.QuantityInStock += amount;
        await _repository.UpdateAsync(product, cancellationToken);

        return product.QuantityInStock;
    }

    public async Task<int> RemoveStockAsync(int productId, int amount, CancellationToken cancellationToken)
    {
        var product = await _repository.FindByIdAsync(productId, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(productId);
        }
        if (amount > product.QuantityInStock)
        {
            throw new NotEnoughStockException(product.QuantityInStock, amount);
        }
        product.QuantityInStock -= amount;
        await _repository.UpdateAsync(product, cancellationToken);

        return product.QuantityInStock;
    }
}
