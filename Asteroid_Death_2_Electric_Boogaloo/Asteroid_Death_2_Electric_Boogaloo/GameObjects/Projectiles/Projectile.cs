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

        protected Weapon.Color color;

        public Type ParentType { get; set; }

        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            MaxSpeed = 200;
        }

        protected void DieIfOutSideMap()
        {
            if (IsOutSideLevel(Game.Level))
                IsDead = true;
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            return base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType();
        }
    }
}
