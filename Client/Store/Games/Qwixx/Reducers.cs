using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Qwixx;

public static class Reducers
{
    [ReducerMethod]
    public static QwixxGameState ReduceGenericGameState(QwixxGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, IsLocked = action.IsLocked, Scores = action.Scores };
    }
}
