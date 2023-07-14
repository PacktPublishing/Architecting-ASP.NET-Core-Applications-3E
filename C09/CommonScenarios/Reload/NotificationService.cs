using Microsoft.Extensions.Options;

namespace CommonScenarios.Reload;

public class NotificationService
{
    private EmailOptions _emailOptions;
    private readonly ILogger _logger;

    public NotificationService(IOptionsMonitor<EmailOptions> emailOptions, ILogger<NotificationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        ArgumentNullException.ThrowIfNull(emailOptions);
        emailOptions.OnChange(options => _emailOptions = options);
        _emailOptions = emailOptions.CurrentValue;
    }

    public Task NotifyAsync(string to)
    {
        _logger.LogInformation("Notification sent by '{SenderEmailAddress}' to '{to}'.", _emailOptions.SenderEmailAddress, to);
        return Task.CompletedTask;
    }
}

public class EmailOptions
{
    public string? SenderEmailAddress { get; set; }
}

public static class NotificationServiceStartupExtensions
{
    public static WebApplicationBuilder AddNotificationService(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
        builder.Services.AddSingleton<NotificationService>();
        return builder;
    }

    public static WebApplication MapNotificationService(this WebApplication app)
    {
        app.MapPost("notify/{email}", async (string email, NotificationService service) =>
        {
            await service.NotifyAsync(email);
            return Results.Ok();
        });
        return app;
    }
}