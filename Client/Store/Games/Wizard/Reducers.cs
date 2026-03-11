using Fluxor;

namespace BlazorScoreCards.Client.Store.Games.Wizard;

public static class Reducers
{
    [ReducerMethod]
    public static WizardGameState ReduceWizardGameState(WizardGameState state, LoadScoresAction action)
    {
        return state with { IsLoading = false, Hands = action.Hands };
    }
}
