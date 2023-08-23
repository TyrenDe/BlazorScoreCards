using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Qwixx;

public static class Reducers
{
    [ReducerMethod]
    public static QwixxGameState ReduceQwixxGameState(QwixxGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, IsLocked = action.IsLocked, Scores = action.Scores };
    }
}
