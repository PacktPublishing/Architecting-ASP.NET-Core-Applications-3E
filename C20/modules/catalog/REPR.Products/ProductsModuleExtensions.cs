using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using REPR.Products.Data;
using REPR.Products.Features;
using System.Reflection;

namespace REPR.Products;

public static class ProductsModuleExtensions
{
    public static WebApplicationBuilder AddProductsModule(this WebApplicationBuilder builder)
    {
        // Register fluent validation
        builder.AddFluentValidationEndpointFilter();
        builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())

            // Add features
            .AddFetchAll()
            .AddFetchOneProduct()
            .AddCreateProduct()
            .AddDeleteProduct()

            // Add and configure db context
            .AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("ProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            )
        ;
        return builder;
    }

    public static IEndpointRouteBuilder MapProductsModule(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(nameof(Products).ToLower())
            .WithTags(nameof(Products))
            .AddFluentValidationFilter()

            // Map endpoints
            .MapFetchAll()
            .MapFetchOneProduct()
            .MapCreateProduct()
            .MapDeleteProduct()
        ;
        return endpoints;
    }

    private static async Task SeedProductsModuleAsync(this WebApplication app)
    {
        // TODO: update this to use commands instead so the events get triggered.
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
        db.Products.Add(new Product(
            Name: "Banana",
            UnitPrice: 0.30m,
            Id: 1
        ));
        db.Products.Add(new Product(
            Name: "Apple",
            UnitPrice: 0.79m,
            Id: 2
        ));
        db.Products.Add(new Product(
            Name: "Habanero Pepper",
            UnitPrice: 0.99m,
            Id: 3
        ));
        await db.SaveChangesAsync();
    }
}
