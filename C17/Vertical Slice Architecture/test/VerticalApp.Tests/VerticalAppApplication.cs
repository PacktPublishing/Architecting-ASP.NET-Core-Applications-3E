using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using VerticalApp.Data;

namespace VerticalApp;

internal class VerticalAppApplication : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _afterConfigureServices;
    private readonly string _databaseName;
    public VerticalAppApplication([CallerMemberName] string? databaseName = null, Action<IServiceCollection>? afterConfigureServices = null)
    {
        _databaseName = databaseName ?? nameof(VerticalAppApplication);
        _afterConfigureServices = afterConfigureServices;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Override the default ProductContext options to make
            // a different InMemory database per test case so there is no
            // seed conflicts.
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<ProductContext>()
                    .UseInMemoryDatabase(_databaseName)
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
            _afterConfigureServices?.Invoke(services);
        });
        return base.CreateHost(builder);
    }

    public Task SeedAsync(Func<ProductContext, Task> seeder)
    {
        using var seedScope = Services.CreateScope();
        var db = seedScope.ServiceProvider.GetRequiredService<ProductContext>();
        return seeder(db);
    }
}
