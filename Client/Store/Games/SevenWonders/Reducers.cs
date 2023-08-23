using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.SevenWonders;

public static class Reducers
{
    [ReducerMethod]
    public static SevenWondersGameState ReduceSevenWondersGameState(SevenWondersGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
