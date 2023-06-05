using Shared.Models;

namespace Shared.Data;
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> AllAsync(CancellationToken cancellationToken);
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer?> DeleteAsync(int customerId, CancellationToken cancellationToken);
    Task<Customer?> FindAsync(int customerId, CancellationToken cancellationToken);
    Task<Customer?> UpdateAsync(Customer customer, CancellationToken cancellationToken);
}