var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory<ContainerBuilder>(new ContainerBuilderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((context, builder) =>
{
    builder.Services.AddSingleton<Dependency1>();
    builder.Services.AddSingleton<Dependency2>();
    builder.Services.AddSingleton<Dependency3>();
});

var app = builder.Build();
app.MapGet("/", (HttpContext context) => new[] {
    $"https://{context.Request.Host}/Dependency1",
    $"https://{context.Request.Host}/Dependency2",
    $"https://{context.Request.Host}/Dependency3",
});
app.MapGet("/Dependency1", (Dependency1 dependency) => dependency.GetType().Name);
app.MapGet("/Dependency2", (Dependency2 dependency) => dependency.GetType().Name);
app.MapGet("/Dependency3", (Dependency3 dependency) => dependency.GetType().Name);

app.Run();

public class Dependency1 { }
public class Dependency2 { }
public class Dependency3 { }

// These are just wrappers to demonstrate the use of custom containers
public class ContainerBuilder
{
    public ContainerBuilder(IServiceCollection services)
    {
        Services = services;
    }
    public IServiceCollection Services { get; }
}
public class ContainerBuilderFactory : IServiceProviderFactory<ContainerBuilder>
{
    public ContainerBuilder CreateBuilder(IServiceCollection services)
        => new(services);

    public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        => containerBuilder.Services.BuildServiceProvider();
}
