using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Mu;

public class Effects
{
    private const string ScoresKey = "mu_scores";

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

        foreach (var kvp in action.Adjustments)
        {
            if (scores.TryGetValue(kvp.Key, out var score))
            {
                scores[kvp.Key] = score + kvp.Value;
            }
            else
            {
                scores[kvp.Key] = kvp.Value;
            }
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
        var players = await _LocalStorageService.GetItemAsync<IReadOnlyDictionary<string, int>>(ScoresKey);
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
