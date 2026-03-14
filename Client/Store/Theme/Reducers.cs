using Fluxor;

namespace BlazorScoreCards.Client.Store.Theme;

public static class Reducers
{
    [ReducerMethod]
    public static ThemeState ReduceThemeState(ThemeState state, SetThemePreferenceCompleteAction action) =>
        state with { Preference = action.Preference };
}
