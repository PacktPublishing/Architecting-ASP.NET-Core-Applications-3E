using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace OptionsValidation;

public class ByPassingInterfaces
{
    [Fact]
    public void Should_support_any_scope()
    {
        var services = new ServiceCollection();
        services.AddOptions<Options>()
            .Configure(o => o.MyImportantProperty = "Some important value");
        services.AddScoped(serviceProvider
            => serviceProvider.GetRequiredService<IOptionsSnapshot<Options>>().Value);
        var serviceProvider = services.BuildServiceProvider();

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
        public string? MyImportantProperty { get; set; }
    }
}
