using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Components.Games.MilleBornes;

public partial class ScoreCard
{
    [EditorRequired]
    [Parameter]
    public string PlayerName { get; set; } = string.Empty;

    [EditorRequired]
    [Parameter]
    public int Score { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private bool DisableButtons = false;

    private async Task AddScore()
    {
        DisableButtons = true;
        var parameters = new DialogParameters<AddScoreDialog>
        {
            { x => x.PlayerName, PlayerName }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
        await DialogService.ShowAsync<AddScoreDialog>($"Adjust Score for {PlayerName}", parameters, options);

        DisableButtons = false ;
    }
}
