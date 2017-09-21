using System;
using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.Factories;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    class PowerupRandom : Powerup
    {
        #region Private fields
        private int _powerupNumber;
        #endregion

        #region Public constructors
        public PowerupRandom(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Random, 0) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player)
        {
        }

        public override void DoEffect(Player player)
        {
            PowerupFactory factory = Game.GameObjectManager.PowerupFactory;
            player.AddPowerUp(factory.GetRandomPowerup());
        } 
        #endregion
    }
}