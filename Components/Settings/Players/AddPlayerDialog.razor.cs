using BlazorScoreCards.Client.Store.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace BlazorScoreCards.Components.Settings.Players;

public partial class AddPlayerDialog
{
    private MudTextField<string> textField = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    private string PlayerName { get; set; } = string.Empty;

    private void Cancel() => MudDialog.Cancel();

    private async Task AddPlayer()
    {
        await textField.BlurAsync();
        await textField.FocusAsync();
        if (!string.IsNullOrWhiteSpace(PlayerName))
        {
            Dispatcher.Dispatch(new AddPlayerAction(PlayerName));
        }

        PlayerName = string.Empty;
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (string.Equals(e.Key, "Enter", StringComparison.OrdinalIgnoreCase))
        {
            await AddPlayer();
        }
    }
}
