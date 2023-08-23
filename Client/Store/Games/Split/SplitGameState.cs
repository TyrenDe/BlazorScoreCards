using Fluxor;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Split;

public enum SplitRanks
{
    A,
    K,
    Q,
    J,
    Ten,
    Nine,
    Eight,
    Seven,
    Six,
    Five,
    Four,
    Three,
    Two,

    Negative,
}

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record SplitGameState(
    bool IsLoading,
    IReadOnlyDictionary<SplitRanks, int> Scores)
{
    public static SplitGameState CreateInitialState() =>
        new(
            IsLoading: true,
            Scores: new Dictionary<SplitRanks, int>
            {
                { SplitRanks.A, 0 },
                { SplitRanks.K, 0 },
                { SplitRanks.Q, 0 },
                { SplitRanks.J, 0 },
                { SplitRanks.Ten, 0 },
                { SplitRanks.Nine, 0 },
                { SplitRanks.Eight, 0 },
                { SplitRanks.Seven, 0 },
                { SplitRanks.Six, 0 },
                { SplitRanks.Five, 0 },
                { SplitRanks.Four, 0 },
                { SplitRanks.Three, 0 },
                { SplitRanks.Two, 0 },

                { SplitRanks.Negative, 0 },
            });
}
