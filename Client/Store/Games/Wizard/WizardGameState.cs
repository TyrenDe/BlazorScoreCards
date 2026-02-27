using Fluxor;
using System.Collections.Generic;
using System.Linq;

namespace BlazorScoreCards.Client.Store.Games.Wizard;

public enum WizardTrump
{
    Hearts,
    Clubs,
    Spades,
    Diamonds,
    NoTrump,
}

public record WizardHandPlayerResult(string PlayerName, int Bid, int Tricks, int Score);

public record WizardHandResult(
    int HandNumber,
    WizardTrump Trump,
    string DealerName,
    IReadOnlyList<WizardHandPlayerResult> PlayerResults);

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record WizardGameState(
    bool IsLoading,
    IReadOnlyList<WizardHandResult> Hands)
{
    public static WizardGameState CreateInitialState() => new(IsLoading: true, Hands: []);

    public int GetTotalScore(string playerName)
    {
        return Hands
            .SelectMany(h => h.PlayerResults)
            .Where(r => r.PlayerName == playerName)
            .Sum(r => r.Score);
    }

    public int GetRunningScore(string playerName, int throughHand)
    {
        return Hands
            .Where(h => h.HandNumber <= throughHand)
            .SelectMany(h => h.PlayerResults)
            .Where(r => r.PlayerName == playerName)
            .Sum(r => r.Score);
    }

    public int CurrentHandNumber => Hands.Count + 1;

    public static int GetMaxHands(int playerCount) => playerCount switch
    {
        3 => 20,
        4 => 15,
        5 => 12,
        6 => 10,
        _ => 10,
    };

    public static WizardTrump GetSuitForHand(int handNumber)
    {
        // Cycle: Hearts, Clubs, Spades, Diamonds, NoTrump
        return ((handNumber - 1) % 5) switch
        {
            0 => WizardTrump.Hearts,
            1 => WizardTrump.Clubs,
            2 => WizardTrump.Spades,
            3 => WizardTrump.Diamonds,
            _ => WizardTrump.NoTrump,
        };
    }

    public static string GetSuitIcon(WizardTrump trump) => trump switch
    {
        WizardTrump.Hearts => "♥",
        WizardTrump.Clubs => "♣",
        WizardTrump.Spades => "♠",
        WizardTrump.Diamonds => "♦",
        WizardTrump.NoTrump => "⊘",
        _ => "",
    };

    public static string GetSuitColor(WizardTrump trump) => trump switch
    {
        WizardTrump.Hearts => "red",
        WizardTrump.Diamonds => "red",
        _ => "inherit",
    };

    public static int CalculateScore(int bid, int tricks)
    {
        if (bid == tricks)
        {
            return 20 + (10 * bid);
        }

        return -10 * System.Math.Abs(bid - tricks);
    }
}
