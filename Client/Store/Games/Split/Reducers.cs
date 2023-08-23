using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Split;

public static class Reducers
{
    [ReducerMethod]
    public static SplitGameState ReduceSplitGameState(SplitGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
