var builder = WebApplication.CreateBuilder(args);
builder.AddExceptionMapper();


var app = builder.Build();
app.UseExceptionMapper();

app.MapGet("/", () => "Hello World!");

app.Run();
