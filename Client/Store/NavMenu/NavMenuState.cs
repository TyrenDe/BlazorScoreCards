using Fluxor;

namespace BlazorScoreCards.Client.Store.NavMenu;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record NavMenuState(bool DrawerOpen)
{
    public static NavMenuState CreateInitialState() => new(true);
}

