using MVC.Models;
using System.Collections.Immutable;

namespace MVC.Data;

public class CustomerRepository : ICustomerRepository
{
    public Task<Customer?> FindAsync(int customerId)
    {
        var entity = MemoryDataStore.Customers.Find(x => x.Id == customerId);
        return Task.FromResult(entity);
    }
    public Task<IEnumerable<Customer>> AllAsync()
    {
        var entities = MemoryDataStore.Customers.ToImmutableArray().AsEnumerable();
        return Task.FromResult(entities);
    }
    public Task CreateAsync(Customer customer)
    {
        MemoryDataStore.Customers.Add(customer);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Customer customer)
    {
        var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customer.Id);
        MemoryDataStore.Customers[index] = customer;
        return Task.CompletedTask;
    }
    public Task DeleteAsync(int customerId)
    {
        var index = MemoryDataStore.Customers.FindIndex(x => x.Id == customerId);
        MemoryDataStore.Customers.RemoveAt(index);
        return Task.CompletedTask;
    }
}
