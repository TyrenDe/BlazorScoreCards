using Fluxor;

namespace BlazorScoreCards.Client.Store.NavMenu;

public static class Reducers
{
    [ReducerMethod]
    public static NavMenuState ReduceNavMenu(NavMenuState state, ToggleDrawerOpenAction action) =>
        state with { DrawerOpen = !state.DrawerOpen };

    [ReducerMethod]
    public static NavMenuState ReduceNavMenu(NavMenuState state, SetDrawerOpenAction action) =>
        state with { DrawerOpen = action.NewState };
}
