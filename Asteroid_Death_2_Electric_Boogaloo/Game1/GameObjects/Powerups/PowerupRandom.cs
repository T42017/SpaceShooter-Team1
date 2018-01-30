using Game1.Enums;
using Game1.Factories;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    class PowerupRandom : Powerup
    {
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