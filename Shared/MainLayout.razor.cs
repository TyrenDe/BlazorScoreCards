using BlazorScoreCards.Client.Store.Theme;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Shared;

public partial class MainLayout
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = default!;

    private MudThemeProvider? _themeProvider;
    private ThemePreference _lastThemePreference = ThemePreference.System;
    private bool _systemIsDarkMode = true;

    private bool UseSystemTheme => ThemeState.Value.Preference == ThemePreference.System;

    private bool CurrentIsDarkMode => ThemeState.Value.Preference switch
    {
        ThemePreference.Dark => true,
        ThemePreference.Light => false,
        _ => _systemIsDarkMode,
    };

    private static readonly LayoutProperties _DefaultLayoutProperties = new LayoutProperties()
    {
        AppbarHeight = "0px !important",
    };

    private MudTheme DefaultTheme { get; init; } = new MudTheme()
    {
        LayoutProperties = _DefaultLayoutProperties
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_themeProvider is null)
        {
            return;
        }

        if (firstRender || _lastThemePreference != ThemeState.Value.Preference && UseSystemTheme)
        {
            _systemIsDarkMode = await _themeProvider.GetSystemDarkModeAsync();
            _lastThemePreference = ThemeState.Value.Preference;
            StateHasChanged();
            return;
        }

        _lastThemePreference = ThemeState.Value.Preference;
    }

    private Task OnDarkModeChanged(bool isDarkMode)
    {
        if (UseSystemTheme && _systemIsDarkMode != isDarkMode)
        {
            _systemIsDarkMode = isDarkMode;
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
