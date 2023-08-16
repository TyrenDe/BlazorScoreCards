using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Generic;

public class Effects
{
    private const string ScoresKey = "generic_scores";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectGenericGameState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        dispatcher.Dispatch(new LoadScoresAction(scores));
    }

    [EffectMethod]
    public async Task EffectGenericGameState(UpdateScoreAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        if (scores.TryGetValue(action.PlayerName, out var score))
        {
            scores[action.PlayerName] = score + action.Adjustment;
        }
        else
        {
            scores[action.PlayerName] = action.Adjustment;
        }

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(ResetScoresAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.RemoveItemAsync(ScoresKey);

        dispatcher.Dispatch(new LoadScoresAction(new Dictionary<string, int>()));
    }

    private async Task<Dictionary<string, int>> LoadScoresAsync()
    {
        var players = await _LocalStorageService.GetItemAsync<IReadOnlyDictionary<string,  int>>(ScoresKey);
        if (players == null)
        {
            players = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        return new Dictionary<string, int>(players, StringComparer.OrdinalIgnoreCase);
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, IReadOnlyDictionary<string, int> scores)
    {
        await _LocalStorageService.SetItemAsync(ScoresKey, scores);
        dispatcher.Dispatch(new LoadScoresAction(scores));
    }
}
