using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Logging;

public class Providers
{
    private readonly ITestOutputHelper _output;
    public Providers(ITestOutputHelper output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Fact]
    public async Task CustomizeProvidersAsync()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.AddxUnitTestOutput(_output);
            })
            .Build();
        var tokenSource = new CancellationTokenSource(1000);
        await host.RunAsync(tokenSource.Token);
    }
}
