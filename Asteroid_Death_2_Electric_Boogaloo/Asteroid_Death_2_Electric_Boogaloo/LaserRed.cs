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
        public LaserRed(Game game, Vector2 position, Vector2 direction, float rotation) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        protected override void LoadContent()
        {
            LoadTexture("laserRed");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Speed = Forward() * 10;
            Accelerate(3);
            Move();
            base.Update(gameTime);
        }
    }
}
