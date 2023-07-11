namespace Factory.Data;

public class NotImplementedDatabase : IDatabase
{
    public Task<IEnumerable<T>> ReadManyAsync<T>(string sql, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
