using BlazorScoreCards.Client.Store.NavMenu;
using BlazorScoreCards.Client.Store.Theme;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Shared;

public partial class NavBar
{
    [Inject]
    private IState<NavMenuState> NavBarState { get; set; } = default!;

    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void OpenChanged(bool newValue)
    {
        // This allows for support of auto open/collapse
        if (newValue != NavBarState.Value.DrawerOpen)
        {
            Dispatcher.Dispatch(new SetDrawerOpenAction(newValue));
        }
    }

    private void DarkModeCheckedChanged(bool newValue)
    {
        Dispatcher.Dispatch(new SetDarkModeAction(newValue));
    }
}
