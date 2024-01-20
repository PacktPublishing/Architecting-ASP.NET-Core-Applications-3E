namespace LoggingConsole;

public class ServiceUsingILogger(ILogger<ServiceUsingILogger> logger)
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public string Generate()
    {
        _logger.LogInformation("ServiceUsingILogger generating a GUID...");
        var guid = Guid.NewGuid();
        _logger.LogDebug("ServiceUsingILogger generated the GUID {guid}.", guid);
        return guid.ToString();
    }
}
