using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles
{
    public class Laser : Projectile
    {
        
        public Laser(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parent) 
            : base(game, position, rotation, color, parent, 1)
        {
            Texture = TextureManager.Instance.LaserTextures[(int) color];
        }

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
