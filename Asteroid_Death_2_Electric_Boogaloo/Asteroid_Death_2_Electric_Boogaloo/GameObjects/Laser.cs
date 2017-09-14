using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Laser : GameObject
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        }

        private Color color;

        public Type ParentType { get; set; }

        public Laser(AsteroidsGame game, Vector2 position, float rotation, Color color) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            Texture = TextureManager.Instance.LaserTextures[(int) color];
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

            base.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType() && !(otherGameObject is Laser);
            if (collides)
            {
                var explosion = new Explosion(Game, Position);
                if (explosion.NoExplosionsNearby())
                    Game.GameObjectManager.Explosions.Add(new Explosion(Game, Position));
            }
            return collides;
        }
    }
}
