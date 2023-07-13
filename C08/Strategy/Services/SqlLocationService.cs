using Strategy.Data;
using Strategy.Models;

namespace Strategy.Services;

public class SqlLocationService : ILocationService
{
    private readonly IDatabase _database;
    public SqlLocationService(IDatabase database)
    {
        _database = database;
    }

    public Task<IEnumerable<Location>> FetchAllAsync(CancellationToken cancellationToken)
    {
        return _database.ReadManyAsync<Location>(
            "SELECT * FROM Location",
            cancellationToken
        );
    }
}
