namespace REPR.Products.Features;

public static class FetchOneProductExtensions
{
    public static IServiceCollection AddFetchOneProduct(this IServiceCollection services)
    {
        return services
            .AddScoped<FetchOneProductHandler>()
            .AddSingleton<FetchOneProductMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapFetchOneProduct(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{ProductId}",
            ([AsParameters] FetchOneProductQuery query, FetchOneProductHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
