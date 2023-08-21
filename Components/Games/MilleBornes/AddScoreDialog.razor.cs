using BlazorScoreCards.Client.Store.Games.MilleBornes;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorScoreCards.Components.Games.MilleBornes;

public partial class AddScoreDialog
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [EditorRequired]
    [Parameter]
    public string PlayerName { get; set; } = string.Empty;

    private int Distance { get; set; } = 0;

    private void Cancel() => MudDialog.Cancel();

    private bool PlayedDrivingAce { get; set; }
    private bool PlayedExtraTank { get; set; }
    private bool PlayedPunctureProof { get; set; }
    private bool PlayedRightOfWay { get; set; }

    private bool CFDrivingAce { get; set; }
    private bool CFExtraTank { get; set; }
    private bool CFPunctureProof { get; set; }

    private bool Shutout { get; set; }
    private bool SafeTrip { get; set; }
    private bool DelayedAction { get; set; }

    private readonly string[] TickLabels = new string[]
    {
        "0", "", "", "",
        "", "", "", "",
        "", "", "250", "",
        "", "", "", "",
        "", "", "", "",
        "500", "", "", "",
        "", "", "", "",
        "", "", "750", "",
        "", "", "", "",
        "", "", "", "",
        "1000",
    };

    private bool Invert(bool value)
    {
        return !value;
    }

    private void UpdateScore()
    {
        var score = Distance;

        if (PlayedDrivingAce)
        {
            score += 100;
            if (CFDrivingAce)
            {
                score += 300;
            }
        }

        if (PlayedExtraTank)
        {
            score += 100;
            if (CFExtraTank)
            {
                score += 300;
            }
        }

        if (PlayedPunctureProof)
        {
            score += 100;
            if (CFPunctureProof)
            {
                score += 300;
            }
        }

        if (PlayedRightOfWay)
        {
            score += 100;
        }

        if (PlayedDrivingAce && PlayedExtraTank && PlayedPunctureProof && PlayedRightOfWay)
        {
            score += 300;
        }

        if (Distance == 1000)
        {
            score += 400;

            if (Shutout)
            {
                score += 500;
            }

            if (SafeTrip)
            {
                score += 300;
            }

            if (DelayedAction)
            {
                score += 300;
            }
        }

        Dispatcher.Dispatch(new UpdateScoreAction(PlayerName, score));

        MudDialog.Close();
    }
}
