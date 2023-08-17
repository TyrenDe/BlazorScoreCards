using BlazorScoreCards.Client.Store;
using BlazorScoreCards.Client.Store.Orientation;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;

[assembly: SupportedOSPlatform("browser")]
namespace BlazorScoreCards;

public partial class App
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await JSHost.ImportAsync("App", "../App.razor.js");

        RegisterForOrientationChangedEvents(this);

        Dispatcher.Dispatch(new LoadInitialStateAction());
        Dispatcher.Dispatch(new SetOrientationAction((Orientations)GetOrientation()));

        base.OnInitialized();
    }

    [JSImport("getOrientation", "App")]
    internal static partial int GetOrientation();

    [JSImport("registerForOrientationChangedEvents", "App")]
    internal static partial void RegisterForOrientationChangedEvents([JSMarshalAs<JSType.Any>] object component);

    [JSExport]
    internal static void OrientationChanged([JSMarshalAs<JSType.Any>] object component, int orientation)
    {
        var app = (App)component;
        app.Dispatcher.Dispatch(new SetOrientationAction((Orientations)orientation));
    }

}
