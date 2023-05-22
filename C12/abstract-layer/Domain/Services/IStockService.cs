namespace Domain.Services;

public interface IStockService
{
    Task<int> AddStockAsync(int productId, int amount, CancellationToken cancellationToken);
    Task<int> RemoveStockAsync(int productId, int amount, CancellationToken cancellationToken);
}
