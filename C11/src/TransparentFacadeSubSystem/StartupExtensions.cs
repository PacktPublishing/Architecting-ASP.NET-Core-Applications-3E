using Microsoft.Extensions.DependencyInjection.Extensions;
using TransparentFacadeSubSystem;
using TransparentFacadeSubSystem.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupExtensions
{
    public static IServiceCollection AddTransparentFacadeSubSystem(this IServiceCollection services)
    {
        services.TryAddSingleton<IInventoryService, InventoryService>();
        services.TryAddSingleton<IOrderProcessingService, OrderProcessingService>();
        services.TryAddSingleton<IShippingService, ShippingService>();
        services.TryAddSingleton<IECommerceTransparentFacade, ECommerceFacade>();
        return services;
    }
}
