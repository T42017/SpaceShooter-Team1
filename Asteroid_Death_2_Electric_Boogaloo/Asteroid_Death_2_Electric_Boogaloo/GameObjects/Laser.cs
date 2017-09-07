using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Laser : GameObject
    {
        public enum Color
        {
            Red,
            Green,
            Blue
        }

        private Color color;

        public Laser(AsteroidsGame game, Vector2 position, float rotation, Color color) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
        }

        public override void LoadContent()
        {
            LoadTexture("laser" + Enum.GetName(typeof(Color), color));
            MaxSpeed = 200;
        }

        public override void Update()
        {
            if (IsOutSideLevel(Game.Level))
                IsDead = true;
            Speed = Forward() * 10;
            AccelerateForward(3);
            Move();
        }
    }
}
