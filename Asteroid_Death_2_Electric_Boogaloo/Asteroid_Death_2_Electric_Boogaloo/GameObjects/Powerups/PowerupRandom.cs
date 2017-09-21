using System;
using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.Enums;

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
            if (_powerupNumber == 1)
                player.Weapon = new Weapon(Game, Weapon.Type.Laser, Weapon.Color.Red);
            //else if(powerupNumber == 4)      
        }

        public override void DoEffect(Player player)
        {
            switch ((int)Globals.RNG.Next(Enum.GetNames(typeof(PowerupType)).Length))
            {
                case 1:
                    _powerupNumber = 1;
                    player.Weapon = new Weapon(Game, Weapon.Type.Missile, Weapon.Color.Red);
                    break;

                case 2:
                    player.Health = player.Health + 1;
                    _powerupNumber = 2;
                    break;

                case 3:
                    player.Boost = player.Boost + 360;
                    _powerupNumber = 3;
                    break;

                case 4:
                    _powerupNumber = 4;
                    break;

                case 5:
                    player.Health = player.Health / 2;
                    _powerupNumber = 5;
                    break;
            }
        } 
        #endregion
    }
}