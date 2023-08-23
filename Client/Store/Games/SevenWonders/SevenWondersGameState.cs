using Fluxor;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.SevenWonders;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record SevenWondersGameState(
    bool IsLoading,
    IReadOnlyDictionary<string, int> Scores)
{
    public static SevenWondersGameState CreateInitialState() => new(IsLoading: true, Scores: new Dictionary<string, int>());

    public int GetScore(string playerName)
    {
        if (Scores.TryGetValue(playerName, out var score))
        {
            return score;
        }

        return 0;
    }
}
