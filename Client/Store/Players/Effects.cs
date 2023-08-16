using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorScoreCards.Client.Store.Players;

public class Effects
{
    private const string PlayersKey = "players";

    private readonly ILocalStorageService _LocalStorageService;

    public Effects(ILocalStorageService localStorageService)
    {
        _LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task EffectPlayersState(LoadInitialStateAction action, IDispatcher dispatcher)
    {
        var players = await LoadPlayersAsync();

        dispatcher.Dispatch(new LoadPlayersAction(players));
    }

    [EffectMethod]
    public async Task EffectPlayersState(AddPlayerAction action, IDispatcher dispatcher)
    {
        var players = await LoadPlayersAsync();

        if (players.Any(p => string.Equals(p.PlayerName, action.PlayerName, StringComparison.OrdinalIgnoreCase)))
        {
            // Player already exists, set to selected
            var updatedList = new List<Player>(players);
            var indexOf = updatedList.FindIndex(p => string.Equals(p.PlayerName, action.PlayerName, StringComparison.OrdinalIgnoreCase));
            updatedList[indexOf] = updatedList[indexOf] with { IsSelected = true };

            players = updatedList;
        }
        else
        {
            players = players.Concat(new[] { new Player(action.PlayerName, true) }).OrderBy(p => p.PlayerName);
        }

        await UpdateAndDispatchAsync(dispatcher, players);
    }


    [EffectMethod]
    public async Task EffectPlayersState(DeletePlayerAction action, IDispatcher dispatcher)
    {
        var players = await LoadPlayersAsync();

        players = players.Where(p => !string.Equals(p.PlayerName, action.PlayerName));

        await UpdateAndDispatchAsync(dispatcher, players);
    }

    [EffectMethod]
    public async Task EffectPlayersState(TogglePlayerAction action, IDispatcher dispatcher)
    {
        var players = await LoadPlayersAsync();

        var updatedList = new List<Player>(players);
        var indexOf = updatedList.FindIndex(p => string.Equals(p.PlayerName, action.PlayerName, StringComparison.OrdinalIgnoreCase));
        updatedList[indexOf] = updatedList[indexOf] with { IsSelected = !updatedList[indexOf].IsSelected };

        await UpdateAndDispatchAsync(dispatcher, updatedList);
    }

    private async Task<IEnumerable<Player>> LoadPlayersAsync()
    {
        var players = await _LocalStorageService.GetItemAsync<IEnumerable<Player>>(PlayersKey);
        if (players == null)
        {
            players = Array.Empty<Player>();
        }

        return players;
    }

    private async Task UpdateAndDispatchAsync(IDispatcher dispatcher, IEnumerable<Player> players)
    {
        await _LocalStorageService.SetItemAsync(PlayersKey, players);
        dispatcher.Dispatch(new LoadPlayersAction(players));
    }

}
