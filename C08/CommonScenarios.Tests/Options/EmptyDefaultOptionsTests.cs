using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonScenarios.Options;

public class EmptyDefaultOptionsTests
{
    private readonly IServiceProvider _serviceProvider;

    public EmptyDefaultOptionsTests()
    {
        var services = new ServiceCollection();
        services.AddOptions();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void OptionsSnapshot()
    {
        var snapshot = _serviceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();
        Assert.NotNull(snapshot.Value);
    }

    [Fact]
    public void OptionsMonitor()
    {
        var monitor = _serviceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
        Assert.NotNull(monitor.CurrentValue);
    }

    [Fact]
    public void OptionsFactory()
    {
        var defaultName = Microsoft.Extensions.Options.Options.DefaultName;
        var factory = _serviceProvider.GetRequiredService<IOptionsFactory<MyOptions>>();

        var options = factory.Create(defaultName);
        Assert.NotNull(options);
    }

    [Fact]
    public void Options()
    {
        var options = _serviceProvider.GetRequiredService<IOptions<MyOptions>>();
        Assert.NotNull(options.Value);
    }
}
