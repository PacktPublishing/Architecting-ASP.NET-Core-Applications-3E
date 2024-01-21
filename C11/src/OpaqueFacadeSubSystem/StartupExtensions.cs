using OpaqueFacadeSubSystem;
using OpaqueFacadeSubSystem.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupExtensions
{
    public static IServiceCollection AddOpaqueFacadeSubSystem(this IServiceCollection services)
    {
        services.AddSingleton<IECommerceOpaqueFacade>(serviceProvider
            => new ECommerceFacade(new InventoryService(), new OrderProcessingService(), new ShippingService()));
        return services;
    }
}
