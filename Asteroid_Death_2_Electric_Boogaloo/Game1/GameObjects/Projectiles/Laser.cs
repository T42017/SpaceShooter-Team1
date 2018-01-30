using System;
using Game1.Managers;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Projectiles
{
    public class Laser : Projectile
    {      
        #region Public constructors
        public Laser(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parentType) : base(game, position, rotation, color, parentType, 1)
        {
            Texture = TextureManager.Instance.LaserTextures[(int)color];
        }
        #endregion

        #region Protected overrides
        protected override Type GetClassType()
        {
            return typeof(Laser);
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