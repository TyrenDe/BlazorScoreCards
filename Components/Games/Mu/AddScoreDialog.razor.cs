using BlazorScoreCards.Client.Store.Games.Mu;
using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Components.Games.Mu;

public enum Trumps
{
    Black,
    Blue,
    Green,
    Red,
    Yellow,
    One,
    Seven,
    Zero,
    Two,
    Three,
    Four,
    Five,
    Six,
    Eight,
    Nine,
    None,
}

public partial class AddScoreDialog
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    private readonly Dictionary<string, int> _Points = new Dictionary<string, int>();

    private int BidValue { get; set; } = 1;

    private Trumps OverTrump { get; set; } = Trumps.Black;
    private Trumps UnderTrump { get; set; } = Trumps.Blue;

    private void Cancel() => MudDialog.Cancel();

    private string PointOffset()
    {
        return (60 - _Points.Values.Sum()).ToString();
    }

    private int GetScore(string playerName)
    {
        if (_Points.TryGetValue(playerName, out var score))
        {
            return score;
        }

        return 0;
    }

    private string GetScoreString(string playerName)
    {
        return GetScore(playerName).ToString();
    }

    private void SetScore(string score, string playerName)
    {
        _Points[playerName] = int.Parse(score);
        StateHasChanged();
    }

    private string _Chief = string.Empty;
    private bool IsChief(string playerName)
    {
        return _Chief == playerName;
    }

    private bool IsChiefDisabled(string playerName)
    {
        return
            (!string.IsNullOrEmpty(_Chief) && _Chief != playerName) ||
            IsVice(playerName) ||
            IsPartner(playerName);
    }

    private void SetChief(bool isSet, string playerName)
    {
        _Chief = isSet ? playerName : string.Empty;
        StateHasChanged();
    }

    private string _Vice = string.Empty;
    private bool IsVice(string playerName)
    {
        return _Vice == playerName;
    }

    private bool IsViceDisabled(string playerName)
    {
        return
            (!string.IsNullOrEmpty(_Vice) && _Vice != playerName) ||
            IsChief(playerName) ||
            IsPartner(playerName);
    }

    private void SetVice(bool isSet, string playerName)
    {
        _Vice = isSet ? playerName : string.Empty;
        StateHasChanged();
    }

    private string _Partner = string.Empty;
    private bool IsPartner(string playerName)
    {
        return _Partner == playerName;
    }

    private bool IsPartnerDisabled(string playerName)
    {
        return
            (!string.IsNullOrEmpty(_Partner) && _Partner != playerName) ||
            IsChief(playerName) ||
            IsVice(playerName);
    }

    private void SetPartner(bool isSet, string playerName)
    {
        _Partner = isSet ? playerName : string.Empty;
        StateHasChanged();
    }

    private void UpdateScore()
    {
        var adjustments = new Dictionary<string, int>();

        // First check to see if the chief team made it
        var chiefTeamPoints = GetScore(_Chief) + GetScore(_Partner);
        var chiefTeamBidMade = GetChiefTeamBidMade(PlayersState.Value.Players.Count(p => p.IsSelected), chiefTeamPoints);

        if (chiefTeamBidMade >= BidValue)
        {
            // Chief made it!
            foreach (var player in PlayersState.Value.Players.Where(p => p.IsSelected))
            {
                var score = GetScore(player.PlayerName);
                if (player.PlayerName == _Chief || player.PlayerName == _Partner)
                {
                    score += GetChiefTeamBonus(OverTrump, BidValue);
                }

                adjustments[player.PlayerName] = score;
            }
        }
        else
        {
            // Chief missed it!
            var missedBy = BidValue - chiefTeamBidMade;
            foreach (var player in PlayersState.Value.Players.Where(p => p.IsSelected))
            {
                var score = GetScore(player.PlayerName);
                if (player.PlayerName == _Chief)
                {
                    score -= 10 * missedBy;
                }
                else if (player.PlayerName != _Partner)
                {
                    score += 5 * missedBy;
                }

                adjustments[player.PlayerName] = score;
            }
        }

        Dispatcher.Dispatch(new UpdateScoreAction(adjustments));

        MudDialog.Close();
    }

    private bool IsOkDisabled()
    {
        // All hands must have a chief and partner
        if (string.IsNullOrEmpty(_Chief) ||
            string.IsNullOrEmpty(_Partner))
        {
            return true;
        }

        // All points must add up to 60
        var total = _Points.Values.Sum();
        if (total != 60)
        {
            return true;
        }

        return false;
    }

    private readonly int[] PointsPerBid = new int[] { 0, 0, 0, 0, 2, 3, 4 };
    private int GetChiefTeamBidMade(int playerCount, int points)
    {
        var currentPoints = 62 - playerCount; // Hacky, but works, as 4 player = 58, 5 player = 57, 6 player = 56
        var currentBid = 60 / playerCount;
        while (currentPoints > points)
        {
            currentBid--;
            currentPoints -= PointsPerBid[playerCount];
        }

        return Math.Max(0, currentBid);
    }

    private int GetChiefTeamBonus(Trumps overTrump, int bid)
    {
        switch (overTrump)
        {
            case Trumps.Black:
            case Trumps.Blue:
            case Trumps.Green:
            case Trumps.Red:
            case Trumps.Yellow:
                return 10 * Math.Min(10, bid);

            case Trumps.One:
            case Trumps.Seven:
                return 10 + 10 * Math.Min(9, bid);

            case Trumps.None:
                return 30 + 10 * Math.Min(7, bid);
        }

        // 0, 2, 3, 4, 5, 6, 8, 9
        return 20 + 10 * Math.Min(6, bid);
    }
}
