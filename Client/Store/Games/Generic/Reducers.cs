using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Generic;

public static class Reducers
{
    [ReducerMethod]
    public static GenericGameState ReduceGenericGameState(GenericGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
