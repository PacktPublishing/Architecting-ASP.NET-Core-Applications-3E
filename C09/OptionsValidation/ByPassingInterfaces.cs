using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace OptionsValidation;

public class ByPassingInterfaces
{
    [Fact]
    public void Should_support_any_scope()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOptions<Options>()
            .Configure(o => o.Name = "John Doe");
        //
        // The following one liner does the same things,
        // but is harder to format in a printed book.
        //
        //services.AddScoped(serviceProvider
        //    => serviceProvider.GetRequiredService<IOptionsSnapshot<Options>>().Value);
        //
        services.AddScoped(serviceProvider => {
            var snapshot = serviceProvider
                .GetRequiredService<IOptionsSnapshot<Options>>();
            return snapshot.Value;
        });
        var serviceProvider = services.BuildServiceProvider();

        // Act & Assert
        using var scope1 = serviceProvider.CreateScope();
        var options1 = scope1.ServiceProvider.GetService<Options>();
        var options2 = scope1.ServiceProvider.GetService<Options>();
        Assert.Same(options1, options2);

        using var scope2 = serviceProvider.CreateScope();
        var options3 = scope2.ServiceProvider.GetService<Options>();
        Assert.NotSame(options2, options3);
    }

    private class Options
    {
        public string? Name { get; set; }
    }
}
