using Microsoft.AspNetCore.Builder;
using Shared.Data;

namespace Shared;
public static  class StartupExtensions
{
    public static IApplicationBuilder InitializeSharedDataStore(this IApplicationBuilder app)
    {
        MemoryDataStore.Seed();
        return app;
    }
}
