using Shared;
using Minimal.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSharedServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDarkSwaggerUI();
}
app.MapCustomerEndpoints();
app.InitializeSharedDataStore();
app.Run();
