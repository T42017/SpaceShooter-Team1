using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class LaserRed : GameObject
    {
        public LaserRed(Game game, Vector2 position, Vector2 direction) : base(game)
        {
            X = (int) position.X;
            Y = (int) position.Y;
        }

        protected override void LoadContent()
        {
            LoadTexture("laserRed");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Y += 10;
            base.Update(gameTime);
        }
    }
}
