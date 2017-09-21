using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.Managers;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    public abstract class Powerup : GameObject
    {
        #region Protected fields
        protected AsteroidsGame Game;
        #endregion

        #region Public properties
        public PowerupType PowerupType { get; }
        public int Timer { get; set; }
        #endregion
        
        #region Protected constructors
        protected Powerup(AsteroidsGame game, Vector2 position, PowerupType powerupType, int duration) : this(game, powerupType, duration)
        {
            Position = position;
        }

        protected Powerup(AsteroidsGame game, PowerupType powerupType, int duration) : base(game)
        {
            Game = game;
            PowerupType = powerupType;
            Texture = TextureManager.Instance.PowerUpTextures[(int)powerupType];
            Timer = duration;
        }
        #endregion

        #region Public abstract methods
        public abstract void Remove(Player player);

        public abstract void DoEffect(Player player);
        #endregion

        public override void Update()
        {
            Rotation = Game.GameObjectManager.Player.Rotation;
            base.Update();
        }
    }
}