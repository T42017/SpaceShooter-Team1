using Game1.Enums;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    public class PowerupHealth : Powerup
    {
        #region Public constructors
        public PowerupHealth(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Health, 900) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player) { }

        public override void DoEffect(Player player)
        {
            player.Health = player.Health + 1;
        } 
        #endregion
    }
}