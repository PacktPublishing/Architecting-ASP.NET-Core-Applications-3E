#undef ACTIVATE_VALIDATION
using OptionsConfiguration;
using Microsoft.Extensions.Options;

const string NamedInstance = "MyNamedInstance";

var builder = WebApplication.CreateBuilder(args);
builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure Before")
);
builder.Services
    .AddSingleton<IPostConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    .Configure<ConfigureMeOptions>(builder.Configuration.GetSection("configureMe"))
    .Configure<ConfigureMeOptions>(NamedInstance, builder.Configuration.GetSection("configureMe"))
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureMoreConfigureMeOptions>()
;
builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure After")
);

#if ACTIVATE_VALIDATION
builder.Services.AddOptions<ConfigureMeOptions>().Validate(options =>
{
    // Validate was not intended for this, but it works nonetheless...
    options.Lines = options.Lines.Append("Inline Validate");
    return true;
});
#endif

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
