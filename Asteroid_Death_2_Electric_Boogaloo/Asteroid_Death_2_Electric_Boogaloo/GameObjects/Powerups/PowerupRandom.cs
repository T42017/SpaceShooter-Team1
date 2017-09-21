using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.Factorys;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupRandom : Powerup
    {
        private int powerupNumber;

        public PowerupRandom(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Random, 0)
        {
        }

        public override void Remove(Player player)
        {
        }

        public override void DoEffect(Player player)
        {
            PowerupFactory factory = Game.GameObjectManager.PowerupFactory;
            player.AddPowerUp(factory.GetRandomPowerup());
        }
    }
}