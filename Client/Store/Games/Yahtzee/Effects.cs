using Blazored.LocalStorage;
using Fluxor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Yahtzee;

public class Effects
{
    private const string ScoresKey = "yahtzee_scores";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectYahtzeeGameState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        dispatcher.Dispatch(new LoadScoresAction(scores.Scores));
    }

    [EffectMethod]
    public async Task EffectYahtzeeGameState(ResetScoresAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.RemoveItemAsync(ScoresKey);

        var state = YahtzeeGameState.CreateInitialState();
        dispatcher.Dispatch(new LoadScoresAction(state.Scores));
    }

    [EffectMethod]
    public async Task EffectYahtzeeGameState(UpdateScoreAction action, IDispatcher dispatcher)
    {
        var scores = await LoadScoresAsync();

        var newScores = new Dictionary<YahtzeeRanks, int?>(scores.Scores);
        newScores[action.Rank] = action.Score;

        scores = scores with { Scores = newScores };

        await UpdateAndDispatchAsync(dispatcher, scores);
    }

    private async Task<YahtzeeGameState> LoadScoresAsync()
    {
        var state = await _LocalStorageService.GetItemAsync<YahtzeeGameState>(ScoresKey);
        if (state == null)
        {
            state = YahtzeeGameState.CreateInitialState();
        }

        return state;
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, YahtzeeGameState state)
    {
        await _LocalStorageService.SetItemAsync(ScoresKey, state);
        dispatcher.Dispatch(new LoadScoresAction(state.Scores));
    }
}
