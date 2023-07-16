using ForEvolve.Testing.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

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
                services.AddSingleton<IService, Service>();
            })
            .Build();
        var service = host.Services.GetRequiredService<IService>();

        // Act
        service.Execute();

        // Assert
        Assert.Collection(lines,
            line => Assert.Equal("[info] Service.Execute()", line),
            line => Assert.Equal("[warning] Service.Execute()", line)
        );
    }

    public interface IService
    {
        void Execute();
    }

    public class Service : IService
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
