using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Enums;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    public class InGameComponent : AsteroidsComponent
    {
        #region Private fields
        private bool hasaddedgameobjetcs, volume;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame _Game;
        private MouseState oldState;
        private GamePadState lastgamePadState;
        private Song song;
        #endregion

        #region Public static properties
        public static bool Playing { get; set; }
        #endregion

        #region Public constructors
        public InGameComponent(Game game) : base(game)
        {
            _Game = (AsteroidsGame)game;
            DrawableStates = GameState.InGame;
            UpdatableStates = GameState.InGame;
            volume = false;
            Playing = false;
            MediaPlayer.IsRepeating = true;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            song = Game.Content.Load<Song>("InGame");
            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void ChangedState(GameState newState)
        {

            Playing = false;
            base.ChangedState(newState);
        }
        public override void Update(GameTime gameTime)
        {
            
            if (Playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);

                Playing = true;
            }

            if (Input.Instance.ClickPause())
            {
                _Game.ChangeGameState(GameState.Paused);
                volume = false;
            }
            base.Update(gameTime);
        } 
        #endregion
    }
}