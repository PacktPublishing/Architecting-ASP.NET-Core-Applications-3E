using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;

namespace MVC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    // GET: api/<CustomerController>
    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAllAsync(ICustomerRepository customerRepository)
    {
        return await customerRepository.AllAsync(HttpContext.RequestAborted);
    }

    // GET api/<CustomerController>/5
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

    // POST api/<CustomerController>
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] Customer value, ICustomerRepository customerRepository)
    {
        var customer = await customerRepository.CreateAsync(value, HttpContext.RequestAborted);
        return Created($"api/customers/{customer.Id}", customer);
    }

    // PUT api/<CustomerController>/5
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

    // DELETE api/<CustomerController>/5
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
