using Web.Features;

var builder = WebApplication.CreateBuilder(args);
builder.AddExceptionMapper();
builder.AddFluentValidationEndpointFilter();
builder.Services.AddFeatures();

var app = builder.Build();
app.UseExceptionMapper();
app.MapFeatures();

await app.SeedFeaturesAsync();

app.Run();
