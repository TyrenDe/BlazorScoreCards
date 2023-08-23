using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Mu;

public static class Reducers
{
    [ReducerMethod]
    public static MuGameState ReduceMuGameState(MuGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
