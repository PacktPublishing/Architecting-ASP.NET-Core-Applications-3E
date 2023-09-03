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
}
