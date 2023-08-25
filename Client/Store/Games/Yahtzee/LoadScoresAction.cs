using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Yahtzee;

public record LoadScoresAction(
    IReadOnlyDictionary<YahtzeeRanks, int?> Scores);
