using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.DTO;
using Shared.Models;

namespace MVC.API.Controllers.DTO;

[Route("dto/[controller]")]
[ApiController]
[Tags("Customers DTO")]
public class CustomersController : ControllerBase
{
    // GET: dto/customers
    [HttpGet]
    public async Task<IEnumerable<CustomerSummary>> GetAllAsync(ICustomerRepository customerRepository)
    {
        // Get all customers
        var customers = await customerRepository.AllAsync(
            HttpContext.RequestAborted
        );

        // Map customers to CustomerSummary DTOs
        var customersSummary = customers
            .Select(customer => new CustomerSummary(
                Id: customer.Id,
                Name: customer.Name,
                TotalNumberOfContracts: customer.Contracts.Count,
                NumberOfOpenContracts: customer.Contracts.Count(x => x.Status.State != WorkState.Completed)
            ))
        ;

        // Return the DTOs
        return customersSummary;
    }

    // GET dto/customers/5
    [HttpGet("{customerId}")]
    public async Task<ActionResult<CustomerDetails>> GetOneAsync(int customerId, ICustomerRepository customerRepository)
    {
        // Get the customers
        var customer = await customerRepository.FindAsync(
            customerId,
            HttpContext.RequestAborted
        );
        if (customer == null)
        {
            return NotFound();
        }

        // Map the customers to a CustomerDetails DTO
        var dto = MapCustomerToCustomerDetails(customer);

        // Return the DTO
        return dto;
    }

    // POST dto/customers
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CreateCustomer input, ICustomerRepository customerRepository)
    {
        // Create the customer
        var createdCustomer = await customerRepository.CreateAsync(
            new(0, input.Name, new()),
            HttpContext.RequestAborted
        );

        // Map the updated customer to a CustomerDetails DTO
        var dto = MapCustomerToCustomerDetails(createdCustomer);

        // Return the DTO
        return Created($"dto/customers/{dto.Id}", dto);
    }

    // PUT dto/customers/5
    [HttpPut("{customerId}")]
    public async Task<ActionResult<CustomerDetails>> PutAsync(int customerId, [FromBody] UpdateCustomer input, ICustomerRepository customerRepository)
    {
        // Get the customer
        var customer = await customerRepository.FindAsync(
            customerId,
            HttpContext.RequestAborted
        );
        if (customer == null)
        {
            return NotFound();
        }

        // Update the customer's name using the UpdateCustomer DTO
        var updatedCustomer = await customerRepository.UpdateAsync(
            customer with { Name = input.Name },
            HttpContext.RequestAborted
        );
        if (updatedCustomer == null)
        {
            return Conflict();
        }

        // Map the updated customer to a CustomerDetails DTO
        var dto = MapCustomerToCustomerDetails(updatedCustomer);

        // Return the DTO
        return dto;
    }

    // DELETE dto/customers/5
    [HttpDelete("{customerId}")]
    public async Task<ActionResult<CustomerDetails>> DeleteAsync(int customerId, ICustomerRepository customerRepository)
    {
        // Delete the customer
        var deletedCustomer = await customerRepository.DeleteAsync(
            customerId,
            HttpContext.RequestAborted
        );
        if (deletedCustomer == null)
        {
            return NotFound();
        }

        // Map the updated customer to a CustomerDetails DTO
        var dto = MapCustomerToCustomerDetails(deletedCustomer);

        // Return the DTO
        return dto;
    }

    private static CustomerDetails MapCustomerToCustomerDetails(Customer customer)
    {
        var dto = new CustomerDetails(
            Id: customer.Id,
            Name: customer.Name,
            Contracts: customer.Contracts.Select(contract => new ContractDetails(
                Id: contract.Id,
                Name: contract.Name,
                Description: contract.Description,

                // Flattening PrimaryContact
                PrimaryContactEmail: contract.PrimaryContact.Email,
                PrimaryContactFirstName: contract.PrimaryContact.FirstName,
                PrimaryContactLastName: contract.PrimaryContact.LastName,

                // Flattening Work
                StatusWorkDone: contract.Status.WorkDone,
                StatusTotalWork: contract.Status.TotalWork,
                StatusWorkState: contract.Status.State.ToString()
            ))
        );
        return dto;
    }
}
