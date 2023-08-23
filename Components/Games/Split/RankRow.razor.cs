using BlazorScoreCards.Client.Store.Games.Split;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace BlazorScoreCards.Components.Games.Split;

public partial class RankRow
{
    private static readonly IReadOnlyDictionary<SplitRanks, int[]> _ValueMap = new Dictionary<SplitRanks, int[]>()
    {
        { SplitRanks.A, new int[] { 0, 30, 70, 120, 180, 200 } },
        { SplitRanks.K , new int[] {0, 20, 50, 90, 140, 200 } },
        { SplitRanks.Q , new int[] {0, 20, 50, 90, 140, 200 } },
        { SplitRanks.J , new int[] {0, 20, 50, 90, 140, 200 } },
        { SplitRanks.Ten, new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Nine , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Eight , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Seven , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Six , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Five , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Four , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Three , new int[] {0, 10, 30, 60, 100, 200 } },
        { SplitRanks.Two , new int[] {0, 5, 20, 40, 70, 200 } },
    };

    private static readonly IReadOnlyDictionary<SplitRanks, string> _TextMap = new Dictionary<SplitRanks, string>()
    {
        { SplitRanks.A, "A" },
        { SplitRanks.K , "K" },
        { SplitRanks.Q , "Q" },
        { SplitRanks.J , "J" },
        { SplitRanks.Ten, "10" },
        { SplitRanks.Nine , "9" },
        { SplitRanks.Eight , "8" },
        { SplitRanks.Seven , "7" },
        { SplitRanks.Six , "6" },
        { SplitRanks.Five , "5" },
        { SplitRanks.Four , "4" },
        { SplitRanks.Three , "3" },
        { SplitRanks.Two , "2" },
    };

    [EditorRequired]
    [Parameter]
    public SplitRanks Rank { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<SplitGameState> SplitGameState { get; set; } = default!;

    private Color GetColor(int index)
    {
        if (SplitGameState.Value.Scores[Rank] == index)
        {
            return Color.Primary;
        }

        return Color.Default;
    }

    private void AddRank()
    {
        Dispatcher.Dispatch(new AddRankAction(Rank));
    }

    public static int GetScore(SplitRanks rank, int index)
    {
        if (rank == SplitRanks.Negative)
        {
            return -5 * index;
        }

        return _ValueMap[rank][index];
    }
}
