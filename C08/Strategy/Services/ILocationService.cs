using Strategy.Models;

namespace Strategy.Services;

public interface ILocationService
{
    Task<IEnumerable<Location>> FetchAllAsync(CancellationToken cancellationToken);
}
