﻿using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Weapon
    {
        #region Public enums
        public enum Type
        {
            Laser,
            Missile
        }

        public enum Color
        {
            Red,
            Blue,
            Green
        }
        #endregion

        #region Private fields
        private AsteroidsGame _game;
        #endregion

        #region Public properties
        public Type WeaponType;
        public Color WeaponColor;
        #endregion

        #region Public constructors
        public Weapon(AsteroidsGame game, Type type, Color color)
        {
            WeaponType = type;
            WeaponColor = color;
            _game = game;
        }
        #endregion

        #region Public methods
        public Projectile GetProjectile(Vector2 Position, float rotation, System.Type parentType)
        {
            Projectile projectile = null;

            if (WeaponType == Type.Laser)
                projectile = new Laser(_game, Position, rotation, WeaponColor, parentType);
            else if (WeaponType == Type.Missile)
                projectile = new Missile(_game, Position, rotation, WeaponColor, parentType);

            return projectile;
        } 
        #endregion
    }
}