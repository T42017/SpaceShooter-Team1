﻿using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    internal class PowerupMariostar : Powerup
    {
        public PowerupMariostar(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Mariostar)
        {
        }

        public override void Remove(Player player)
        {
        }

        public override void DoEffect(Player player)
        {
        }
    }
}