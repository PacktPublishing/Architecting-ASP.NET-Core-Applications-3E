namespace Baskets.Features;

public static class RemoveItemExtensions
{

    public static IServiceCollection AddRemoveItem(this IServiceCollection services)
    {
        return services
            .AddScoped<RemoveItemHandler>()
            .AddSingleton<RemoveItemMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapRemoveItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{customerId}/{productId}",
            ([AsParameters] RemoveItemCommand command, RemoveItemHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(command, cancellationToken)
        );
        return endpoints;
    }
}
