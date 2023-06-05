using MVC.Models;

namespace MVC.Data;
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> AllAsync();
    Task CreateAsync(Customer customer);
    Task DeleteAsync(int customerId);
    Task<Customer?> FindAsync(int customerId);
    Task UpdateAsync(Customer customer);
}