using Fluxor;

namespace BlazorScoreCards.Client.Store.Orientation;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record OrientationState(Orientations Orientation)
{
    public static OrientationState CreateInitialState() => new(Orientations.Desktop);
}
