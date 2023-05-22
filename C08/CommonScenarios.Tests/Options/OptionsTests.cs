using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonScenarios.Options;

public class OptionsTests
{
    public const string OptionName = "Default Options";
    private readonly IServiceProvider _serviceProvider;

    public OptionsTests()
    {
        var services = new ServiceCollection();
        services.Configure<MyOptions>(myOptions =>
        {
            myOptions.Name = OptionName;
        });
        services.Configure<MyOptions>("Options2", myOptions =>
        {
            myOptions.Name = "Options 2";
        });
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void TestSingleton()
    {
        var options1 = _serviceProvider.GetRequiredService<IOptions<MyOptions>>();
        var options2 = _serviceProvider.GetRequiredService<IOptions<MyOptions>>();
        Assert.Same(options1, options2);
        Assert.Same(options1.Value, options2.Value);
        Assert.Equal(OptionName, options1.Value.Name);
    }

    [Fact]
    public void TestScope()
    {
        using var scope1 = _serviceProvider.CreateScope();
        using var scope2 = _serviceProvider.CreateScope();
        var options1 = scope1.ServiceProvider.GetRequiredService<IOptions<MyOptions>>();
        var options2 = scope2.ServiceProvider.GetRequiredService<IOptions<MyOptions>>();
        Assert.Same(options1, options2);
        Assert.Same(options1.Value, options2.Value);
        Assert.Equal(OptionName, options1.Value.Name);
    }
}
