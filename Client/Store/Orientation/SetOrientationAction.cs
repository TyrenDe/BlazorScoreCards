namespace BlazorScoreCards.Client.Store.Orientation;

public enum Orientations : int
{
    Desktop = 0,
    Portrait = 1,
    Landscape = 2,
}

public record SetOrientationAction(Orientations Orientation);
