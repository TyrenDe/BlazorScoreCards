﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Components.Games.SevenWonders;

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

    private async Task AddScore()
    {
        var parameters = new DialogParameters<AddScoreDialog>
        {
            { x => x.PlayerName, PlayerName }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };
        await DialogService.ShowAsync<AddScoreDialog>($"Score for {PlayerName}", parameters, options);
    }
}
