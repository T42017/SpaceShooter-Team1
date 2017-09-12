﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Ship : GameObject
    {
        private bool ShootLefCannon = false;
        private DateTime _timeSenceLastShot = DateTime.Today;
        protected int ShootingSpeed = 100;
        private SoundEffect pewEffect;

        protected Ship(AsteroidsGame game) : base(game)
        {
            pewEffect = Game.Content.Load<SoundEffect>("Blaster");
        }
        
        public void Shoot(Type parentType)
        {
            if (!((DateTime.Now - _timeSenceLastShot).TotalMilliseconds > ShootingSpeed))
                return;

            Point shipCenterPoint = new Point((int)(Position.X), (int)(Position.Y));
            Point shootPoint = new Point((int) (Position.X + Width / 2), (int) (Position.Y + (Height / 4 * (ShootLefCannon ? 1 : -1))));

            shootPoint = MathHelper.RotateAroundPoint(shootPoint, shipCenterPoint, Rotation);

            Laser laser = new Laser(Game, new Vector2(shootPoint.X, shootPoint.Y), Rotation, Laser.Color.Red)
            {
                ParentType = parentType
            };
            pewEffect.Play();
            Game.GameObjectManager.GameObjects.Add(laser);
            ShootLefCannon = !ShootLefCannon;
            _timeSenceLastShot = DateTime.Now;
        }
    }
}
