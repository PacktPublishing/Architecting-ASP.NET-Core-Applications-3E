using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Minimal.API;

public static class MinimalEndpoints
{
    public static void AddMinimalEndpoints(this IServiceCollection services)
    {
        services.AddSingleton<SomeInternalService>();
    }

    public static void MapMinimalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("minimal-endpoint-inline", () => "GET!");
        app.MapGet("minimal-endpoint-method", MyMethod);
        app.MapGet(
            "minimal-endpoint-input-route-implicit/{id}",
            (int id) => $"The id was {id}."
        );
        app.MapGet(
            "minimal-endpoint-input-route-explicit/{id}",
            ([FromRoute] int id) => $"The id was {id}."
        );
        app.MapGet(
            "minimal-endpoint-input-service/{value}",
            (string value, SomeInternalService service)
                => service.Respond(value)
        );
        app.MapGet(
            "minimal-endpoint-input-HttpContext/",
            (HttpContext context)
                => context.Response.WriteAsync("HttpContext!")
        );
        app.MapGet(
            "minimal-endpoint-input-HttpResponse/",
            (HttpResponse response)
                => response.WriteAsync("HttpResponse!")
        );
        app.MapGet(
            "minimal-endpoint-input-Coordinate/",
            (Coordinate coordinate) => coordinate
        );

        // GET /minimal-endpoint-input-Person?name=John%20Doe&birthday=2023-06-14
        app.MapGet(
            "minimal-endpoint-input-Person/",
            (Person person) => person
        );
        // GET /minimal-endpoint-input-Person2?name=John%20Doe&birthday=2023-06-14
        app.MapGet(
            "minimal-endpoint-input-Person2/",
            ([AsParameters] Person2 person) => person
        );

        app.MapGet(
            "minimal-endpoint-output-Results/",
            () => Results.Ok()
        );
        app.MapGet(
            "minimal-endpoint-output-TypedResults/",
            () => TypedResults.Ok()
        );
        app.MapGet(
            "minimal-endpoint-output-TypedResults-multiple/",
            Results<Ok, Conflict> ()
                => Random.Shared.Next(0, 100) % 2 == 0
                    ? TypedResults.Ok()
                    : TypedResults.Conflict()
        );
        app.MapGet(
            "minimal-endpoint-output-TypedResults-multiple-delegate/",
            MultipleResultsDelegate
        );
        app.MapGet(
            "minimal-endpoint-output-coordinate/",
            () => new Coordinate
            {
                Latitude = 43.653225,
                Longitude = -79.383186
            }
        );
        app.MapGet(
            "minimal-endpoint-output-coordinate-ok1/",
            () => Results.Ok(new Coordinate {
                Latitude = 43.653225,
                Longitude = -79.383186
            })
        );
        app.MapGet(
            "minimal-endpoint-output-coordinate-ok2/",
            () => TypedResults.Ok(new Coordinate {
                Latitude = 43.653225,
                Longitude = -79.383186
            })
        );
        app.MapGet(
            "minimal-endpoint-output-LocalRedirect/",
            () => TypedResults.LocalRedirect("https://localhost:7298/minimal-endpoint-output-coordinate-ok2") // InvalidOperationException
            //() => TypedResults.LocalRedirect("/minimal-endpoint-output-coordinate-ok2") // Works
        );
        app.MapGet(
            "minimal-endpoint-output-Redirect/",
            () => TypedResults.Redirect("/minimal-endpoint-output-coordinate-ok2")
        );
        app.MapGet(
            "minimal-endpoint-output-Bytes/",
            () => TypedResults.Bytes(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })
        );
        app.MapGet(
            "minimal-endpoint-output-Content/",
            () => TypedResults.Content("Some text")
        );
        app.MapGet(
            "minimal-endpoint-output-Json/",
            () => TypedResults.Json(new Coordinate {
                Latitude = 43.653225,
                Longitude = -79.383186
            }, new JsonSerializerOptions(JsonSerializerOptions.Default) { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper })
        );
        app.MapGet(
            "minimal-endpoint-output-Stream/",
            () => TypedResults.Stream(Stream.Null)
        );
        app.MapGet(
            "minimal-endpoint-output-Text/",
            () => TypedResults.Text("Some raw text")
        );
        app.MapGet(
            "minimal-endpoint-output-Text-json/",
            () => TypedResults.Text("""{ "hello": "world" }""", contentType: "application/json", Encoding.UTF8) // C# 7 Raw string literals
        );
        app.MapGet(
            "minimal-endpoint-output-PhysicalFile/",
            (string? filePath, string? contentType) => TypedResults.PhysicalFile(
                filePath ?? "../Minimal.API.http",
                contentType: contentType ?? "text/plain"
            )
        );
        app.MapGet(
            "minimal-endpoint-output-VirtualFile/",
            (string? filePath, string? contentType) => TypedResults.VirtualFile(
                filePath ?? "minimal-virtual-file.txt",
                contentType: contentType ?? "text/plain"
            )
        );
        app.MapGet(
            "minimal-endpoint-output-VirtualFile-provider/",
            (IWebHostEnvironment webHostEnvironment)
                => webHostEnvironment.WebRootFileProvider.GetDirectoryContents("/").Select(fileInfo => fileInfo.Name)
        );
    }

    private static void MyMethod() { }

    private static Results<Ok, Conflict> MultipleResultsDelegate()
    {
        return Random.Shared.Next(0, 100) % 2 == 0
            ? TypedResults.Ok()
            : TypedResults.Conflict();
    }

    public class SomeInternalService
    {
        public string Respond(string value)
            => $"The value was {value}";
    }

    public class Coordinate : IParsable<Coordinate>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool TryParse(
            [NotNullWhen(true)] string? s,
            IFormatProvider? provider,
            [MaybeNullWhen(false)] out Coordinate result)
        {
            var segments = s?.Split(
                ',',
                StringSplitOptions.TrimEntries |
                StringSplitOptions.RemoveEmptyEntries
            );
            if (segments?.Length == 2)
            {
                var validLatitude = double.TryParse(
                    segments[0],
                    out var latitude
                );
                var validLongitude = double.TryParse(
                    segments[1],
                    out var longitude
                );
                if (validLatitude && validLongitude)
                {
                    result = new()
                    {
                        Latitude = latitude,
                        Longitude = longitude
                    };
                    return true;
                }
            }
            result = null;
            return false;
        }

        public static Coordinate Parse(string value, IFormatProvider? provider)
        {
            if (TryParse(value, provider, out var result))
            {
                return result;
            }
            throw new ArgumentException(
                "Cannot parse the value into a Coordinate.",
                nameof(value)
            );
        }
    }

    public class Person : IBindableFromHttpContext<Person>
    {
        public required string Name { get; set; }
        public required DateOnly Birthday { get; set; }

        public static ValueTask<Person?> BindAsync(
            HttpContext context,
            ParameterInfo parameter)
        {
            var name = context.Request.Query["name"].Single();
            if (name is not null &&
                DateOnly.TryParse(
                    context.Request.Query["birthday"],
                    out var birthday
                ))
            {
                var person = new Person()
                {
                    Name = name,
                    Birthday = birthday
                };
                return ValueTask.FromResult(person)!;
            }
            return ValueTask.FromResult(default(Person));
        }
    }

    public class Person2
    {
        public required string Name { get; set; }
        public required DateOnly Birthday { get; set; }
    }
}
