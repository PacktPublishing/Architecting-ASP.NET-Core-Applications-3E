using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Shared;
public static  class StartupExtensions
{
    public static IServiceCollection AddCustomerRepository(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        return services;
    }

    public static IApplicationBuilder InitializeSharedDataStore(this IApplicationBuilder app)
    {
        MemoryDataStore.Seed();
        return app;
    }

    public static WebApplication UseDarkSwaggerUI(this WebApplication app)
    {
        // SwaggerDark.css source: https://dev.to/amoenus/turn-swagger-theme-to-the-dark-mode-4l5f
        app.UseSwaggerUI(c =>
        {
            c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
        });
        app.MapGet("/swagger-ui/SwaggerDark.css", async (CancellationToken cancellationToken) =>
        {
            var css = await File.ReadAllBytesAsync("SwaggerDark.css", cancellationToken);
            return Results.File(css, "text/css");
        }).ExcludeFromDescription();
        return app;
    }
}
