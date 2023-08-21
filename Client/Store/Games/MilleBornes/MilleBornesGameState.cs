using Fluxor;
using System;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.MilleBornes;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record MilleBornesGameState(
    bool IsLoading,
    IReadOnlyDictionary<string, int> Scores)
{
    public static MilleBornesGameState CreateInitialState() => new(IsLoading: true, Scores: new Dictionary<string, int>());

    public int GetScore(string playerName)
    {
        if (Scores.TryGetValue(playerName, out var score))
        {
            return score;
        }

        return 0;
    }
}
