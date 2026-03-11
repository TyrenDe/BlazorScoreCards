# Blazor Score Cards

A digital scorecard Progressive Web App (PWA) for tracking scores during game nights. Built with Blazor WebAssembly, the app runs entirely in the browser with no server required and can be installed on phones and tablets for offline use.

**Live app:** <https://tyrende.github.io/BlazorScoreCards/>

## Supported Games

- **Generic** -- flexible multi-player scorecard for any game
- **Qwixx** -- colored rows with lock/unlock mechanics and penalty tracking
- **Split** -- card game scorecard tracking ranks from Ace through 2
- **Yahtzee** -- full scorecard with all 13 scoring categories
- **Wizard** -- trick-taking game tracker with bid/trick recording and running totals
- **7 Wonders** -- multi-player score tracking for the board game
- **Mille Bornes** -- score tracking for the racing card game
- **Mu** -- score tracking for the card game Mu

## Features

- Player roster management with local storage persistence
- Per-game score state saved to browser local storage
- Dark mode toggle
- Responsive layout with orientation detection (portrait, landscape, desktop)
- Installable as a PWA with offline support

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Getting Started

Clone the repository:

```bash
git clone https://github.com/TyrenDe/BlazorScoreCards.git
cd BlazorScoreCards
```

Build the project:

```bash
dotnet build
```

Run the app locally:

```bash
dotnet run
```

The app will be available at the URL shown in the console output (typically `https://localhost:5001` or `http://localhost:5000`).

## Technology

- [Blazor WebAssembly](https://learn.microsoft.com/aspnet/core/blazor/) (.NET 10)
- [MudBlazor](https://mudblazor.com/) for Material Design UI components
- [Fluxor](https://github.com/mrpmorris/Fluxor) for Redux-style state management
- [Blazored.LocalStorage](https://github.com/Blazored/LocalStorage) for browser local storage
