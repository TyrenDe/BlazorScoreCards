using BlazorScoreCards.Client.Store.Games.Generic;
using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Pages.Games;

public partial class Generic
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IState<GenericGameState> GenericGameState { get; set; } = default!;

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
