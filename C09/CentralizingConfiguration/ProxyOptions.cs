using Microsoft.Extensions.Options;

namespace CentralizingConfiguration;

public class ProxyOptions : IConfigureOptions<ProxyOptions>, IValidateOptions<ProxyOptions>
{
    public static readonly int DefaultCacheTimeInSeconds = 60;

    public string? Name { get; set; }
    public int CacheTimeInSeconds { get; set; }

    void IConfigureOptions<ProxyOptions>.Configure(ProxyOptions options)
    {
        options.CacheTimeInSeconds = DefaultCacheTimeInSeconds;
    }

    ValidateOptionsResult IValidateOptions<ProxyOptions>.Validate(string? name, ProxyOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Name))
        {
            return ValidateOptionsResult.Fail("The 'Name' property is required.");
        }
        return ValidateOptionsResult.Success;
    }
}
