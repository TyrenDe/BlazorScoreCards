using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.SevenWonders;

public record LoadScoresAction(IReadOnlyDictionary<string, int> Scores);
