//
// Look at the Container.cs and RawClient.cs files for more code
//
using CoreConcepts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISingleton, ObjectLifetime>();
builder.Services.AddScoped<IScoped, ObjectLifetime>();
builder.Services.AddTransient<ITransient, ObjectLifetime>();
builder.Services.AddTransient<ServiceConsumer>();

var app = builder.Build();
app.MapGet("/", (ServiceConsumer serviceConsumer1, ServiceConsumer serviceConsumer2) =>
{
    return TypedResults.Ok(new[] {
        serviceConsumer1,
        serviceConsumer2
    });
});
app.Run();
