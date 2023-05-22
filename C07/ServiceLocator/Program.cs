using ServiceLocator;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IMyService, MyServiceImplementation>()
    .AddControllers()
;
var app = builder.Build();
app.MapControllers();
app.MapGet("/", (HttpContext context) => new[] {
    $"https://{context.Request.Host}/service-locator",
    $"https://{context.Request.Host}/method-injection",
    $"https://{context.Request.Host}/constructor-injection",
    $"https://{context.Request.Host}/minimal-api",
});
app.MapGet("/minimal-api", (IMyService myService) =>
{
    myService.Execute();
    return "Success!";
});
app.Run();
