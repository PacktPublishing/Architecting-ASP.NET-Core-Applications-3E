using CompositionRoot.DemoFeature;

namespace Microsoft.Extensions.DependencyInjection;

public static class DemoFeatureExtensions
{
    public static IServiceCollection AddDemoFeature(this IServiceCollection services)
    {
        return services
            .AddSingleton<MyFeature>()
            .AddSingleton<IMyFeatureDependency, MyFeatureDependency>()
        ;
    }
}
