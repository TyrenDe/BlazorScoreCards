namespace BlazorScoreCards.Client.Store.Games.Yahtzee;

public record UpdateScoreAction(YahtzeeRanks Rank, int? Score);
