using Blazored.LocalStorage;
using Fluxor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Games.Wizard;

public class Effects
{
    private const string ScoresKey = "wizard_scores";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectWizardGameState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var hands = await LoadHandsAsync();

        dispatcher.Dispatch(new LoadScoresAction(hands));
    }

    [EffectMethod]
    public async Task EffectWizardGameState(ScoreHandAction action, IDispatcher dispatcher)
    {
        var hands = await LoadHandsAsync();
        hands.Add(action.HandResult);

        await UpdateAndDispatchAsync(dispatcher, hands);
    }

    [EffectMethod]
    public async Task EffectWizardGameState(UndoLastHandAction action, IDispatcher dispatcher)
    {
        var hands = await LoadHandsAsync();
        if (hands.Count > 0)
        {
            hands.RemoveAt(hands.Count - 1);
        }

        await UpdateAndDispatchAsync(dispatcher, hands);
    }

    [EffectMethod]
    public async Task EffectWizardGameState(ResetScoresAction action, IDispatcher dispatcher)
    {
        await _LocalStorageService.RemoveItemAsync(ScoresKey);

        dispatcher.Dispatch(new LoadScoresAction([]));
    }

    private async Task<List<WizardHandResult>> LoadHandsAsync()
    {
        var hands = await _LocalStorageService.GetItemAsync<List<WizardHandResult>>(ScoresKey);
        return hands ?? [];
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, List<WizardHandResult> hands)
    {
        await _LocalStorageService.SetItemAsync(ScoresKey, hands);
        dispatcher.Dispatch(new LoadScoresAction(hands));
    }
}
