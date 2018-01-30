using Game1.Enums;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    public class PowerupMariostar : Powerup
    {
        #region Public constructors
        public PowerupMariostar(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Mariostar, 900) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player) { }

        public override void DoEffect(Player player) { } 
        #endregion
    }
}