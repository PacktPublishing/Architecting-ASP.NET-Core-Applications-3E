namespace LoggingConsole;

public class ServiceUsingLoggerFactory
{
    public const string CategoryName = "My Service";
    private readonly ILogger _logger;
    public ServiceUsingLoggerFactory(ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
        _logger = loggerFactory.CreateLogger(CategoryName);
    }

    public string Generate()
    {
        _logger.LogInformation("ServiceUsingLoggerFactory generating a GUID...");
        var guid = Guid.NewGuid();
        _logger.LogDebug("ServiceUsingLoggerFactory generated the GUID {guid}.", guid);
        return guid.ToString();
    }
}