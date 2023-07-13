using Factory.Models;

namespace Factory.Services;

public interface ILocationService
{
    Task<IEnumerable<Location>> FetchAllAsync(CancellationToken cancellationToken);
}
