using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles
{
    public class Missile : Projectile
    {

        public Missile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type paren) 
            : base(game, position, rotation, color, paren, 3)
        {
            Texture = TextureManager.Instance.MissileTextures[(int) color];
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
