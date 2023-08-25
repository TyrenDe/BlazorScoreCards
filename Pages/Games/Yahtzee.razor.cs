using BlazorScoreCards.Client.Store.Games.Yahtzee;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Pages.Games;

public partial class Yahtzee
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
