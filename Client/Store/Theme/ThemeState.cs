using Fluxor;

namespace BlazorScoreCards.Client.Store.Theme;

public enum ThemePreference
{
    System,
    Light,
    Dark,
}

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record ThemeState(ThemePreference Preference)
{
    public static ThemeState CreateInitialState() => new(ThemePreference.System);
}
