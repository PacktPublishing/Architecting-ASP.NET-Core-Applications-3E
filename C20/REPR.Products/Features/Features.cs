using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Web.Features;

public static class Features
{
    public static IServiceCollection AddFeatures(this WebApplicationBuilder builder)
    {
        // Register fluent validation
        builder.AddFluentValidationEndpointFilter();
        return builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())

            // Add features
            .AddProductsFeature()
        ;
    }

    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/")
            .AddFluentValidationFilter();
        ;
        group.MapProductsFeature();
        return endpoints;
    }

    public static async Task SeedFeaturesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await scope.SeedProductsAsync();
    }
}
