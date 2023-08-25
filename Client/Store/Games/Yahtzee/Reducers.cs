using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Yahtzee;

public static class Reducers
{
    [ReducerMethod]
    public static YahtzeeGameState ReduceSplitGameState(YahtzeeGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Scores = action.Scores };
    }
}
