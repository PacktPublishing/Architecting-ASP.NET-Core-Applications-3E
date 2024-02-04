namespace Baskets.Features;

public static class AddItemExtensions
{
    public static IServiceCollection AddAddItem(this IServiceCollection services)
    {
        return services
            .AddScoped<AddItemHandler>()
            .AddSingleton<AddItemMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapAddItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (AddItemCommand command, AddItemHandler handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return TypedResults.Created($"/products/{result.ProductId}", result);
            }
        );
        return endpoints;
    }
}
