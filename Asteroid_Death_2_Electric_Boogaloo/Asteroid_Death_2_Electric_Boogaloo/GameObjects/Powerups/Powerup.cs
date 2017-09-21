using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Powerup : GameObject
    {
        public PowerupType PowerupType { get; }

        public float Timer { get; set; }
        
        public Powerup(AsteroidsGame game, Vector2 position, PowerupType powerupType) : this(game, powerupType)
        {
            Position = position;
            Speed = new Vector2(
                (float)Globals.RNG.NextDouble(),
                (float)Globals.RNG.NextDouble()
            );
        }

        public Powerup(AsteroidsGame game, PowerupType powerupType) : base(game)
        {
            PowerupType = powerupType;
            Texture = TextureManager.Instance.PowerUpTextures[(int)powerupType];

            if (powerupType == PowerupType.Missile)
            {
                Timer = 900;
            }
            else if (powerupType == PowerupType.Mariostar)
            {
                Timer = 900;
            }
            
        }

        public abstract void Remove(Player player);
        
        public abstract void DoEffect(Player player);

        public override void Update()
        {
            //var previousWeapon = player.Weapon = new Weapon(Game, Weapon.Type.Laser, Weapon.Color.Blue);
            Timer--;
            //if (Timer == 0) player.Weapon = previousWeapon;
            base.Update();
        }
    }
}
