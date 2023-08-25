using BlazorScoreCards.Client.Store.Games.Yahtzee;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorScoreCards.Components.Games.Yahtzee;

public partial class NumberRow
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<YahtzeeGameState> YahtzeeGameState { get; set; } = default!;

    [EditorRequired]
    [Parameter] public YahtzeeRanks Rank { get; set; }

    private string Score 
    {
        get => YahtzeeGameState.Value.Scores[Rank]?.ToString() ?? string.Empty;
        set
        {
            int? val = null;

            if (int.TryParse(value, out var theValue))
            {
                val = theValue;
            }

            Dispatcher.Dispatch(new UpdateScoreAction(Rank, val));
        }
    }

    private string GetLabel()
    {
        return Rank switch
        {
            YahtzeeRanks.Ones => "Ones",
            YahtzeeRanks.Twos => "Twos",
            YahtzeeRanks.Threes => "Threes",
            YahtzeeRanks.Fours => "Fours",
            YahtzeeRanks.Fives => "Fives",
            YahtzeeRanks.Sixes => "Sixes",
            YahtzeeRanks.Chance => "Chance",
            YahtzeeRanks.ThreeOfAKind => "3 of a Kind",
            YahtzeeRanks.FourOfAKind => "4 of a Kind",
            _ => string.Empty,
        };
    }

    private Variant GetVariant()
    {
        if (string.IsNullOrWhiteSpace(Score))
        {
            return Variant.Outlined;
        }

        return Variant.Filled;
    }
}
