using Blazored.LocalStorage;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorScoreCards;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        var currentAssembly = typeof(Program).Assembly;
        builder.Services.AddFluxor(options =>
        {
            options.ScanAssemblies(currentAssembly);
#if DEBUG
            options.UseReduxDevTools(rdt =>
            {
                rdt.Name = "Blazor Score Cards";
                rdt.EnableStackTrace();
            });
#endif
        });
        
        builder.Services.AddMudServices();
        builder.Services.AddBlazoredLocalStorage();

        await builder.Build().RunAsync();
    }
}
