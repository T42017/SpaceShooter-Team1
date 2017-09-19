using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Missile : Projectile
    {
        
        public Missile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType) : base(game, position, rotation, color, parenType, 3)
        {
            Texture = TextureManager.Instance.MissileTextures[(int) color];
        }

        protected override Type GetClassType()
        {
            return typeof(Missile);
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
