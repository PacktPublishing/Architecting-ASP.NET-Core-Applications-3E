#define FILTER_BY_CATEGORY
using LoggingConsole;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

#if FILTER_BY_CATEGORY
builder.Logging.AddFilter<ConsoleLoggerProvider>(
    ServiceUsingLoggerFactory.CategoryName,
    level => level >= LogLevel.Debug
);
#else
builder.Logging.AddFilter<ConsoleLoggerProvider>(
    level => level >= LogLevel.Debug
);
#endif

builder.Services
    .AddSingleton<ServiceUsingILogger>()
    .AddSingleton<ServiceUsingLoggerFactory>()
;

var app = builder.Build();

app.MapGet("/", (ServiceUsingILogger service) => service.Generate());
app.MapGet("/factory", (ServiceUsingLoggerFactory service) => service.Generate());

app.Run();
