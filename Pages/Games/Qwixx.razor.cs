using BlazorScoreCards.Client.Store.Games.Qwixx;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorScoreCards.Pages.Games;

public partial class Qwixx
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await JSRuntime.InvokeVoidAsync("setOrientation", "landscape");
        await base.OnInitializedAsync();
    }

    protected override void Dispose(bool disposing)
    {
        JSRuntime.InvokeVoidAsync("setOrientation", "any");
        base.Dispose(disposing);
    }

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
