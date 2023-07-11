using Factory.Models;

namespace Factory.Services;

public class InMemoryLocationService : ILocationService
{
    public async Task<IEnumerable<Location>> FetchAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(Random.Shared.Next(1, 100), cancellationToken);
        return new Location[] {
            new Location(1, "Paris", "FR"),
            new Location(2, "New York City", "US"),
            new Location(3, "Tokyo", "JP"),
            new Location(4, "Rome", "IT"),
            new Location(5, "Sydney", "AU"),
            new Location(6, "Cape Town", "ZA"),
            new Location(7, "Istanbul", "TR"),
            new Location(8, "Bangkok", "TH"),
            new Location(9, "Rio de Janeiro", "BR"),
            new Location(10, "Toronto", "CA"),
        };
    }
}
