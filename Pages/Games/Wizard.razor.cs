using BlazorScoreCards.Client.Store.Games.Wizard;
using BlazorScoreCards.Client.Store.Players;
using BlazorScoreCards.Components.Games.Wizard;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorScoreCards.Pages.Games;

public partial class Wizard
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IState<WizardGameState> WizardState { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task ResetAll()
    {
        var result = await DialogService.ShowMessageBoxAsync(
            "Reset Scores?",
            "Are you sure you want to reset all Wizard scores?",
            yesText: "Yes", cancelText: "Cancel");

        if (result.HasValue && result.Value)
        {
            Dispatcher.Dispatch(new ResetScoresAction());
        }
    }

    private async Task UndoLastHand()
    {
        var result = await DialogService.ShowMessageBoxAsync(
            "Undo Last Hand?",
            "Are you sure you want to undo the last scored hand?",
            yesText: "Yes", cancelText: "Cancel");

        if (result.HasValue && result.Value)
        {
            Dispatcher.Dispatch(new UndoLastHandAction());
        }
    }

    private async Task ScoreHand()
    {
        var selectedPlayers = PlayersState.Value.Players.Where(p => p.IsSelected).ToList();
        var playerCount = selectedPlayers.Count;
        var maxHands = WizardGameState.GetMaxHands(playerCount);
        var currentHand = WizardState.Value.CurrentHandNumber;

        if (currentHand > maxHands)
            return;

        var playerNames = selectedPlayers.Select(p => p.PlayerName).ToList();
        var defaultTrump = WizardGameState.GetSuitForHand(currentHand);
        var dealerIndex = (currentHand - 1) % playerCount;
        var defaultDealer = playerNames[dealerIndex];

        var parameters = new DialogParameters<ScoreHandDialog>
        {
            { x => x.HandNumber, currentHand },
            { x => x.PlayerNames, playerNames },
            { x => x.DefaultTrump, defaultTrump },
            { x => x.DefaultDealer, defaultDealer },
        };

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        await DialogService.ShowAsync<ScoreHandDialog>($"Score Hand {currentHand}", parameters, options);
    }
}
