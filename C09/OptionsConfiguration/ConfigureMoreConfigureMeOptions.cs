using Microsoft.Extensions.Options;

namespace OptionsConfiguration;

public class ConfigureMoreConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
{
    public void Configure(ConfigureMeOptions options)
    {
        options.Lines = options.Lines.Append("ConfigureMore:Configure");
    }
}
