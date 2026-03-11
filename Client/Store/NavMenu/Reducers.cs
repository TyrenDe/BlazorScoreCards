using Fluxor;

namespace BlazorScoreCards.Client.Store.NavMenu;

public static class Reducers
{
    [ReducerMethod]
    public static NavMenuState ReduceNavMenuState(NavMenuState state, ToggleDrawerOpenAction action) =>
        state with { DrawerOpen = !state.DrawerOpen };

    [ReducerMethod]
    public static NavMenuState ReduceNavMenuState(NavMenuState state, SetDrawerOpenAction action) =>
        state with { DrawerOpen = action.NewState };
}
