using BlazorScoreCards.Client.Store.Games.MilleBornes;
using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Pages.Games;

public partial class MilleBornes
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IState<MilleBornesGameState> MilleBornesGameState { get; set; } = default!;

    private void ResetAll()
    {
        Dispatcher.Dispatch(new ResetScoresAction());
    }
}
