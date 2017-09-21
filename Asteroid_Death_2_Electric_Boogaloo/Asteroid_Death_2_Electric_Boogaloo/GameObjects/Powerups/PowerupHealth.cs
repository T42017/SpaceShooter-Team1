﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupHealth : Powerup
    {
        public PowerupHealth(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Health, 900)
        {
        }

        public override void Remove(Player player)
        {
        }

        public override void DoEffect(Player player)
        {
            player.Health = player.Health + 1;
        }
    }
}
