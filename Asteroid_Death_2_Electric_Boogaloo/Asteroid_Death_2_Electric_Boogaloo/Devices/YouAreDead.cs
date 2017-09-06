using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class YouAreDead : AstroidsComponent
    {
        public YouAreDead(Game game) : base(game)
        {
            

            DrawableStates = GameState.gameover;
            UpdatableStates = GameState.gameover;
        }
    }
}
