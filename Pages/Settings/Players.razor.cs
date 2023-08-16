using BlazorScoreCards.Client.Store.Players;
using BlazorScoreCards.Components.Settings.Players;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorScoreCards.Pages.Settings;

public partial class Players
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private IState<PlayersState> PlayersState { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    private void ShowAddPlayerDialog()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = DialogService.Show<AddPlayerDialog>("Add Player", options);
    }

    private void DeletePlayer(string playerName)
    {
        Dispatcher.Dispatch(new DeletePlayerAction(playerName));
    }

    private void TogglePlayer(string playerName)
    {
        Dispatcher.Dispatch(new TogglePlayerAction(playerName));
    }
}
