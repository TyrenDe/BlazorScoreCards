using BlazorScoreCards.Client.Store.Games.Generic;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace BlazorScoreCards.Components.Games.Generic;

public partial class AddScoreDialog
{
    private MudTextField<string> textField = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [EditorRequired]
    [Parameter]
    public string PlayerName { get; set; } = string.Empty;

    private string Score { get; set; } = string.Empty;

    private void Cancel() => MudDialog.Cancel();

    private async Task UpdateScore()
    {
        await textField.BlurAsync();
        await textField.FocusAsync();
        if (!string.IsNullOrWhiteSpace(Score) && int.TryParse(Score, out var adjustment))
        {
            Dispatcher.Dispatch(new UpdateScoreAction(PlayerName, adjustment));
            MudDialog.Close();
        }
    }

    private async Task<bool> HandleKeyDown(KeyboardEventArgs e)
    {
        if (string.Equals(e.Key, "Enter", StringComparison.OrdinalIgnoreCase))
        {
            await UpdateScore();
            return false;
        }

        return true;
    }
}
