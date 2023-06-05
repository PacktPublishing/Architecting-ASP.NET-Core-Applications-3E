using Shared.Models;
using Shared.Data;

namespace Minimal.API;

public static class CustomersEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customers").WithTags(nameof(Customer));

        group.MapGet("/", async (ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            return await customerRepository.AllAsync(cancellationToken);
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async (int id, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            return await customerRepository.FindAsync(id, cancellationToken);
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async (int id, Customer input, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            await customerRepository.UpdateAsync(input, cancellationToken);
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer model, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            var createdCustomer = await customerRepository.CreateAsync(model, cancellationToken);
            return TypedResults.Created($"/api/Customers/{createdCustomer.Id}", createdCustomer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            var deletedCustomer = await customerRepository.DeleteAsync(id, cancellationToken);
            return TypedResults.Ok(deletedCustomer);
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
