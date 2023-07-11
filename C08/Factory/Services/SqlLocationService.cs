using Factory.Data;
using Factory.Models;

namespace Factory.Services;

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
