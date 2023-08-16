using BlazorScoreCards.Client.Store;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards;

public partial class App
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    protected override void OnInitialized()
    {
        Dispatcher.Dispatch(new LoadInitialStateAction());

        base.OnInitialized();
    }
}
