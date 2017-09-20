using System;
using System.CodeDom;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Ship : GameObject
    {
        public Weapon Weapon;
        private SoundEffect _pewEffect;
        protected int ShootingSpeed = 100;

        private bool ShootLefCannon = false;
        private DateTime _timeSenceLastShot = DateTime.Today;

        public int Health;
        public int BaseHealth;
        public int Boost = 1;
        protected Ship(AsteroidsGame game,int baseHealth) : base(game)
        {
            _pewEffect = Game.Content.Load<SoundEffect>("Deus");
            BaseHealth = baseHealth;
            Health = baseHealth;
        }

        protected Ship(AsteroidsGame game, Weapon weapon,int baseHealth) : base(game)
        {
            _pewEffect = Game.Content.Load<SoundEffect>("shot");
            BaseHealth = baseHealth;
            Health = baseHealth;
            this.Weapon = weapon;
        }

        public bool IsWeaponOverheated()
        {
            return !((DateTime.Now - _timeSenceLastShot).TotalMilliseconds > ShootingSpeed);
        }
        
        public void Shoot(Type parentType)
        {

            if (Weapon.WeaponType == Weapon.Type.Laser)
            {
                _pewEffect.Play();
            }
            if (Weapon.WeaponType == Weapon.Type.Missile)
            {
                
            }
            Vector2 shipCenterPoint = new Vector2((int)(Position.X), (int)(Position.Y));
            Vector2 shootPoint = new Vector2((int) (Position.X + Width / 2), (int) (Position.Y + (Height / 4 * (ShootLefCannon ? 1 : -1))));

            shootPoint = MathHelper.RotateAroundPoint(shootPoint, shipCenterPoint, Rotation);

            Projectile projectile = Weapon.GetProjectile(shootPoint, Rotation, parentType);
            Game.GameObjectManager.GameObjects.Add(projectile);

           
                ShootLefCannon = !ShootLefCannon;
            _timeSenceLastShot = DateTime.Now;
        }
    }
}
