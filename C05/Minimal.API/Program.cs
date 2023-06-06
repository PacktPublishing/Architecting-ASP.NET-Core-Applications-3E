using Shared;
using Minimal.API;

var builder = WebApplication.CreateBuilder(args);
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

app.InitializeSharedDataStore();

app.Run();
