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
            .AddBasketsFeature()
        ;
    }

    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/")
            .AddFluentValidationFilter();
        ;
        group.MapBasketsFeature();
        return endpoints;
    }
}
