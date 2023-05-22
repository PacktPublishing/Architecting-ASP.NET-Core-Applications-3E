//#define USE_MEMORY_CACHE

using ApplicationState;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
#if USE_MEMORY_CACHE
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IApplicationState, ApplicationMemoryCache>();
#else
        builder.Services.AddSingleton<IApplicationState, ApplicationDictionary>();
#endif
var app = builder.Build();
app.MapGet("/", (IApplicationState myAppState, string key) =>
{
    var value = myAppState.Get<string>(key);
    return $"{key} = {value ?? "null"}";
});
app.MapPost("/", (IApplicationState myAppState, SetAppState dto) =>
{
    myAppState.Set(dto.Key, dto.Value);
    return $"{dto.Key} = {dto.Value}";
});

app.Run();

public record class SetAppState(string Key, string Value);