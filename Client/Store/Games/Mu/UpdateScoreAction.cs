using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Mu;

public record UpdateScoreAction(IDictionary<string, int> Adjustments);
