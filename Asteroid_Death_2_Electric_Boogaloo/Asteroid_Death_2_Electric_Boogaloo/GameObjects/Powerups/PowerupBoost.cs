using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupBoost : Powerup
    {
        public PowerupBoost(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Boost)
        {

        }

        public override void Remove(Player player)
        {

        }

        public override void DoEffect(Player player)
        {
            player.Boost = player.Boost + 360;
        }
    }
}
