using Fluxor;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Yahtzee;

public enum YahtzeeRanks
{
    Ones,       // Score
    Twos,       // Score
    Threes,     // Score
    Fours,      // Score
    Fives,      // Score
    Sixes,      // Score
    ThreeOfAKind,   // Score
    FourOfAKind,    // Score
    FullHouse,      // 25
    SmallStraight,  // 30
    LargeStraight,  // 40
    Yahtzees,   // 50 + 100 + 100 + 100 (max)
    Chance,     // Score
}

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record YahtzeeGameState(
    bool IsLoading,
    IReadOnlyDictionary<YahtzeeRanks, int?> Scores)
{
    public static YahtzeeGameState CreateInitialState() =>
        new(
            IsLoading: true,
            Scores: new Dictionary<YahtzeeRanks, int?>
            {
                { YahtzeeRanks.Ones, null },
                { YahtzeeRanks.Twos, null },
                { YahtzeeRanks.Threes, null },
                { YahtzeeRanks.Fours, null },
                { YahtzeeRanks.Fives, null },
                { YahtzeeRanks.Sixes, null },
                { YahtzeeRanks.ThreeOfAKind, null },
                { YahtzeeRanks.FourOfAKind, null },
                { YahtzeeRanks.FullHouse, null },
                { YahtzeeRanks.SmallStraight, null },
                { YahtzeeRanks.LargeStraight, null },
                { YahtzeeRanks.Yahtzees, null },
                { YahtzeeRanks.Chance, null },
            });
}
