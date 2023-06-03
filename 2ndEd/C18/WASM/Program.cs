using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StateR;
using StateR.Blazor.ReduxDevTools;

namespace WASM;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        // Register StateR
        builder.Services
            .AddStateR(typeof(Program).Assembly)
            .AddReduxDevTools()
            .Apply()
        ;
        await builder.Build().RunAsync();
    }
}
