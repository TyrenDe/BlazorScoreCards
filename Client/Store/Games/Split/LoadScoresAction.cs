using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Split;

public record LoadScoresAction(
    IReadOnlyDictionary<SplitRanks, int> Scores);
