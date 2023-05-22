using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonScenarios.Options;

public class OptionsFactoryTests
{
    public const string Option1Name = "Options 1";
    private readonly IServiceProvider _serviceProvider;

    public OptionsFactoryTests()
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
        var factory1 = _serviceProvider.GetRequiredService<IOptionsFactory<MyOptions>>();
        var factory2 = _serviceProvider.GetRequiredService<IOptionsFactory<MyOptions>>();
        Assert.NotSame(factory1, factory2);

        var options1_1 = factory1.Create("Options1");
        var options1_2 = factory1.Create("Options1");
        var options2_1 = factory2.Create("Options1");
        var options2_2 = factory2.Create("Options1");

        Assert.NotSame(options1_1, options1_2);
        Assert.NotSame(options1_2, options2_1);
        Assert.NotSame(options2_1, options2_2);

        Assert.Equal(Option1Name, options1_1.Name);
    }

    [Fact]
    public void TestScope()
    {
        using var scope1 = _serviceProvider.CreateScope();
        using var scope2 = _serviceProvider.CreateScope();
        var factory1 = scope1.ServiceProvider.GetRequiredService<IOptionsFactory<MyOptions>>();
        var factory2 = scope2.ServiceProvider.GetRequiredService<IOptionsFactory<MyOptions>>();
        Assert.NotSame(factory1, factory2);

        var options1_1 = factory1.Create("Options1");
        var options1_2 = factory1.Create("Options1");
        var options2_1 = factory2.Create("Options1");
        var options2_2 = factory2.Create("Options1");

        Assert.NotSame(options1_1, options1_2);
        Assert.NotSame(options1_2, options2_1);
        Assert.NotSame(options2_1, options2_2);

        Assert.Equal(Option1Name, options1_1.Name);
        Assert.Equal(Option1Name, options2_1.Name);
    }
}
