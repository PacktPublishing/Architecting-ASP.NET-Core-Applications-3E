using Infrastructure.Data.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using Xunit;

namespace Web;

public class StartupTest
{
    [Fact]
    public async Task The_products_endpoint_should_be_reachable()
    {
        // Arrange
        await using var application = new AutoMapperAppWebApplication();
        using var client = application.CreateClient();

        // Act
        using var response = await client.GetAsync("/products");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}

internal class AutoMapperAppWebApplication : WebApplicationFactory<Program>
{
    private readonly string _databaseName;
    public AutoMapperAppWebApplication([CallerMemberName]string? databaseName = default)
    {
        _databaseName = databaseName ?? nameof(AutoMapperAppWebApplication);
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
        });
        return base.CreateHost(builder);
    }
}
