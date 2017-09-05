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
        
        Menu = 0,
        loading = 1,
        ingame = 2,
        paused = 4,
        gameover= 8,

        All = loading | ingame | Menu | paused | gameover
    }
}
