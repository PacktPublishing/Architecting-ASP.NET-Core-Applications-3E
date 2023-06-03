using Microsoft.AspNetCore.Mvc;
using DTOs.Models;
using DTOs.Services;

namespace DTOs.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly CustomerRepository _customerRepository = new();

    // GET customers
    [HttpGet]
    public ActionResult<IEnumerable<CustomerSummaryDto>> Get()
    {
        var customers = _customerRepository.ReadAll();
        var dto = customers.Select(customer => new CustomerSummaryDto(
            Id: customer.Id,
            Name: customer.Name,
            TotalNumberOfContracts: customer.Contracts.Count,
            NumberOfOpenContracts: customer.Contracts.Count(x => x.Work.State != WorkState.Completed)
        )).ToArray();
        return dto;
    }

    // GET customers/1
    [HttpGet("{id}")]
    public ActionResult<CustomerDetailsDto> Get(int id)
    {
        var customer = _customerRepository.ReadOne(id);
        if (customer == default)
        {
            return NotFound();
        }
        var dto = new CustomerDetailsDto(
            Id: customer.Id,
            Name: customer.Name,
            Contracts: customer.Contracts.Select(contract => new ContractDetailsDto(
                Id: contract.Id,
                Name: contract.Name,
                Description: contract.Description,

                // Flattening PrimaryContact
                PrimaryContactEmail: contract.PrimaryContact.Email,
                PrimaryContactFirstname: contract.PrimaryContact.Firstname,
                PrimaryContactLastname: contract.PrimaryContact.Lastname,

                // Flattening Work
                WorkDone: contract.Work.Done,
                WorkState: contract.Work.State,
                WorkTotal: contract.Work.Total
            ))
        );
        return Ok(dto);
    }
}
