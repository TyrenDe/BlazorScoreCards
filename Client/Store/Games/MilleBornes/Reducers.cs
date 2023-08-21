using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.MilleBornes;

public static class Reducers
{
    [ReducerMethod]
    public static MilleBornesGameState ReduceMilleBornesGameState(MilleBornesGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
