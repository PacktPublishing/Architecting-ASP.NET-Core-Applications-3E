using Minimal.API;
using Shared;

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
    //app.UseSwaggerUI(); // <-- Light (default) version of Swagger UI
}

app.MapGet("most-basic-delegate", () => { });

app.MapCustomerEndpoints();
app.MapCustomerDtoEndpoints();
app.MapMinimalEndpoints();
app
    .MapOrganizingEndpointsFluently()
    .MapOrganizingEndpoints()
;

app.InitializeSharedDataStore();

app.Run();
