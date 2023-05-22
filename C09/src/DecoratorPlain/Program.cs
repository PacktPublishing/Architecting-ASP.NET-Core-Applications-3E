using DecoratorPlain;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    // .AddSingleton<IComponent, ComponentA>()
    // .AddSingleton<IComponent>(serviceProvider => new DecoratorA(new ComponentA()))
    .AddSingleton<IComponent>(serviceProvider => new DecoratorB(new DecoratorA(new ComponentA())))
;

var app = builder.Build();
app.MapGet("/", (IComponent component) => component.Operation());
app.Run();
