﻿using System;
using Microsoft.Xna.Framework;

namespace Game1
{
    class MathHelper
    {
        #region Public static methods
        public static float DegreesToRadians(int angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }

        public static Vector2 RotateAroundPoint(Vector2 pointToRotate, Vector2 centerPoint, double angleInRadians)
        {
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            return new Vector2()
            {
                X = (int)(cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (int)(sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public static float LookAt(Vector2 point, Vector2 lookAtpoint)
        {
            return (float)Math.Atan2(point.Y - lookAtpoint.Y, point.X - lookAtpoint.X) - DegreesToRadians(180);
        } 
        #endregion
    }
}