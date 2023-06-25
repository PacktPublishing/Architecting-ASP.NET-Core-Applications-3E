using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;

namespace MVC.API.Controllers.Empty;

[Route("empty/[controller]")]
[ApiController]
[Tags("Customers Empty")]
public class CustomersController : ControllerBase
{
    // GET: empty/customers
    [HttpGet]
    public Task<IEnumerable<Customer>> GetAllAsync(ICustomerRepository customerRepository)
        => throw new NotImplementedException();

    // GET empty/customers/5
    [HttpGet("{id}")]
    public Task<ActionResult<Customer>> GetOneAsync(int id, ICustomerRepository customerRepository)
        => throw new NotImplementedException();

    // POST empty/customers
    [HttpPost]
    public Task<ActionResult> PostAsync([FromBody] Customer value, ICustomerRepository customerRepository)
        => throw new NotImplementedException();

    // PUT empty/customers/5
    [HttpPut("{id}")]
    public Task<ActionResult<Customer>> PutAsync(int id, [FromBody] Customer value, ICustomerRepository customerRepository)
        => throw new NotImplementedException();

    // DELETE empty/customers/5
    [HttpDelete("{id}")]
    public Task<ActionResult<Customer>> DeleteAsync(int id, ICustomerRepository customerRepository)
        => throw new NotImplementedException();
}
