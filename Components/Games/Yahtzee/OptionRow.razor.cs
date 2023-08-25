using BlazorScoreCards.Client.Store.Games.Yahtzee;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;

namespace BlazorScoreCards.Components.Games.Yahtzee;

public partial class OptionRow
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<YahtzeeGameState> YahtzeeGameState { get; set; } = default!;

    [EditorRequired]
    [Parameter] public YahtzeeRanks Rank { get; set; }

    private string Value
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

    private Variant GetVariant()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Variant.Outlined;
        }

        return Variant.Filled;
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
            YahtzeeRanks.FullHouse => "Full House",
            YahtzeeRanks.SmallStraight => "Sm. Straight",
            YahtzeeRanks.LargeStraight => "Lg. Straight",
            YahtzeeRanks.Yahtzees => "Yahtzee",
            _ => string.Empty,
        };
    }

    private IEnumerable<(string? Value, string Text)> GetOptions()
    {
        return Rank switch
        {
            YahtzeeRanks.Ones => new (string? Value, string Text)[] { (null, "-"), ("1", "1"), ("2", "2"), ("3", "3"), ("4", "4"), ("5", "5"), ("6", "6"), ("0", "0") },
            YahtzeeRanks.Twos => new (string? Value, string Text)[] { (null, "-"), ("2", "2"), ("4", "4"), ("6", "6"), ("8", "8"), ("10", "10"), ("12", "12"), ("0", "0") },
            YahtzeeRanks.Threes => new (string? Value, string Text)[] { (null, "-"), ("3", "3"), ("6", "6"), ("9", "9"), ("12", "12"), ("15", "15"), ("18", "18"), ("0", "0") },
            YahtzeeRanks.Fours => new (string? Value, string Text)[] { (null, "-"), ("4", "4"), ("8", "8"), ("12", "12"), ("16", "16"), ("20", "20"), ("24", "24"), ("0", "0") },
            YahtzeeRanks.Fives => new (string? Value, string Text)[] { (null, "-"), ("5", "5"), ("10", "10"), ("15", "15"), ("20", "20"), ("25", "25"), ("30", "30"), ("0", "0") },
            YahtzeeRanks.Sixes => new (string? Value, string Text)[] { (null, "-"), ("6", "6"), ("12", "12"), ("18", "18"), ("24", "24"), ("30", "30"), ("36", "36"), ("0", "0") },
            YahtzeeRanks.FullHouse => new (string? Value, string Text)[] { (null, "-"), ("25", "25"), ("0", "0") },
            YahtzeeRanks.SmallStraight => new (string? Value, string Text)[] { (null, "-"), ("30", "30"), ("0", "0") },
            YahtzeeRanks.LargeStraight => new (string? Value, string Text)[] { (null, "-"), ("40", "40"), ("0", "0") },
            YahtzeeRanks.Yahtzees => new (string? Value, string Text)[] { (null, "-"), ("50", "50"), ("0", "0"), ("150", "50+1"), ("250", "50+2"), ("350", "50+3") },
            _ => Array.Empty<(string?, string)>(),
        };
    }
}
