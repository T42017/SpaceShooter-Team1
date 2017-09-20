﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupRandom : Powerup
    {
        public PowerupRandom(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Random)
        {

        }

        public override void Remove(Player player)
        {

        }

        public override void DoEffect(Player player)
        {
            switch((int)Globals.RNG.Next(Enum.GetNames(typeof(PowerupType)).Length))
            {

                case 1:
                    player.Weapon = new Weapon(Game, Weapon.Type.Missile, Weapon.Color.Red);
                    break;

                case 2:
                    player.Health = player.Health + 1;
                    break;

                case 3:
                    player.Boost = player.Boost + 360;
                    break;

                case 4:
                    break;

                case 5:
                    player.Health = player.Health / 2;
                    break;
            }
        }

    }
}
