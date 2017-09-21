using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.Enums;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    public class PowerupBoost : Powerup
    {
        #region Public constructors
        public PowerupBoost(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Boost, 900) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player) {}

        public override void DoEffect(Player player)
        {
            player.Boost = player.Boost + 120;
        } 
        #endregion
    }
}