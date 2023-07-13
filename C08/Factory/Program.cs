using Factory.Data;
using Factory.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<ILocationService>(sp =>
{
    if (builder.Environment.IsDevelopment())
    {
        return new InMemoryLocationService();
    }
    return new SqlLocationService(new NotImplementedDatabase());
});

var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => TypedResults.Redirect("/travel/locations"));
app.Run();
