using Shared;
using Minimal.API;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//
// Uncomment to enable KebabCaseLower globally, for all endpoints.
//
//builder.Services.ConfigureHttpJsonOptions(options => {
//    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
//});
builder.Services.AddMinimalEndpoints();
builder.Services.AddCustomerRepository();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDarkSwaggerUI();
}

app.MapCustomerEndpoints();
app.MapCustomerDtoEndpoints();
app.MapMinimalEndpoints();

app.InitializeSharedDataStore();

app.Run();
