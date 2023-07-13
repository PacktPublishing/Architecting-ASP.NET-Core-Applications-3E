namespace Factory.Data;

public interface IDatabase
{
    Task<IEnumerable<T>> ReadManyAsync<T>(string sql, CancellationToken cancellationToken);
}
