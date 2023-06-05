using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Shared;
public static  class StartupExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        return services;
    }

    public static IApplicationBuilder InitializeSharedDataStore(this IApplicationBuilder app)
    {
        MemoryDataStore.Seed();
        return app;
    }
}
