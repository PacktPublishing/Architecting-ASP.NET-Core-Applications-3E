using CentralizingConfiguration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IConfigureOptions<ProxyOptions>, ProxyOptions>()
    .AddSingleton<IValidateOptions<ProxyOptions>, ProxyOptions>()
    .AddSingleton(sp => sp.GetRequiredService<IOptions<ProxyOptions>>().Value)
    .Configure<ProxyOptions>(options => options.Name = "High-speed proxy")
    .AddOptions<ProxyOptions>()
    .ValidateOnStart()
;

var app = builder.Build();

app.MapGet("/", (ProxyOptions options) => options);

app.Run();
