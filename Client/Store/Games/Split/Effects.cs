using Blazored.LocalStorage;
using Fluxor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Split;

public class Effects
{
    private const string ScoresKey = "split_scores";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectSplitGameState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        dispatcher.Dispatch(new LoadScoresAction(scores.Scores));
    }

    [EffectMethod]
    public async Task EffectSplitGameState(AddRankAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<SplitRanks, int>(scores.Scores);
        newScores[action.Rank] = (newScores[action.Rank] + 1) % 5;

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectSplitGameState(AddNegativeAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<SplitRanks, int>(scores.Scores);
        newScores[SplitRanks.Negative] = newScores[SplitRanks.Negative] + 1;

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectSplitGameState(RemoveNegativeAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<SplitRanks, int>(scores.Scores);
        newScores[SplitRanks.Negative] = newScores[SplitRanks.Negative] - 1;

        if (newScores[SplitRanks.Negative] < 0)
        {
            newScores[SplitRanks.Negative] = 0;
        }

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    [EffectMethod]
    public async Task EffectSplitGameState(ResetScoresAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.RemoveItemAsync(ScoresKey);

        var state = SplitGameState.CreateInitialState();
        dispatcher.Dispatch(new LoadScoresAction(state.Scores));
    }

    private async Task<SplitGameState> LoadScoresAsync()
    {
        var state = await _LocalStorageService.GetItemAsync<SplitGameState>(ScoresKey);
        if (state == null)
        {
            state = SplitGameState.CreateInitialState();
        }

        return state;
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, SplitGameState state)
    {
        await _LocalStorageService.SetItemAsync(ScoresKey, state);
        dispatcher.Dispatch(new LoadScoresAction(state.Scores));
    }
}
