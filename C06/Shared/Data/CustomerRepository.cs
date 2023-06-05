using Shared.Models;
using System.Collections.Immutable;

namespace Shared.Data;

/// <remarks>
/// This is simple demo code, do not use something similar in prod.
/// This code is not thread safe.
/// Using a static MemoryDataStore is a bad idea.
/// </remarks>
public class CustomerRepository : ICustomerRepository
{
    public Task<Customer?> FindAsync(int customerId, CancellationToken cancellationToken)
    {
        var entity = MemoryDataStore.Customers.Find(x => x.Id == customerId);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Customer>> AllAsync(CancellationToken cancellationToken)
    {
        var entities = MemoryDataStore.Customers.ToImmutableArray().AsEnumerable();
        return Task.FromResult(entities);
    }

    public Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        MemoryDataStore.Customers.Add(customer);
        return Task.FromResult(customer);
    }

    public Task<Customer?> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customer.Id);
        if (index == -1)
        {
            return Task.FromResult(default(Customer));
        }
        MemoryDataStore.Customers[index] = customer;
        return Task.FromResult<Customer?>(customer);
    }

    public Task<Customer?> DeleteAsync(int customerId, CancellationToken cancellationToken)
    {
        var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customerId);
        if (index == -1)
        {
            return Task.FromResult(default(Customer));
        }
        var customer = MemoryDataStore.Customers[index];
        MemoryDataStore.Customers.RemoveAt(index);
        return Task.FromResult<Customer?>(customer);
    }
}
