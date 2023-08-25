using BlazorScoreCards.Client.Store.Games.Yahtzee;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Yahtzee;

public partial class ScoreCard
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<YahtzeeGameState> YahtzeeGameState { get; set; } = default!;

    private int GetScore()
    {
        var topSectionValue = GetScore(YahtzeeRanks.Ones, YahtzeeRanks.Twos, YahtzeeRanks.Threes, YahtzeeRanks.Fours, YahtzeeRanks.Fives, YahtzeeRanks.Sixes);
        var bonus = (topSectionValue >= 63) ? 35 : 0;
        var bottomSectionValue = GetScore(YahtzeeRanks.ThreeOfAKind, YahtzeeRanks.FourOfAKind, YahtzeeRanks.FullHouse, YahtzeeRanks.SmallStraight, YahtzeeRanks.LargeStraight, YahtzeeRanks.Yahtzees, YahtzeeRanks.Chance);

        return topSectionValue + bonus + bottomSectionValue;
    }

    private string GetBonusLabel()
    {
        var topSectionValue = GetScore(YahtzeeRanks.Ones) + GetScore(YahtzeeRanks.Twos) + GetScore(YahtzeeRanks.Threes) +
            GetScore(YahtzeeRanks.Fours) + GetScore(YahtzeeRanks.Fives) + GetScore(YahtzeeRanks.Sixes);

        return $"Bonus: {topSectionValue} of 63";
    }

    private string GetBonusText()
    {
        var topSectionValue = GetScore(YahtzeeRanks.Ones, YahtzeeRanks.Twos, YahtzeeRanks.Threes, YahtzeeRanks.Fours, YahtzeeRanks.Fives, YahtzeeRanks.Sixes);

        if (topSectionValue >= 63)
        {
            return "35";
        }

        return "0";
    }

    private int GetScore(params YahtzeeRanks[] ranks)
    {
        var value = 0;
        foreach (var rank in ranks)
        {
            value += YahtzeeGameState.Value.Scores[rank] ?? 0;
        }

        return value;
    }

    private IEnumerable<int?> GetScores(params YahtzeeRanks[] ranks)
    {
        foreach (var rank in ranks)
        {
            yield return YahtzeeGameState.Value.Scores[rank];
        }
    }

    private string GetBonusClass()
    {
        var topSectionScores = GetScores(YahtzeeRanks.Ones, YahtzeeRanks.Twos, YahtzeeRanks.Threes, YahtzeeRanks.Fours, YahtzeeRanks.Fives, YahtzeeRanks.Sixes);
        if (topSectionScores.Any(s => s == null))
        {
            return string.Empty;
        }

        return "rounded mud-theme-primary mud-primary-text";
    }
}
