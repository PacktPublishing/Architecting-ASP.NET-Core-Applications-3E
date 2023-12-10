using ForEvolve.Testing.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Logging;

public class Filtering
{
    private readonly ITestOutputHelper _output;
    public Filtering(ITestOutputHelper output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Fact]
    public void Should_filter_logs_by_provider()
    {
        // Arrange
        var lines = new List<string>();
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.AddAssertableLogger(lines);
                loggingBuilder.AddxUnitTestOutput(_output);
                loggingBuilder.AddFilter<XunitTestOutputLoggerProvider>(
                    level => level >= LogLevel.Warning
                );
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<Service>();
            })
            .Build();
        var service = host.Services.GetRequiredService<Service>();

        // Act
        service.Execute();

        // Assert that AssertableLogger received the two entries
        Assert.Collection(lines,
            line => Assert.Equal("[info] Service.Execute()", line),
            line => Assert.Equal("[warning] Service.Execute()", line)
        );

        // Assert that XunitTestOutputLogger received only the warning
        var testOutputHelper = Assert.IsType<TestOutputHelper>(_output);
        Assert.Equal(
            "[warning] Service.Execute()",
            testOutputHelper.Output.TrimEnd()
        );
    }

    public class Service
    {
        private readonly ILogger _logger;
        public Service(ILogger<Service> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            _logger.LogInformation("[info] Service.Execute()");
            _logger.LogWarning("[warning] Service.Execute()");
        }
    }
}
