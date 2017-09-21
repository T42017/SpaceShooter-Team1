using System;
using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Ship : GameObject
    {
        #region Private fields
        private bool _shootLefCannon;
        private DateTime _timeSenceLastShot = DateTime.Today;
        #endregion

        #region Protected fields
        protected int ShootingSpeed = 100;
        #endregion

        #region Public properties
        public Weapon Weapon { get; set; }
        public int Health { get; set; }
        public int BaseHealth { get; set; }
        public int Boost { get; set; } = 1;
        #endregion

        #region Protected constructors
        protected Ship(AsteroidsGame game, int baseHealth) : base(game)
        {
            BaseHealth = baseHealth;
            Health = baseHealth;
        }

        protected Ship(AsteroidsGame game, Weapon weapon, int baseHealth) : base(game)
        {
            BaseHealth = baseHealth;
            Health = baseHealth;
            this.Weapon = weapon;
        }
        #endregion

        #region Public methods
        public bool IsWeaponOverheated()
        {
            return !((DateTime.Now - _timeSenceLastShot).TotalMilliseconds > ShootingSpeed);
        }

        public void Shoot(Type parentType)
        {
            Vector2 shipCenterPoint = new Vector2((int)(Position.X), (int)(Position.Y));
            Vector2 shootPoint = new Vector2((int)(Position.X + Width / 2), (int)(Position.Y + (Height / 4 * (_shootLefCannon ? 1 : -1))));

            shootPoint = MathHelper.RotateAroundPoint(shootPoint, shipCenterPoint, Rotation);

            Projectile projectile = Weapon.GetProjectile(shootPoint, Rotation, parentType);
            Game.GameObjectManager.Add(projectile);

            _shootLefCannon = !_shootLefCannon;
            _timeSenceLastShot = DateTime.Now;
        } 
        #endregion
    }
}