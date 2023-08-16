using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Players;

public record LoadPlayersAction(IEnumerable<Player> Players);
