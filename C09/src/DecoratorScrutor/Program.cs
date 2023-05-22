using DecoratorScrutor;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IComponent, ComponentA>()
    .Decorate<IComponent, DecoratorA>()
    .Decorate<IComponent, DecoratorB>()
;

var app = builder.Build();
app.MapGet("/", (IComponent component) => component.Operation());
app.Run();
