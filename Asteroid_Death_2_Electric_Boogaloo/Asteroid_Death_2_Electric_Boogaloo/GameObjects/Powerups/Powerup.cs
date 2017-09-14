using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Powerup : GameObject
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

            Texture = TextureManager.Instance.PowerUpTextures[(int) powerupType];
        }

        public abstract void DoEffect(Player player);
        
        public override void LoadContent()
        {

        }
    }
}
