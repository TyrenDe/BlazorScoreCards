using Fluxor;

namespace BlazorScoreCards.Client.Store.Orientation;

public static class Reducers
{
    [ReducerMethod]
    public static OrientationState ReduceOrientationState(OrientationState state, SetOrientationAction action)
    {
        return state with { Orientation = action.Orientation };
    }
}
