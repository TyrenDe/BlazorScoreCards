using BlazorScoreCards.Client.Store.Games.Split;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Pages.Games;

public partial class Split
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
