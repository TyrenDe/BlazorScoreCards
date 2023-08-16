using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Qwixx;

public record LoadScoresAction(
    IReadOnlyDictionary<QwixxRanks, bool> IsLocked,
    IReadOnlyDictionary<QwixxRanks, int[]> Scores);
