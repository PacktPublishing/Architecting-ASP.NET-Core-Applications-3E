using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;

namespace MVC.API.Controllers.Raw;

[Route("raw/[controller]")]
[ApiController]
[Tags("Customers Raw")]
public class CustomersController : ControllerBase
{
    // GET: raw/customers
    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAllAsync(ICustomerRepository customerRepository)
    {
        return await customerRepository.AllAsync(HttpContext.RequestAborted);
    }

    // GET raw/customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetOneAsync(int id, ICustomerRepository customerRepository)
    {
        var customer = await customerRepository.FindAsync(id, HttpContext.RequestAborted);
        if(customer == null)
        {
            return NotFound();
        }
        return customer;
    }

    // POST raw/customers
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] Customer value, ICustomerRepository customerRepository)
    {
        var customer = await customerRepository.CreateAsync(value, HttpContext.RequestAborted);
        return Created($"api/customers/{customer.Id}", customer);
    }

    // PUT raw/customers/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> PutAsync(int id, [FromBody] Customer value, ICustomerRepository customerRepository)
    {
        var customer = await customerRepository.UpdateAsync(value, HttpContext.RequestAborted);
        if (customer == null)
        {
            return NotFound();
        }
        return customer;
    }

    // DELETE raw/customers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Customer>> DeleteAsync(int id, ICustomerRepository customerRepository)
    {
        var customer = await customerRepository.DeleteAsync(id, HttpContext.RequestAborted);
        if (customer == null)
        {
            return NotFound();
        }
        return customer;
    }
}
