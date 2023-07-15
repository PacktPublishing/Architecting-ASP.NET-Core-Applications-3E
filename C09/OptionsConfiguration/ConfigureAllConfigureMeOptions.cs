using Microsoft.Extensions.Options;

namespace OptionsConfiguration;

public class ConfigureAllConfigureMeOptions :
    IPostConfigureOptions<ConfigureMeOptions>,
    IConfigureNamedOptions<ConfigureMeOptions>
{
    private readonly ILoggerFactory _loggerFactory;
    public ConfigureAllConfigureMeOptions(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public void Configure(ConfigureMeOptions options) // should never be called
        => Configure(Options.DefaultName, options);

    public void Configure(string? name, ConfigureMeOptions options)
    {
        _loggerFactory
            .CreateLogger($"ConfigureAll name={name}")
            .LogInformation("Configure")
        ;
        options.Lines = options.Lines.Append($"ConfigureAll:Configure name: {name}");
        // You can check for the options name or use a switch here instead of
        // checking for "not the default" options.
        if (name != Options.DefaultName)
        {
            options.Lines = options.Lines.Append($"ConfigureAll:Configure Not Default: {name}");
        }
    }

    public void PostConfigure(string? name, ConfigureMeOptions options)
    {
        _loggerFactory
            .CreateLogger($"ConfigureAll name={name}")
            .LogInformation("PostConfigure")
        ;
        options.Lines = options.Lines.Append($"ConfigureAll:PostConfigure name: {name}");
    }
}
