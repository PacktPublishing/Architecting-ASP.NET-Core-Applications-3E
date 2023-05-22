using OptionsConfiguration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .Configure<ConfigureMeOptions>(builder.Configuration.GetSection("configureMe"))
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure1ConfigureMeOptions>()
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure2ConfigureMeOptions>()
;
builder.Services.Configure<ConfigureMeOptions>(options
    => options.Lines = options.Lines.Append("Another Configure call"));
builder.Services.PostConfigure<ConfigureMeOptions>(options
    => options.Lines = options.Lines.Append("What about PostConfigure?"));
builder.Services.PostConfigureAll<ConfigureMeOptions>(options
    => options.Lines = options.Lines.Append("Did you forgot about PostConfigureAll?"));
builder.Services.ConfigureAll<ConfigureMeOptions>(options
    => options.Lines = options.Lines.Append("Or ConfigureAll?"));
builder.Services.AddOptions<ConfigureMeOptions>().Validate(options =>
{
    options.Lines = options.Lines.Append("Validate was not intended for this, but this is a trace isn't it?");
    return true;
});

var app = builder.Build();
app.MapGet("/", (HttpContext context) => new[] { $"https://{context.Request.Host}/configure-me" });
app.MapGet("/configure-me", (IOptionsMonitor<ConfigureMeOptions> options) => options);
app.Run();

public class Configure1ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
{
    public void Configure(ConfigureMeOptions options)
    {
        options.Lines = options.Lines.Append("Added line 1!");
    }
}

public class Configure2ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
{
    public void Configure(ConfigureMeOptions options)
    {
        options.Lines = options.Lines.Append("Added line 2!");
    }
}
