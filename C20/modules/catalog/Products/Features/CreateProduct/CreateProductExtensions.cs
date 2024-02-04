namespace Products.Features;

public static class CreateProductExtensions
{
    public static IServiceCollection AddCreateProduct(this IServiceCollection services)
    {
        return services
            .AddScoped<CreateProductHandler>()
            .AddSingleton<CreateProductMapper>()
        ;
    }

    public static IEndpointRouteBuilder MapCreateProduct(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (CreateProductCommand query, CreateProductHandler handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(query, cancellationToken);
                return TypedResults.Created($"/products/{result.Id}", result);
            }
        );
        return endpoints;
    }
}
