using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        _ = endpoints
            .MapGroup(Constants.ModuleName.ToLower())
            .WithTags(Constants.ModuleName)
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
