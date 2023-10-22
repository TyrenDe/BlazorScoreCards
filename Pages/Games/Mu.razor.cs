using BlazorScoreCards.Client.Store.Games.Mu;
using BlazorScoreCards.Client.Store.Players;
using BlazorScoreCards.Components.Games.Mu;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Pages.Games;

public partial class Mu
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IState<MuGameState> MuGameState { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task ScoreHand()
    {
        var parameters = new DialogParameters<AddScoreDialog>();
        var options = new DialogOptions { CloseOnEscapeKey = true };
        await DialogService.ShowAsync<AddScoreDialog>("Score Hand", parameters, options);
    }

    private async Task Stalemate()
    {
        var parameters = new DialogParameters<AddScoreDialog>();
        var options = new DialogOptions { CloseOnEscapeKey = true };
        await DialogService.ShowAsync<StalemateDialog>("Score Stalemate", parameters, options);
    }

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
