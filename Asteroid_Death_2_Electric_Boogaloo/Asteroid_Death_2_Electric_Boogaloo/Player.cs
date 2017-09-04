using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Player : Ship
    {
        public Player(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
        }
    }
}
