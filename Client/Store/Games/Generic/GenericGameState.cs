using Fluxor;
using System;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Generic;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record GenericGameState(
    bool IsLoading,
    IReadOnlyDictionary<string, int> Scores)
{
    public static GenericGameState CreateInitialState() => new(IsLoading: true, Scores: new Dictionary<string, int>());

    public int GetScore(string playerName)
    {
        if (Scores.TryGetValue(playerName, out var score))
        {
            return score;
        }

        return 0;
    }
}
