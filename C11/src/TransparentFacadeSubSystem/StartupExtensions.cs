using TransparentFacadeSubSystem;
using TransparentFacadeSubSystem.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class StartupExtensions
{
    public static IServiceCollection AddTransparentFacadeSubSystem(this IServiceCollection services)
    {
        services.AddSingleton<InventoryService>();
        services.AddSingleton<OrderProcessingService>();
        services.AddSingleton<ShippingService>();
        services.AddSingleton<IECommerceTransparentFacade, ECommerceFacade>();
        return services;
    }
}
