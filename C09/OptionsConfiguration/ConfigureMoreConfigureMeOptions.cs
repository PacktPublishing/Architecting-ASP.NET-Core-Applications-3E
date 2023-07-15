using Microsoft.Extensions.Options;

namespace OptionsConfiguration;

public class ConfigureMoreConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
{
    private readonly ILoggerFactory _loggerFactory;
    public ConfigureMoreConfigureMeOptions(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public void Configure(ConfigureMeOptions options)
    {
        _loggerFactory
            .CreateLogger("ConfigureMore")
            .LogInformation("Configure")
        ;
        options.Lines = options.Lines.Append("ConfigureMore:Configure");
    }
}
