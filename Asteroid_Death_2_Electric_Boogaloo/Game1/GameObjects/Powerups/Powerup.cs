using Game1.Enums;
using Game1.Managers;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects.Powerups
{
    public abstract class Powerup : GameObject
    {
        #region Protected fields
#pragma warning disable 108,114
        protected AsteroidsGame Game;
#pragma warning restore 108,114
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

        #region Public overrides
        public override void Update()
        {
            Rotation = Game.GameObjectManager.Player.Rotation;
            base.Update();
        } 
        #endregion
    }
}