using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRepository<T, TId>
        where T : class, IEntity<TId>
    {
        Task<IEnumerable<T>> AllAsync(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(TId id, CancellationToken cancellationToken);
    }

    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
