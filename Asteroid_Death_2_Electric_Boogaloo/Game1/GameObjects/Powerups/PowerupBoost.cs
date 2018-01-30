using Game1.Enums;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    public class PowerupBoost : Powerup
    {
        #region Public constructors
        public PowerupBoost(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Boost, 0) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player) {}

        public override void DoEffect(Player player)
        {
            player.Boost = player.Boost + 60;
        } 
        #endregion
    }
}