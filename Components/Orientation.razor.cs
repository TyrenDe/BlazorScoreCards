using BlazorScoreCards.Client.Store.Orientation;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorScoreCards.Components;

public partial class Orientation
{
    [EditorRequired]
    [Parameter]
    public RenderFragment LandscapeContent { get; set; } = default!;

    [EditorRequired]
    [Parameter]
    public RenderFragment PortraitContent { get; set; } = default!;

    [EditorRequired]
    [Parameter]
    public Orientations DesktopDefault { get; set; } = default!;

    public RenderFragment GetDesktopLayout()
    {
        return DesktopDefault switch
        {
            Orientations.Portrait => PortraitContent,
            Orientations.Landscape => LandscapeContent,
            _ => throw new ArgumentException("DesktopDefault layout cannot be Desktop"),
        };
    }

    [Inject]
    private IState<OrientationState> OrientationState { get; set; } = default!;
}
