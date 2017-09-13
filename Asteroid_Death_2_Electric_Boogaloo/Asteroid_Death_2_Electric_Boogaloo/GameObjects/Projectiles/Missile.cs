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

        

        public Missile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color) : base(game, position, rotation, color)
        {
            LoadTexture("Missile" + Enum.GetName(typeof(Weapon.Color), color));
        }

        public override void LoadContent()
        {
            
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
