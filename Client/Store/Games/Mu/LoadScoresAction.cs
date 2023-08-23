using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Mu;

public record LoadScoresAction(IReadOnlyDictionary<string, int> Scores);
