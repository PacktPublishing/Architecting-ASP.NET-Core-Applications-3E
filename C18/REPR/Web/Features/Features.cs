namespace Web.Features;

public static class Features
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        return services
            .AddProductsFeature()
            .AddBasketsFeature()
        ;
    }

    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/")
            .AddFluentValidationFilter();
        ;
        group
            .MapProductsFeature()
            .MapBasketsFeature()
        ;
        return endpoints;
    }

    public static async Task SeedFeaturesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await scope.SeedProductsAsync();
        await scope.SeedBasketAsync();
    }
}
