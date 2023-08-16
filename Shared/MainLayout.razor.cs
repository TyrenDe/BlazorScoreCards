using BlazorScoreCards.Client.Store.Theme;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorScoreCards.Shared;

public partial class MainLayout
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = default!;
}
