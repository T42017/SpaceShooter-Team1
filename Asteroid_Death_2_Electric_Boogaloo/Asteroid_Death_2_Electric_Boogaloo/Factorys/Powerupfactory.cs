using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.Factorys
{
    public class PowerupFactory
    {
        private AsteroidsGame _game;

        public PowerupFactory(AsteroidsGame game)
        {
            _game = game;
        }

        public Powerup GetRandomPowerup()
        {
            PowerupType powerupType = (PowerupType)Globals.RNG.Next(Enum.GetNames(typeof(PowerupType)).Length);

            Vector2 position = Vector2.Zero;
            while (Vector2.Distance(position, _game.GameObjectManager.Player.Position) < 1000 || position.Equals(Vector2.Zero))
                position = new Vector2(Globals.RNG.Next(_game.Level.SizeX - 1), Globals.RNG.Next(_game.Level.SizeY - 1));

            Powerup powerup = null;
            switch (powerupType)
            {
                case PowerupType.Missile:
                    powerup = new PowerupMissile(_game, position);
                    break;

                case PowerupType.Health:
                    powerup = new PowerupHealth(_game, position);
                    break;

                case PowerupType.Mariostar:
                    powerup = new PowerupMariostar(_game, position);
                    break;

                    //case PowerupType.Random:
                    //    powerup = new PowerupRandom(_game, position);
                    //    break;
            }
            return powerup;
        }
    }
}
