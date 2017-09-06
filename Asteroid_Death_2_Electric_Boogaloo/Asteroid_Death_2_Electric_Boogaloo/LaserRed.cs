using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class LaserRed : GameObject
    {
        public LaserRed(Game game, Vector2 position, float rotation) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        protected override void LoadContent()
        {
            LoadTexture("laserRed");
            MaxSpeed = 200;
        }

        public override void Update(GameTime gameTime)
        {
            Speed = Forward() * 10;
            Accelerate(3);
            Move();

            if (Position.X < 0 || Position.Y < 0 || Position.X > Globals.ScreenWidth ||
                Position.Y > Globals.ScreenHeight)
            {
                Game.Components.Remove(this);
            }
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject);
            if (collides) Game.Components.Remove(this);
            return collides;
        }
    }
}
