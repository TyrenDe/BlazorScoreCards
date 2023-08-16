using BlazorScoreCards.Client.Store.Games.Qwixx;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Qwixx;

public partial class ColorRow
{
    private readonly int[] ScoreMap = { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78 };
    private readonly IReadOnlyDictionary<ButtonStates, string> ButtonText = new Dictionary<ButtonStates, string>
    {
        { ButtonStates.DoubleScored, "XX" },
        { ButtonStates.Scored, "X" },
        { ButtonStates.Unavailable, "--" },
    };

    [EditorRequired]
    [Parameter]
    public QwixxRanks Color { get; set; }

    [EditorRequired]
    [Parameter]
    public bool IsLocked { get; set; }

    [Inject]
    private IState<QwixxGameState> QwixxGameState { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void Lock()
    {
        Dispatcher.Dispatch(new LockValueAction(Color));
    }

    private void Unlock()
    {
        Dispatcher.Dispatch(new UnlockValueAction(Color));
    }

    private string GetTextForButton(int number)
    {
        (var buttonState, _) = GetButtonState(number);
        if (ButtonText.TryGetValue(buttonState, out var text))
        {
            return text;
        }

        return number.ToString();
    }

    private bool IsDisabled(int number)
    {
        (var buttonState, _) = GetButtonState(number);
        return buttonState == ButtonStates.Disabled;
    }

    private void ScoreNumber(int number)
    {
        (var buttonState, var highest) = GetButtonState(number);

        switch (buttonState)
        {
            case ButtonStates.Normal:
                Dispatcher.Dispatch(new AddValueAction(Color, number));
                break;


            case ButtonStates.Scored:
            case ButtonStates.DoubleScored:
                if (highest)
                {
                    Dispatcher.Dispatch(new RemoveValueAction(Color, number));
                }

                break;

            default:
                break;
        }
    }

    private (ButtonStates State, bool IsHighest) GetButtonState(int number)
    {
        var isRedOrYellow = Color == QwixxRanks.Red || Color == QwixxRanks.Yellow;

        var highest = isRedOrYellow ? 0 : 13;
        if (QwixxGameState.Value.Scores[Color].Length > 0)
        {
            if (isRedOrYellow)
            {
                highest = QwixxGameState.Value.Scores[Color].Max();
            }
            else
            {
                highest = QwixxGameState.Value.Scores[Color].Min();
            }
        }

        if (QwixxGameState.Value.Scores[Color].Contains(number))
        {
            if ((isRedOrYellow && number == 12) || (!isRedOrYellow && number == 2))
            {
                return (ButtonStates.DoubleScored, true);
            }

            return (ButtonStates.Scored, highest == number);
        }

        if (QwixxGameState.Value.IsLocked[Color])
        {
            return (ButtonStates.Unavailable, false);
        }

        if ((isRedOrYellow && number < highest) || (!isRedOrYellow && number > highest))
        {
            return (ButtonStates.Unavailable, false);
        }

        if (QwixxGameState.Value.Scores[Color].Length < 5)
        {
            if ((isRedOrYellow && number == 12) || (!isRedOrYellow && number == 2))
            {
                return (ButtonStates.Disabled, false);
            }
        }

        return (ButtonStates.Normal, false);
    }

    private string GetClassNames()
    {
        return Color switch
        {
            QwixxRanks.Red => "row-color row-color-red",
            QwixxRanks.Yellow => "row-color row-color-yellow",
            QwixxRanks.Green => "row-color row-color-green",
            QwixxRanks.Blue => "row-color row-color-blue",
            _ => throw new InvalidOperationException(),
        };
    }

    private IEnumerable<int> GetNumbers()
    {
        return Color switch
        {
            QwixxRanks.Red => Enumerable.Range(2, 11),
            QwixxRanks.Yellow => Enumerable.Range(2, 11),
            QwixxRanks.Green => Enumerable.Range(2, 11).Reverse(),
            QwixxRanks.Blue => Enumerable.Range(2, 11).Reverse(),
            _ => throw new InvalidOperationException(),
        };
    }

    private enum ButtonStates
    {
        Normal,
        Unavailable,
        Disabled,
        Scored,
        DoubleScored,
    }
}
