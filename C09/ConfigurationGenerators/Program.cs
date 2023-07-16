using ConfigurationGenerators;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOptions<MyOptions>("valid")
    .BindConfiguration("MyOptions")
    .ValidateOnStart()
;
builder.Services
    .AddOptions<MyOptions>("invalid")
    .BindConfiguration("MissingSection")
    // The following line makes the app fail to start
    // Comment it out to start the app.
    // However, the '/' endpoint will throw an exception because
    // the options will still fail the validation.
    // In any case, if that app runs, calling the '/valid' endpoint will work.
    .ValidateOnStart() 
;

builder.Services.AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidator>();

var app = builder.Build();

app.MapGet("/", (IOptionsFactory<MyOptions> factory) => new
{
    valid = factory.Create("valid"),
    invalid = factory.Create("invalid")
});
app.MapGet("/valid", (IOptionsFactory<MyOptions> factory) => factory.Create("valid"));

app.Run();
