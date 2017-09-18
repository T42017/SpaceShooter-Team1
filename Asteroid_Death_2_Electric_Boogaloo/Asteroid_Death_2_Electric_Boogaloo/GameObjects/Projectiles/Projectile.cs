using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Projectile : GameObject
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        }

        protected Weapon.Color color;

        public Type ParentType { get; set; }

        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            Texture = TextureManager.Instance.LaserTextures[(int) color];
            MaxSpeed = 200;
            ParentType = parenType;
        }

        protected void DieIfOutSideMap()
        {
          if (IsOutSideLevel(Game.Level))
                IsDead = true;
        }

        public override void Update()
        {
            DieIfOutSideMap();
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
                var explosion = new Explosion(Game, otherGameObject.Position);
                if (explosion.NoExplosionsNearby())
                    Game.GameObjectManager.Explosions.Add(explosion);
            }
            return collides;
        }
    }
}
