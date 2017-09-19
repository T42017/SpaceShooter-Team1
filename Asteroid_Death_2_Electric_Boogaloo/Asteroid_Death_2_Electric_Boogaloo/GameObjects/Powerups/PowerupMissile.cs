using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupMissile : Powerup
    {
        public PowerupMissile(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Missile)
        {

        }

        public override void DoEffect(Player player)
        {
            player.Weapon=new Weapon(Game, Weapon.Type.Missile, Weapon.Color.Red);
        }
    }
}
