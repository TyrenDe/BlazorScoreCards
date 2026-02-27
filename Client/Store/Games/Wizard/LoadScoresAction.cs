using System.Collections.Generic;

namespace BlazorScoreCards.Client.Store.Games.Wizard;

public record LoadScoresAction(IReadOnlyList<WizardHandResult> Hands);
