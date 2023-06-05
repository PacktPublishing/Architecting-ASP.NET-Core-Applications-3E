using Shared;
using Minimal.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCustomerEndpoints();
app.InitializeSharedDataStore();
app.Run();
