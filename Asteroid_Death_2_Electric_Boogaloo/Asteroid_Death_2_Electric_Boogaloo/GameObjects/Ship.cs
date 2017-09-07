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
        
        protected Ship(AsteroidsGame game) : base(game)
        {
        }
        
        public void Shoot()
        {
            Point shipCenterPoint = new Point((int)(Position.X), (int)(Position.Y));
            Point shootPoint = new Point((int) (Position.X + Width / 2), (int) (Position.Y + (Height / 4 * (ShootLefCannon ? 1 : -1))));

            shootPoint = RotatePoint(shootPoint, shipCenterPoint, Rotation);

            Laser laser = new Laser(Game, new Vector2(shootPoint.X, shootPoint.Y), Rotation, Laser.Color.Red);
            laser.LoadContent();

            Game.GameObjectManager.GameObjects.Add(laser);
            ShootLefCannon = !ShootLefCannon;
        }

        public Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInRadians)
        {
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            return new Point
            {
                X = (int)
                (cosTheta * (pointToRotate.X - centerPoint.X) -
                 sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (int)
                (sinTheta * (pointToRotate.X - centerPoint.X) +
                 cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject);
            if (collides) Game.GameObjectManager.GameObjects.Remove(this);
            return collides;
        }
    }
}
