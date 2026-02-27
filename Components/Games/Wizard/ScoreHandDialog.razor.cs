using BlazorScoreCards.Client.Store.Games.Wizard;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Wizard;

public partial class ScoreHandDialog
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public int HandNumber { get; set; }

    [Parameter]
    public IReadOnlyList<string> PlayerNames { get; set; } = [];

    [Parameter]
    public WizardTrump DefaultTrump { get; set; }

    [Parameter]
    public string DefaultDealer { get; set; } = string.Empty;

    private WizardTrump Trump { get; set; }
    private string DealerName { get; set; } = string.Empty;

    private readonly Dictionary<string, int> _Bids = new();
    private readonly Dictionary<string, int> _Tricks = new();

    private string ValidationMessage { get; set; } = string.Empty;

    private int CardsPerPlayer => HandNumber;

    protected override void OnParametersSet()
    {
        Trump = DefaultTrump;
        DealerName = DefaultDealer;
    }

    private int GetBid(string playerName) =>
        _Bids.TryGetValue(playerName, out var bid) ? bid : 0;

    private void SetBid(string playerName, int value)
    {
        _Bids[playerName] = value;
        ValidationMessage = string.Empty;
        StateHasChanged();
    }

    private int GetTricks(string playerName) =>
        _Tricks.TryGetValue(playerName, out var tricks) ? tricks : 0;

    private void SetTricks(string playerName, int value)
    {
        _Tricks[playerName] = value;
        ValidationMessage = string.Empty;
        StateHasChanged();
    }

    private int GetPreviewScore(string playerName)
    {
        var bid = GetBid(playerName);
        var tricks = GetTricks(playerName);
        return WizardGameState.CalculateScore(bid, tricks);
    }

    private bool IsSubmitDisabled()
    {
        if (string.IsNullOrEmpty(DealerName))
            return true;

        var totalTricks = PlayerNames.Sum(GetTricks);
        if (totalTricks != CardsPerPlayer)
            return true;

        return false;
    }

    private void Cancel() => MudDialog.Cancel();

    private void Submit()
    {
        var totalTricks = PlayerNames.Sum(GetTricks);
        if (totalTricks != CardsPerPlayer)
        {
            ValidationMessage = $"Total tricks ({totalTricks}) must equal the number of cards dealt ({CardsPerPlayer}).";
            return;
        }

        var playerResults = PlayerNames.Select(name =>
            new WizardHandPlayerResult(
                name,
                GetBid(name),
                GetTricks(name),
                GetPreviewScore(name))).ToList();

        var handResult = new WizardHandResult(HandNumber, Trump, DealerName, playerResults);

        Dispatcher.Dispatch(new ScoreHandAction(handResult));
        MudDialog.Close();
    }
}
