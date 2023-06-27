namespace Minimal.API;

public static class OrganizingEndpoints
{
    /// <summary>
    /// Create a group and map endpoints to it.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> to map endpoints to.</param>
    public static void MapOrganizingEndpoints(this IEndpointRouteBuilder app)
    {
        // Create a reusable logger
        var loggerFactory = app.ServiceProvider
            .GetRequiredService<ILoggerFactory>();
        var groupLogger = loggerFactory
            .CreateLogger("organizing-endpoints");

        // Create the group
        var group = app
            .MapGroup("organizing-endpoints")
            .WithTags("Organizing Endpoints")
            .AddEndpointFilter(async (context, next) => {
                groupLogger.LogTrace("Entering organizing-endpoints");
                if (context.Arguments.Count > 0 && groupLogger.IsEnabled(LogLevel.Debug)) {
                    for (var i = 0; i < context.Arguments.Count; i++)
                    {
                        var argument = context.GetArgument<object>(i);
                        groupLogger.LogDebug(
                            "Argument {i}: {type} = {value}",
                            i + 1,
                            argument.GetType().Name,
                            argument
                        );
                    }
                }
                var result = await next(context);
                groupLogger.LogTrace("Exiting organizing-endpoints");
                return result;
            })
        ;

        // Map endpoints and groups
        group.MapGet("demo/", ()
            => "GET endpoint from the organizing-endpoints group.");
        group.MapGet("demo/{id}", (int id)
            => $"GET {id} endpoint from the organizing-endpoints group.");
        group.MapGet("", () // URL: /organizing-endpoints
            => "GET endpoint from the organizing-endpoints group.");
    }

    /// <summary>
    /// Create a group and map endpoints to it before returning the <see cref="IEndpointRouteBuilder"/> to
    /// map more endpoints or groups from the caller fluently.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> to map endpoints to.</param>
    /// <returns>The original IEndpointRouteBuilder.</returns>
    public static IEndpointRouteBuilder MapOrganizingEndpointsFluently(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("organizing-endpoints-fluently")
            .WithTags("Organizing Fluent Endpoints")
        ;
        // Map endpoints and groups here
        return app;
    }

    /// <summary>
    /// Create a group and map endpoints to it before returning the <see cref="RouteGroupBuilder"/> itself,
    /// allowing the caller to customize the group further and add subgroups.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> to map endpoints to.</param>
    /// <returns>The newly created RouteGroupBuilder instance.</returns>
    public static RouteGroupBuilder MapOrganizingEndpointsComposable(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("organizing-endpoints-composable")
            .WithTags("Organizing Composable Endpoints")
        ;
        // Map endpoints and groups here
        return group;
    }
}
