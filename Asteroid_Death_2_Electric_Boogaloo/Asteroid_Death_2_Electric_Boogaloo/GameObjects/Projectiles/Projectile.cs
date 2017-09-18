using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles
{
    public abstract class Projectile : GameObject
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        }
        public Type ParentType { get; set; }
        public int Damage;

        protected Weapon.Color color;

        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType, int damage) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            MaxSpeed = 200;
            ParentType = parenType;
            Damage = damage;
            Texture = TextureManager.Instance.LaserTextures[(int) color];
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
            return base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType();
        }
    }
}
