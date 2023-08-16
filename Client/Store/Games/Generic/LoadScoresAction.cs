using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Generic;

public record LoadScoresAction(IReadOnlyDictionary<string, int> Scores);
