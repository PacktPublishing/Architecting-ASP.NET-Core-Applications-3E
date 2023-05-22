var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Dependency1>();
builder.Services.AddSingleton<Dependency2>();
builder.Services.AddSingleton<Dependency3>();
builder.Services.AddDemoFeature();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();

public class Dependency1 { }
public class Dependency2 { }
public class Dependency3
{
    public Dependency3(Dependency1 dependency1, Dependency2 dependency2)
    {
        Dependency1 = dependency1;
        Dependency2 = dependency2;
    }

    public Dependency1 Dependency1 { get; }
    public Dependency2 Dependency2 { get; }
}