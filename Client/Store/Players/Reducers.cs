using Fluxor;

namespace BlazorScoreCards.Client.Store.Players;

public static class Reducers
{
    [ReducerMethod]
    public static PlayersState ReducePlayersState(PlayersState state, LoadPlayersAction action)
    {
        return state with { IsLoading = false, Players = action.Players };
    }
}
