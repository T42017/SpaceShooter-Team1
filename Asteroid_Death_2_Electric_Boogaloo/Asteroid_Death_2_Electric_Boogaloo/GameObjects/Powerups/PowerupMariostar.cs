using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
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