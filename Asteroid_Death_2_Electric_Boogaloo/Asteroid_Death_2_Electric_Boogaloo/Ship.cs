using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Ship : GameObject
    {
        
        public Ship(Game game) : base(game)
        {
            
        }

        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
        }
    }
}
