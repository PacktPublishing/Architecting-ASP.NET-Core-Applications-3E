using CommonScenarios;
using CommonScenarios.Reload;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MyOptions>(myOptions =>
{
    myOptions.Name = "Default Option";
});
var defaultOptionsSection = builder.Configuration
    .GetSection("defaultOptions");
builder.Services
    .Configure<MyOptions>(defaultOptionsSection);


builder.AddNotificationService();
builder.Services.Configure<MyOptions>("Options1", builder.Configuration.GetSection("options1"));
builder.Services.Configure<MyOptions>("Options2", builder.Configuration.GetSection("options2"));
builder.Services.Configure<MyDoubleNameOptions>(builder.Configuration.GetSection("myDoubleNameOptions"));
builder.Services.AddTransient<MyNameServiceUsingDoubleNameOptions>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsFactory>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsMonitor>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsSnapshot>();

var app = builder.Build();
app.MapNotificationService();
app.MapGet(
    "/my-options/",
    (IOptions<MyOptions> options) => options.Value
);
app.MapGet("/options/{firstOption}", (bool firstOption, MyNameServiceUsingDoubleNameOptions service)
    => new { name = service.GetName(firstOption) });
app.MapGet("/factory/{firstOption}", (bool firstOption, MyNameServiceUsingNamedOptionsFactory service)
    => new { name = service.GetName(firstOption) });
app.MapGet("/monitor/{firstOption}", (bool firstOption, MyNameServiceUsingNamedOptionsMonitor service)
    => new { name = service.GetName(firstOption) });
app.MapGet("/snapshot/{firstOption}", (bool firstOption, MyNameServiceUsingNamedOptionsSnapshot service)
    => new { name = service.GetName(firstOption) });
app.Run();

