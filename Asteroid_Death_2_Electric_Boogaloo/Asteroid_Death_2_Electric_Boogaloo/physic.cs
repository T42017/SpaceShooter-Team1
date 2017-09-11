using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Physic
    {

        public static float DegreesToRadians(int angle)
        {
            return (float) (Math.PI * angle / 180.0);
        }

        public static Point RotateAroundPoint(Point pointToRotate, Point centerPoint, double angleInRadians)
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

        public static float LookAt(Vector2 point, Vector2 lookAtpoint)
        {
            return (float) Math.Atan2(point.Y - lookAtpoint.Y, point.X - lookAtpoint.X) - DegreesToRadians(180);
        }

    }
}
