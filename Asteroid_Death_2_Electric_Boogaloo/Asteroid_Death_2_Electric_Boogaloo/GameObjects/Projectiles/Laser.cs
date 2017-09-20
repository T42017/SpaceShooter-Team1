using System;
using Asteroid_Death_2_Electric_Boogaloo;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Laser : Projectile
    {
        
        public Laser(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parentType) : base(game, position, rotation, color, parentType, 1)
        {
            Texture = TextureManager.Instance.LaserTextures[(int) color];
        }

        protected override Type GetClassType()
            return typeof(Laser);
        public override void Update()
        {
            DieIfOutSideMap();

            Speed = Forward() * 11;
            AccelerateForward(9);
            Move();

            base.Update();
        }
    }
}
