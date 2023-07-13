var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddJsonConsole();

var app = builder.Build();

app.MapGet("/", (ILoggerFactory loggerFactory) =>
{
    const string category = "root";
    var logger = loggerFactory.CreateLogger(category);
    logger.LogInformation("You hit the {category} URL!", category);
    return "Hello World!";
});

app.Run();
