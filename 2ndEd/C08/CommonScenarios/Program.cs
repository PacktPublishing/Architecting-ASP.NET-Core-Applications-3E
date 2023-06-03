using CommonScenarios;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MyOptions>("Options1", builder.Configuration.GetSection("options1"));
builder.Services.Configure<MyOptions>("Options2", builder.Configuration.GetSection("options2"));
builder.Services.Configure<MyDoubleNameOptions>(builder.Configuration.GetSection("myDoubleNameOptions"));
builder.Services.AddTransient<MyNameServiceUsingDoubleNameOptions>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsFactory>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsMonitor>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsSnapshot>();

var app = builder.Build();
app.MapGet("/", (HttpContext context) => new[] {
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/options/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/options/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/factory/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/factory/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/monitor/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/monitor/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/snapshot/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/snapshot/false" },
});
app.MapGet("/options/{someCondition}", (bool someCondition, MyNameServiceUsingDoubleNameOptions service)
    => new { name = service.GetName(someCondition) });
app.MapGet("/factory/{someCondition}", (bool someCondition, MyNameServiceUsingNamedOptionsFactory service)
    => new { name = service.GetName(someCondition) });
app.MapGet("/monitor/{someCondition}", (bool someCondition, MyNameServiceUsingNamedOptionsMonitor service)
    => new { name = service.GetName(someCondition) });
app.MapGet("/snapshot/{someCondition}", (bool someCondition, MyNameServiceUsingNamedOptionsSnapshot service)
    => new { name = service.GetName(someCondition) });
app.Run();

