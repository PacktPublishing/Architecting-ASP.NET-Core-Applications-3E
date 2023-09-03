using REPR.Baskets;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.AddExceptionMapper();
builder.AddBasketModule();
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });

    x.AddBasketModuleConsumers();
});
var app = builder.Build();
app.UseExceptionMapper();
app.MapBasketModule();

//app.MapGet("/", () => "Hello World!");

app.Run();
