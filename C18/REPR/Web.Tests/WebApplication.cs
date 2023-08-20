using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace Web;

public class C18WebApplication : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _afterConfigureServices;
    private readonly string _databaseName;
    public C18WebApplication([CallerMemberName] string? databaseName = null, Action<IServiceCollection>? afterConfigureServices = null)
    {
        _databaseName = databaseName ?? nameof(C18WebApplication);
        // Add some randomness to the database name to ensure uniqueness
        // for test methods that have the same name.
        _databaseName += Guid.NewGuid().ToString();
        _afterConfigureServices = afterConfigureServices;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Override the default DbContext options to make
            // a different InMemory database per test case so there is no
            // seed conflicts.
            services
                .AddScoped(ConfigureContext<Features.Products.ProductContext>)
                .AddScoped(ConfigureContext<Features.Baskets.BasketContext>)
            ;
            _afterConfigureServices?.Invoke(services);
        });
        return base.CreateHost(builder);
    }

    public DbContextOptions<TDbContext> ConfigureContext<TDbContext>(IServiceProvider sp)
        where TDbContext : DbContext
    {
        return new DbContextOptionsBuilder<TDbContext>()
            .UseInMemoryDatabase(_databaseName + typeof(TDbContext).Name)
            .UseApplicationServiceProvider(sp)
            .Options;
    }

    public Task SeedAsync<TDbContext>(Func<TDbContext, Task> seeder)
        where TDbContext : DbContext
    {
        using var seedScope = Services.CreateScope();
        var db = seedScope.ServiceProvider.GetRequiredService<TDbContext>();
        return seeder(db);
    }
}