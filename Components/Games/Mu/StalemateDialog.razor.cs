using BlazorScoreCards.Client.Store.Games.Mu;
using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Mu;

public partial class StalemateDialog
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    private int BidValue { get; set; } = 1;

    private void Cancel() => MudDialog.Cancel();

    private string _LastBidder = string.Empty;
    private bool IsLastBidder(string playerName)
    {
        return _LastBidder == playerName;
    }

    private bool IsLastBidderDisabled(string playerName)
    {
        return
            (!string.IsNullOrEmpty(_LastBidder) && _LastBidder != playerName) ||
            IsHighestBidder(playerName);
    }

    private void SetLastBidder(bool isSet, string playerName)
    {
        _LastBidder = isSet ? playerName : string.Empty;
        StateHasChanged();
    }

    private readonly HashSet<string> _HighestBidder = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private bool IsHighestBidder(string playerName)
    {
        return _HighestBidder.Contains(playerName);
    }

    private bool IsHighestBidderDisabled(string playerName)
    {
        return
            IsLastBidder(playerName);
    }

    private void SetHighestBidder(bool isSet, string playerName)
    {
        if (isSet)
        {
            _HighestBidder.Add(playerName);
        }
        else
        {
            _HighestBidder.Remove(playerName);
        }

        StateHasChanged();
    }

    private void UpdateScore()
    {
        var adjustments = new Dictionary<string, int>();

        foreach (var player in PlayersState.Value.Players.Where(p => p.IsSelected))
        {
            if (player.PlayerName == _LastBidder)
            {
                adjustments[player.PlayerName] = -10 * BidValue;
            }
            else if (_HighestBidder.Contains(player.PlayerName))
            {
                adjustments[player.PlayerName] = 5 * BidValue;
            }
            else
            {
                adjustments[player.PlayerName] = 0;
            }
        }

        Dispatcher.Dispatch(new UpdateScoreAction(adjustments));

        MudDialog.Close();
    }

    private bool IsOkDisabled()
    {
        return false;
    }
}
