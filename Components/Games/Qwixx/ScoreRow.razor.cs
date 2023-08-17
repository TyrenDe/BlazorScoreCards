using BlazorScoreCards.Client.Store.Games.Qwixx;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Qwixx;

public partial class ScoreRow
{
    private readonly int[] ScoreMap = { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78 };

    [Inject]
    private IState<QwixxGameState> QwixxGameState { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void ToggleNegative(int index)
    {
        if (IsChecked(index))
        {
            Dispatcher.Dispatch(new ClearNegativeAction(index));
        }
        else
        {
            Dispatcher.Dispatch(new SetNegativeAction(index));
        }
    }

    private bool IsChecked(int index)
    {
        return QwixxGameState.Value.Scores[QwixxRanks.Negative].Contains(index);
    }

    private int GetScore(QwixxRanks color)
    {
        var values = QwixxGameState.Value.Scores[color];

        if (color == QwixxRanks.Negative)
        {
            return -5 * values.Length;
        }

        var isRedOrYellow = (color == QwixxRanks.Red) || (color == QwixxRanks.Yellow);
        var count = values.Length;
        if ((isRedOrYellow && values.Contains(12)) || (!isRedOrYellow && values.Contains(2)))
        {
            // They earned an extra X
            count++;
        }

        return ScoreMap[count];
    }

    private int GetTotalScore()
    {
        return GetScore(QwixxRanks.Red) +
            GetScore(QwixxRanks.Yellow) +
            GetScore(QwixxRanks.Green) +
            GetScore(QwixxRanks.Blue) +
            GetScore(QwixxRanks.Negative);
    }
}
