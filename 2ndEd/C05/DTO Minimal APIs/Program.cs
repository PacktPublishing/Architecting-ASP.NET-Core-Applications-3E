using DTO.Data;
using DTO.Data.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomerRepository>();
var app = builder.Build();

app.MapGet("/", (HttpContext context) => new[] {
    $"https://{context.Request.Host}/customers",
    $"https://{context.Request.Host}/customers/1",
});
app.MapGet("/customers", (CustomerRepository customerService) => {
    var customers = customerService.ReadAll();
    var dto = customers.Select(customer => new CustomerSummary(
        Id: customer.Id,
        Name: customer.Name,
        TotalNumberOfContracts: customer.Contracts.Count,
        NumberOfOpenContracts: customer.Contracts.Count(x => x.Work.State != WorkState.Completed)
    ));
    return dto;
});
app.MapGet("/customers/{id:int}", (CustomerRepository customerService, int id) => {
    var customer = customerService.ReadOne(id);
    if (customer == default)
    {
        return Results.NotFound();
    }
    var dto = new CustomerDetails(
        Id: customer.Id,
        Name: customer.Name,
        Contracts: customer.Contracts.Select(contract => new ContractDetails(
            Id: contract.Id,
            Name: contract.Name,
            Description: contract.Description,

            // Flattening PrimaryContact
            PrimaryContactEmail: contract.PrimaryContact.Email,
            PrimaryContactFirstname: contract.PrimaryContact.Firstname,
            PrimaryContactLastname: contract.PrimaryContact.Lastname,

            // Flattening Work
            WorkDone: contract.Work.Done,
            WorkState: contract.Work.State.ToString(),
            WorkTotal: contract.Work.Total
        ))
    );
    return Results.Ok(dto);
});
app.Run();


public record class ContractDetails(
    int Id,
    string Name,
    string Description,
    int WorkTotal,
    int WorkDone,
    string WorkState,
    string PrimaryContactFirstname,
    string PrimaryContactLastname,
    string PrimaryContactEmail
);
public record class CustomerDetails(
    int Id,
    string Name,
    IEnumerable<ContractDetails> Contracts
);
public record class CustomerSummary(
    int Id,
    string Name,
    int TotalNumberOfContracts,
    int NumberOfOpenContracts
);