using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public abstract class Ship : GameObject
    {
        private bool ShootLefCannon = false;
        private DateTime _timeSenceLastShot = DateTime.Today;
        protected int ShootingSpeed = 100;

        protected Ship(AsteroidsGame game) : base(game)
        {
            
        }
        
        public void Shoot()
        {
            if (!((DateTime.Now - _timeSenceLastShot).TotalMilliseconds > ShootingSpeed))
                return;
            Point shipCenterPoint = new Point((int)(Position.X), (int)(Position.Y));
            Point shootPoint = new Point((int) (Position.X + Width / 2), (int) (Position.Y + (Height / 4 * (ShootLefCannon ? 1 : -1))));

            shootPoint = Physic.RotateAroundPoint(shootPoint, shipCenterPoint, Rotation);

            Laser laser = new Laser(Game, new Vector2(shootPoint.X, shootPoint.Y), Rotation, Laser.Color.Red);
            laser.LoadContent();

            Game.GameObjectManager.GameObjects.Add(laser);
            ShootLefCannon = !ShootLefCannon;
            _timeSenceLastShot = DateTime.Now;
        }
    }
}
