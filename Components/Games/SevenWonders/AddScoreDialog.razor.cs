using BlazorScoreCards.Client.Store.Games.SevenWonders;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorScoreCards.Components.Games.SevenWonders;

public partial class AddScoreDialog
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [EditorRequired]
    [Parameter]
    public string PlayerName { get; set; } = string.Empty;

    private void Cancel() => MudDialog.Cancel();

    private int Military { get; set; }
    private int Money { get; set; }
    private int Wonders { get; set; }
    private int Civic { get; set; }
    private int Commerce { get; set; }
    private int Guild { get; set; }
    private int Science { get; set; }

    private int Total { get => Military + Money + Wonders + Civic + Commerce + Guild + Science; }

    private void UpdateScore()
    {
        Dispatcher.Dispatch(new UpdateScoreAction(PlayerName, Total));
        MudDialog.Close();
    }
}
