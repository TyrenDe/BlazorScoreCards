using Fluxor;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Mu;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record MuGameState(
    bool IsLoading,
    IReadOnlyDictionary<string, int> Scores)
{
    public static MuGameState CreateInitialState() => new(IsLoading: true, Scores: new Dictionary<string, int>());

    public int GetScore(string playerName)
    {
        if (Scores.TryGetValue(playerName, out var score))
        {
            return score;
        }

        return 0;
    }
}
