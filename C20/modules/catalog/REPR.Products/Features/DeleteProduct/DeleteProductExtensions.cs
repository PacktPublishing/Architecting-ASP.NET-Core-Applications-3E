namespace REPR.Products.Features;

public static class DeleteProductExtensions
{

    public static IServiceCollection AddDeleteProduct(this IServiceCollection services)
    {
        return services
            .AddScoped<DeleteProductHandler>()
            .AddSingleton<DeleteProductMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapDeleteProduct(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{ProductId}",
            ([AsParameters] DeleteProductCommand query, DeleteProductHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
