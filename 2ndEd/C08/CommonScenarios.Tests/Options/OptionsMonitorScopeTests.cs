using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonScenarios.Options;

public class OptionsMonitorScopeTests
{
    public const string Option1Name = "Options 1";
    private readonly IServiceProvider _serviceProvider;

    public OptionsMonitorScopeTests()
    {
        var services = new ServiceCollection();
        services.Configure<MyOptions>("Options1", myOptions =>
        {
            myOptions.Name = Option1Name;
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
        var monitor1 = _serviceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
        var monitor2 = _serviceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
        Assert.Same(monitor1, monitor2);

        var options1_1 = monitor1.Get("Options1");
        var options1_2 = monitor1.Get("Options1");
        var options2_1 = monitor2.Get("Options1");
        var options2_2 = monitor2.Get("Options1");

        Assert.Same(options1_1, options1_2);
        Assert.Same(options1_2, options2_1);
        Assert.Same(options2_1, options2_2);

        Assert.NotNull(monitor1.CurrentValue);

        Assert.Equal(Option1Name, options1_1.Name);
    }

    [Fact]
    public void TestScope()
    {
        using var scope1 = _serviceProvider.CreateScope();
        using var scope2 = _serviceProvider.CreateScope();
        var monitor1 = scope1.ServiceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
        var monitor2 = scope2.ServiceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
        Assert.Same(monitor1, monitor2);

        var options1_1 = monitor1.Get("Options1");
        var options1_2 = monitor1.Get("Options1");
        var options2_1 = monitor2.Get("Options1");
        var options2_2 = monitor2.Get("Options1");

        Assert.Same(options1_1, options1_2);
        Assert.Same(options1_2, options2_1);
        Assert.Same(options2_1, options2_2);

        Assert.NotNull(monitor1.CurrentValue);

        Assert.Equal(Option1Name, options1_1.Name);
    }
}
