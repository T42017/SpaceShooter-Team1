using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    [Flags]
    public enum GameState
    {
        None = 0,
        Menu = 1,
        loading = 2,
        ingame = 4,
        paused = 8,
        gameover= 16,
        highscoremenu= 32,

        All = loading | ingame | Menu | paused | gameover | highscoremenu
    }
}
