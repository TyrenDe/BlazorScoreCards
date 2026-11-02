using Blazored.LocalStorage;
using Fluxor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Theme;

public class Effects
{
    private const string ThemePreferenceKey = "darkmode";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectThemeState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var current = ThemePreference.System;
        var currentValue = await _LocalStorageService.GetItemAsStringAsync(ThemePreferenceKey);

        if (!string.IsNullOrWhiteSpace(currentValue))
        {
            if (System.Enum.TryParse<ThemePreference>(currentValue, true, out var currentPreference))
            {
                current = currentPreference;
            }
            else if (bool.TryParse(currentValue, out var isDarkMode))
            {
                current = isDarkMode ? ThemePreference.Dark : ThemePreference.Light;
            }
        }

        dispatcher.Dispatch(new SetThemePreferenceCompleteAction(current));
    }

    [EffectMethod]
    public async Task EffectThemeState(SetThemePreferenceAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.SetItemAsStringAsync(ThemePreferenceKey, action.Preference.ToString());
        dispatcher.Dispatch(new SetThemePreferenceCompleteAction(action.Preference));
    }
}
