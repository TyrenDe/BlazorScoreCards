using Blazored.LocalStorage;
using Fluxor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Theme;

public class Effects
{
    private const string DarkModeKey = "darkmode";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectThemeState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var current = true;
        var currentValue = await _LocalStorageService.GetItemAsStringAsync(DarkModeKey);
        if (!string.IsNullOrWhiteSpace(currentValue))
        {
            bool.TryParse(currentValue, out current);
        }

        dispatcher.Dispatch(new SetDarkModeCompleteAction(current));
    }

    [EffectMethod]
    public async Task EffectThemeState(SetDarkModeAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.SetItemAsStringAsync(DarkModeKey, action.IsDarkMode.ToString());
        dispatcher.Dispatch(new SetDarkModeCompleteAction(action.IsDarkMode));
    }
}
