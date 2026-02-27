using BlazorScoreCards.Client.Store.Games.Split;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Pages.Games;

public partial class Split
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task ResetAll()
    {
        var result = await DialogService.ShowMessageBoxAsync(
            "Reset Scores?",
            "Are you sure you want to reset the scores?",
            yesText: "Yes", cancelText: "Cancel");

        if (result.HasValue && result.Value)
        {
            Dispatcher.Dispatch(new ResetScoresAction());
        }
    }
}
