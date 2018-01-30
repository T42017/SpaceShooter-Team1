using Game1.Enums;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    public class PowerupMissile : Powerup
    {
        #region Public constructors
        public PowerupMissile(AsteroidsGame game, Vector2 position) : base(game, position, PowerupType.Missile, 900) { }
        #endregion

        #region Public overrides
        public override void Remove(Player player)
        {
            player.Weapon = new Weapon(Game, Weapon.Type.Laser, Weapon.Color.Red);
        }

        public override void DoEffect(Player player)
        {
            player.Weapon = new Weapon(Game, Weapon.Type.Missile, Weapon.Color.Red);
        } 
        #endregion
    }
}