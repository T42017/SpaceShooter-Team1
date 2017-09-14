using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Powerup : GameObject
    {
        public PowerupType PowerupType { get; }

        public Powerup(AsteroidsGame game, Vector2 position, PowerupType powerupType) : base(game)
            {
            Position = position;
            Speed = new Vector2(
                (float)Globals.RNG.NextDouble(),
                (float)Globals.RNG.NextDouble()
            );
            PowerupType = powerupType;
            SetAppropriateTexture();
        }

        /// <summary>
        /// Calls LoadTexture from parent with a filename according to meteor's size and colour.
        /// </summary>
        private void SetAppropriateTexture()
        {
            string fileSuffix = string.Empty;
            
            switch (PowerupType)
            {
                case PowerupType.Missile:
                    fileSuffix = "Missile";
                    break;
                case PowerupType.Health:
                    fileSuffix = "Brown";
                    break;
                case PowerupType.Lightsaber:
                    fileSuffix = "Grey";
                    break;
                case PowerupType.Mariostar:
                    fileSuffix = "Brown";
                    break;
                case PowerupType.ImprovedLaser:
                    fileSuffix = "Grey";
                    break;
                case PowerupType.Random:
                    fileSuffix = "Brown";
                    break;
        }

        string fileName = $"Poweruptype{fileSuffix}";
        LoadTexture(fileName);
        }

        public override void LoadContent()
        {

        }

    }
}
