using BlazorScoreCards.Client.Store.Theme;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorScoreCards.Shared;

public partial class MainLayout
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = default!;

    private static readonly LayoutProperties _DefaultLayoutProperties = new LayoutProperties()
    {
        AppbarHeight = "0px !important",
    };

    private MudTheme DefaultTheme { get; init; } = new MudTheme()
    {
        LayoutProperties = _DefaultLayoutProperties
    };
}
