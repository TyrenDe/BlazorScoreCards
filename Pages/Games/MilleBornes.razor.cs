using BlazorScoreCards.Client.Store.Games.MilleBornes;
using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Pages.Games;

public partial class MilleBornes
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IState<MilleBornesGameState> MilleBornesGameState { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task ResetAll()
    {
        var result = await DialogService.ShowMessageBox(
            "Reset Scores?",
            "Are you sure you want to reset the scores?",
            yesText: "Yes", cancelText: "Cancel");

        if (result.HasValue && result.Value)
        {
            Dispatcher.Dispatch(new ResetScoresAction());
        }
    }
}
