using BlazorScoreCards.Client.Store.Games.Split;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Components.Games.Split;

public partial class ScoreCard
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<SplitGameState> SplitGameState { get; set; } = default!;

    private int GetScore()
    {
        var score = 0;
        foreach (var kvp in SplitGameState.Value.Scores)
        {
            score += RankRow.GetScore(kvp.Key, kvp.Value);
        }

        return score;
    }

    private int GetNegativeScore()
    {
        return  RankRow.GetScore(SplitRanks.Negative, SplitGameState.Value.Scores[SplitRanks.Negative]);
    }

    private void AddNegative()
    {
        Dispatcher.Dispatch(new AddNegativeAction());
    }

    private void RemoveNegative()
    {
        Dispatcher.Dispatch(new RemoveNegativeAction());
    }
}
