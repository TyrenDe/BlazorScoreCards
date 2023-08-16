using Fluxor;
using System;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Qwixx;

public enum QwixxRanks
{
    Red,
    Yellow,
    Green,
    Blue,

    Negative,
}

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record QwixxGameState(
    bool IsLoading,
    IReadOnlyDictionary<QwixxRanks, bool> IsLocked,
    IReadOnlyDictionary<QwixxRanks, int[]> Scores)
{
    public static QwixxGameState CreateInitialState() =>
        new(
            IsLoading: true,
            IsLocked: new Dictionary<QwixxRanks, bool>
            {
                { QwixxRanks.Red, false },
                { QwixxRanks.Yellow, false },
                { QwixxRanks.Green, false },
                { QwixxRanks.Blue, false },
                { QwixxRanks.Negative, false },
            },
            Scores: new Dictionary<QwixxRanks, int[]>
            {
                { QwixxRanks.Red, Array.Empty<int>() },
                { QwixxRanks.Yellow, Array.Empty<int>() },
                { QwixxRanks.Green, Array.Empty<int>() },
                { QwixxRanks.Blue, Array.Empty<int>() },
                { QwixxRanks.Negative, Array.Empty<int>() },
            });
}
