﻿using Fluxor;

namespace BlazorScoreCards.Client.Store.Theme;

public static class Reducers
{
    [ReducerMethod]
    public static ThemeState ReduceDarkMode(ThemeState state, SetDarkModeCompleteAction action) =>
        state with { IsDarkMode = action.IsDarkMode };
}
