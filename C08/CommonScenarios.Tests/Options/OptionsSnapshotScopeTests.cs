using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonScenarios.Options;

public class OptionsSnapshotScopeTests
{
    public const string Option1Name = "Options 1";
    private readonly IServiceProvider _serviceProvider;

    public OptionsSnapshotScopeTests()
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
        var snapshot1 = _serviceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();
        var snapshot2 = _serviceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();
        Assert.Same(snapshot1, snapshot2);

        var options1_1 = snapshot1.Get("Options1");
        var options1_2 = snapshot1.Get("Options1");
        var options2_1 = snapshot2.Get("Options1");
        var options2_2 = snapshot2.Get("Options1");

        Assert.Same(options1_1, options1_2);
        Assert.Same(options1_2, options2_1);
        Assert.Same(options2_1, options2_2);

        Assert.NotNull(snapshot1.Value);

        Assert.Equal(Option1Name, options1_1.Name);
    }

    [Fact]
    public void TestScope()
    {
        using var scope1 = _serviceProvider.CreateScope();
        using var scope2 = _serviceProvider.CreateScope();
        var snapshot1 = scope1.ServiceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();
        var snapshot2 = scope2.ServiceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();
        Assert.NotSame(snapshot1, snapshot2);

        var options1_1 = snapshot1.Get("Options1");
        var options1_2 = snapshot1.Get("Options1");
        var options2_1 = snapshot2.Get("Options1");
        var options2_2 = snapshot2.Get("Options1");

        Assert.Same(options1_1, options1_2);
        Assert.NotSame(options1_2, options2_1);
        Assert.Same(options2_1, options2_2);

        Assert.NotNull(snapshot1.Value);

        Assert.Equal(Option1Name, options1_1.Name);
        Assert.Equal(Option1Name, options2_1.Name);
    }
}
