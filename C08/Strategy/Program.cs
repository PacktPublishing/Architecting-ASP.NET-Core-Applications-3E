#define DEPLOY_TO_PROD
using Strategy.Data;
using Strategy.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

#if DEPLOY_TO_PROD
builder.Services.AddSingleton<IDatabase, NotImplementedDatabase>(); // The NotImplementedDatabase throws a NotImplementedException; injected into SqlLocationService.
builder.Services.AddSingleton<ILocationService, SqlLocationService>(); // Good for: InjectAbstractionLocationsController and InjectAbstractionUpdatedLocationsController
builder.Services.AddSingleton<SqlLocationService>(); // Good for: InjectImplementationUpdatedLocationsController
#else
builder.Services.AddSingleton<InMemoryLocationService>(); // Good for: InjectImplementationLocationsController
builder.Services.AddSingleton<ILocationService, InMemoryLocationService>(); // Good for: InjectAbstractionLocationsController and InjectAbstractionUpdatedLocationsController
#endif

var app = builder.Build();
app.MapControllers();
app.Run();
