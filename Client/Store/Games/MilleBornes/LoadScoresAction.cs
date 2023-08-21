using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.MilleBornes;

public record LoadScoresAction(IReadOnlyDictionary<string, int> Scores);
