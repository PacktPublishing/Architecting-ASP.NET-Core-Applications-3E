using CommonScenarios;
using CommonScenarios.Reload;
using Microsoft.Extensions.Options;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Manual configuration
builder.Services.Configure<MyOptions>(myOptions =>
{
    myOptions.Name = "Default Option";
});

// Using the settings file
var defaultOptionsSection = builder.Configuration
    .GetSection("defaultOptions");
builder.Services
    .Configure<MyOptions>(defaultOptionsSection);

// Named options
builder.Services.Configure<MyOptions>(
    "Options1",
    builder.Configuration.GetSection("options1")
);
builder.Services.Configure<MyOptions>(
    "Options2",
    builder.Configuration.GetSection("options2")
);

// Reload
builder.AddNotificationService();

var app = builder.Build();

// Injecting options
app.MapGet(
    "/my-options/",
    (IOptions<MyOptions> options) => options.Value
);

// Named options
app.MapGet(
    "/factory/{name}",
    (string name, IOptionsFactory<MyOptions> factory)
        => factory.Create(name)
);
app.MapGet(
    "/monitor",
    (IOptionsMonitor<MyOptions> monitor)
        => monitor.CurrentValue
);
app.MapGet(
    "/monitor/{name}",
    (string name, IOptionsMonitor<MyOptions> monitor)
        => monitor.Get(name)
);
app.MapGet(
    "/snapshot",
    (IOptionsSnapshot<MyOptions> snapshot)
        => snapshot.Value
);
app.MapGet(
    "/snapshot/{name}",
    (string name, IOptionsSnapshot<MyOptions> snapshot)
        => snapshot.Get(name)
);

// Reload
app.MapNotificationService();

app.Run();

