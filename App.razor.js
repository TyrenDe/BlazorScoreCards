export function getOrientation() {
    if (screen.orientation.type.startsWith("portrait-")) {
        if (screen.width > 1000) {
            return 0; // Destkop
        }

        return 1; // Portrait
    }

    if (screen.height > 1000) {
        return 0; // Destkop
    }

    return 2; // Landscape
}

export async function registerForOrientationChangedEvents(component) {
    screen.orientation.addEventListener("change", async (event) => {
        const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
        var exports = await getAssemblyExports("BlazorScoreCards.dll");
        exports.BlazorScoreCards.App.OrientationChanged(component, getOrientation());
    });
}
