using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Weapon
    {
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

        public Type WeaponType;
        public Color WeaponColor;

        private AsteroidsGame _game;

        public Weapon(AsteroidsGame game, Type type, Color color)
        {
            WeaponType = type;
            WeaponColor= color;
            _game = game;
        }

        public Projectile GetProjectile(Vector2 Position, float rotation, System.Type parentType)
        {
            Projectile projectile = null;

            if (WeaponType == Type.Laser)
                projectile = new Laser(_game, Position, rotation, WeaponColor, parentType);
            else if (WeaponType == Type.Missile)
                projectile = new Missile(_game, Position, rotation, WeaponColor, parentType);

            return projectile;
        }
    }
}
