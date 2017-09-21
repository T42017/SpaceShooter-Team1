using System;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles
{
    public class Missile : Projectile
    {       
        #region Public constructors
        public Missile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type paren) 
            : base(game, position, rotation, color, paren, 3)
        {
            Texture = TextureManager.Instance.MissileTextures[(int)color];
        }
        #endregion

        #region Protected overrides
        protected override Type GetClassType()
        {
            return typeof(Missile);
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            DieIfOutSideMap();

            Speed = Forward() * 11;
            AccelerateForward(9);
            Move();

            base.Update();
        } 
        #endregion
    }
}