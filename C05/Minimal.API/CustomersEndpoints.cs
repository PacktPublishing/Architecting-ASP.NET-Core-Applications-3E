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
            var customer = await customerRepository.FindAsync(id, cancellationToken);
            if (customer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(customer);
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async (int id, Customer input, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
        {
            var updatedCustomer = await customerRepository.UpdateAsync(input, cancellationToken);
            if (updatedCustomer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(updatedCustomer);
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
            if (deletedCustomer == null)
            {
                return Results.NotFound();
            }
            return TypedResults.Ok(deletedCustomer);
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
