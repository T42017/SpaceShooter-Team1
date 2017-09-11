using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
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
            LoadTexture("laser" + Enum.GetName(typeof(Color), color));
            MaxSpeed = 200;
        }

        public override void LoadContent()
        {
            
        }

        public override void Update()
        {
            if (IsOutSideLevel(Game.Level))
                IsDead = true;

            Speed = Forward() * 11;
            AccelerateForward(9);
            Move();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            var collides = base.CollidesWith(otherGameObject);
            if (collides) Game.GameObjectManager.GameObjects.Remove(this);
            return collides;
        }
    }
}
