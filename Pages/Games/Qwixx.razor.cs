using BlazorScoreCards.Client.Store.Games.Qwixx;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Pages.Games;

public partial class Qwixx
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
