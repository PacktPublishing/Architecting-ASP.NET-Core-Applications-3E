using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using REPR.Baskets.Data;
using REPR.Baskets.Features;
using System.Reflection;

namespace REPR.Baskets;

public static class BasketModuleExtensions
{
    public static WebApplicationBuilder AddBasketModule(this WebApplicationBuilder builder)
    {
        // Register fluent validation
        builder.AddFluentValidationEndpointFilter();
        builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())

            // Add features
            .AddAddItem()
            .AddFetchItems()
            .AddRemoveItem()
            .AddUpdateQuantity()
            .AddDbContext<BasketContext>(options => options
                .UseInMemoryDatabase("BasketContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            )
        ;
        return builder;
    }

    public static IEndpointRouteBuilder MapBasketModule(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(nameof(Baskets).ToLower())
            .WithTags(nameof(Baskets))
            .AddFluentValidationFilter()

            // Map endpoints
            .MapFetchItems()
            .MapAddItem()
            .MapUpdateQuantity()
            .MapRemoveItem()
        ;
        return endpoints;
    }

    public static void AddBasketModuleConsumers(this IRegistrationConfigurator configurator)
    {
        configurator.AddConsumers(typeof(ProductEventsConsumers));
    }
}
