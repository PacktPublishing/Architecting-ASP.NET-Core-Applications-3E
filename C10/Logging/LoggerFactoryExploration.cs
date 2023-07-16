using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Logging;

public class LoggerFactoryExploration
{
    private readonly ITestOutputHelper _output;
    public LoggerFactoryExploration(ITestOutputHelper output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Fact]
    public void Create_a_ILoggerFactory()
    {
        // Arrange
        var lines = new List<string>();
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging(loggingBuilder => loggingBuilder
                .AddAssertableLogger(lines)
                .AddxUnitTestOutput(_output))
            .ConfigureServices(services => services.AddSingleton<Service>())
            .Build()
        ;
        var service = host.Services.GetRequiredService<Service>();

        // Act
        service.Execute();

        // Assert
        Assert.Collection(lines,
            line => Assert.Equal("LogInformation like any ILogger<T>.", line)
        );
    }

    public class Service
    {
        private readonly ILogger _logger;
        public Service(ILoggerFactory loggerFactory)
        {
            ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger("My Service");
        }

        public void Execute()
        {
            _logger.LogInformation("LogInformation like any ILogger<T>.");
        }
    }
}
