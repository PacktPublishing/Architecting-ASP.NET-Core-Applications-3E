using OptionsConfiguration;
using Microsoft.Extensions.Options;

const string NamedInstance = "MyNamedInstance";

var builder = WebApplication.CreateBuilder(args);
builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure Before")
);
builder.Services
    .Configure<ConfigureMeOptions>(builder.Configuration.GetSection("configureMe"))
    .Configure<ConfigureMeOptions>(NamedInstance, builder.Configuration.GetSection("configureMe"))
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    //.AddSingleton<IConfigureNamedOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    .AddSingleton<IPostConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureMoreConfigureMeOptions>()
;
builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure After")
);

builder.Services.AddOptions<ConfigureMeOptions>().Validate(options =>
{
    // Validate was not intended for this, but it works nonetheless...
    options.Lines = options.Lines.Append("Inline Validate");
    return true;
});


var app = builder.Build();
app.MapGet("/", () => Results.Redirect("/configure-me"));
app.MapGet(
    "/configure-me",
    (IOptionsMonitor<ConfigureMeOptions> options) => new {
        DefaultInstance = options.CurrentValue,
        NamedInstance = options.Get(NamedInstance)
    }
);
app.Run();

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


public class ConfigureAllConfigureMeOptions :
    IPostConfigureOptions<ConfigureMeOptions>,
    IConfigureNamedOptions<ConfigureMeOptions>
{
    private readonly ILoggerFactory _loggerFactory;
    public ConfigureAllConfigureMeOptions(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }


    public void Configure(ConfigureMeOptions options)
    {
        _loggerFactory
            .CreateLogger("ConfigureAll")
            .LogWarning("Configure")
        ;
        options.Lines = options.Lines.Append("ConfigureAll:Configure");
    }

    public void Configure(string? name, ConfigureMeOptions options)
    {
        _loggerFactory
            .CreateLogger($"ConfigureAll name={name}")
            .LogInformation("Configure")
        ;
        options.Lines = options.Lines.Append($"ConfigureAll:Configure name: {name}");
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
