using Microsoft.EntityFrameworkCore.Diagnostics;
using REPR.Baskets.Features;

namespace REPR.Baskets;

public static class BasketModuleExtensions
{
    public static WebApplicationBuilder AddBasketModule(this WebApplicationBuilder builder)
    {
        builder.Services
            // Add features
            .AddAddItem()
            .AddFetchItems()
            .AddRemoveItem()
            .AddUpdateQuantity()

            // Add and configure db context
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
