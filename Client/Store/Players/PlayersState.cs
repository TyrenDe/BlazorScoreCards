using Fluxor;
using System;
using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Players;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record PlayersState(bool IsLoading, IEnumerable<Player> Players)
{
    public static PlayersState CreateInitialState() => new(IsLoading: true, Players: Array.Empty<Player>());
}

public record Player(string PlayerName, bool IsSelected);