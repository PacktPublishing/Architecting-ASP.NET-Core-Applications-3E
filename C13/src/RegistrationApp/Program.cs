using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using RegistrationApp;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ConcertRegistrationService>();
builder.Services.Configure<JsonOptions>(o => {
    o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapPost("/concerts/{concertId}/register",
    async Task<Results<Ok<ConcertRegistrationResult>, BadRequest<ConcertRegistrationResult>>> (int concertId, ConcertRegistrationService service) =>
{
    // Simulate fetching objects
    var user = GetCurrentUser();
    var concert = GetConcert(concertId);

    // Execute the operation
    var result = await service.RegisterAsync(user, concert);

    // Handle the operation result
    if (result.RegistrationSucceeded)
    {
        return TypedResults.Ok(result);
    }
    else
    {
        await LogErrorMessageAsync(result.ErrorMessage); // Showcases the usefulness of the MemberNotNullWhen attributes
        return TypedResults.BadRequest(result);
    }
});

app.Run();

// Helper methods to simulate interacting with a complex system
static Concert GetConcert(int concertId) => new(concertId, $"Some amazing concert—Part {concertId}");
static User GetCurrentUser() => new("John Doe");
static Task LogErrorMessageAsync(string message) => Task.CompletedTask;