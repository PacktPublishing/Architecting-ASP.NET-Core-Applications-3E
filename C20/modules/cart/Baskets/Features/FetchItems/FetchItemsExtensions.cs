namespace Baskets.Features;

public static class FetchItemsExtensions
{
    public static IServiceCollection AddFetchItems(this IServiceCollection services)
    {
        return services
            .AddScoped<FetchItemsHandler>()
            .AddSingleton<FetchItemsMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchItems(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{CustomerId}",
            ([AsParameters] FetchItemsQuery query, FetchItemsHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
