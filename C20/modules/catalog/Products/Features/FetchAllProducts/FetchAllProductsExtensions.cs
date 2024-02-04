namespace Products.Features;

public static class FetchAllProductsExtensions
{

    public static IServiceCollection AddFetchAll(this IServiceCollection services)
    {
        return services
            .AddScoped<FetchAllProductsHandler>()
            .AddSingleton<FetchAllProductsMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchAll(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            (FetchAllProductsHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(new FetchAllProductsQuery(), cancellationToken)
        );
        return endpoints;
    }
}
