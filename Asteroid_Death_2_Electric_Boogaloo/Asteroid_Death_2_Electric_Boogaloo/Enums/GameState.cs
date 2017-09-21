using System;

namespace Asteroid_Death_2_Electric_Boogaloo.Enums
{
    [Flags]
    public enum GameState
    {
        None = 0,
        Menu = 1,
        Loading = 2,
        InGame = 4,
        Paused = 8,
        GameOver= 16,
        HighscoreMenu= 32,

        All = Loading | InGame | Menu | Paused | GameOver | HighscoreMenu
    }
}
