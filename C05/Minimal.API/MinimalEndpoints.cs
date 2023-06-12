namespace Minimal.API;

public static class MinimalEndpoints
{
    public static void MapMinimalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("minimal-endpoint", () => "GET!");
    }
}
