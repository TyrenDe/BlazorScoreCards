using Fluxor;

namespace BlazorScoreCards.Client.Store.Theme;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record ThemeState(bool IsDarkMode)
{
    public static ThemeState CreateInitialState() => new(true);
}

