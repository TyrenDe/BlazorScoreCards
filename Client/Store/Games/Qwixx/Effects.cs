using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Qwixx;

public class Effects
{
    private const string ScoresKey = "qwixx_scores";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectGenericGameState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        dispatcher.Dispatch(new LoadScoresAction(scores.IsLocked, scores.Scores));
    }

    [EffectMethod]
    public async Task EffectGenericGameState(RemoveValueAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<QwixxRanks, int[]>(scores.Scores);
        var indexOf = Array.IndexOf(newScores[action.Rank], action.Number);
        newScores[action.Rank] = newScores[action.Rank][..indexOf];

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(AddValueAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<QwixxRanks, int[]>(scores.Scores);
        newScores[action.Rank] = newScores[action.Rank].Concat(new int[] { action.Number }).ToArray();

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(SetNegativeAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<QwixxRanks, int[]>(scores.Scores);
        newScores[QwixxRanks.Negative] = newScores[QwixxRanks.Negative].Concat(new int[] { action.Index }).ToArray();

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(ClearNegativeAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<QwixxRanks, int[]>(scores.Scores);
        newScores[QwixxRanks.Negative] = newScores[QwixxRanks.Negative].Where(v => v != action.Index).ToArray();

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(LockValueAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newIsLocked = new Dictionary<QwixxRanks, bool>(scores.IsLocked);
        newIsLocked[action.Rank] = newIsLocked[action.Rank] = true;

        scores = scores with { IsLocked = newIsLocked };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(UnlockValueAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newIsLocked = new Dictionary<QwixxRanks, bool>(scores.IsLocked);
        newIsLocked[action.Rank] = newIsLocked[action.Rank] = false;

        scores = scores with { IsLocked = newIsLocked };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectGenericGameState(ResetScoresAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.RemoveItemAsync(ScoresKey);

        var state = QwixxGameState.CreateInitialState();
        dispatcher.Dispatch(new LoadScoresAction(state.IsLocked, state.Scores));
    }

    private async Task<QwixxGameState> LoadScoresAsync()
    {
        var state = await _LocalStorageService.GetItemAsync<QwixxGameState>(ScoresKey);
        if (state == null)
        {
            state = QwixxGameState.CreateInitialState();
        }

        return state;
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, QwixxGameState state)
    {
        await _LocalStorageService.SetItemAsync(ScoresKey, state);
        dispatcher.Dispatch(new LoadScoresAction(state.IsLocked, state.Scores));
    }
}
