using BlazorScoreCards.Client.Store.NavMenu;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Shared;

public partial class AppBar
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    public void ToggleDrawer()
    {
        Dispatcher.Dispatch(new ToggleDrawerOpenAction());
    }
}
