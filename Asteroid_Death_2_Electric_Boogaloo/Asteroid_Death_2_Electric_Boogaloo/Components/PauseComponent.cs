using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.UI;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    public class PauseComponent : AsteroidsComponent
    {
        #region Private fields
        private Song _song;
        private bool _playing;
        private SpriteFont _font;
        private AsteroidsGame _game;
        #endregion

        #region Public constructors
        public PauseComponent(Game game) : base(game)
        {
            _game = (AsteroidsGame)game;
            _playing = false;
            UpdatableStates = GameState.Paused;
            DrawableStates = GameState.Paused;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            _song = Game.Content.Load<Song>("Chameleon");
            _font = Game.Content.Load<SpriteFont>("Text");

            UiComponents.Add(new UiLabel(_game, new Vector2(0, -120), "Paused", _font));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -60), "Resume", _font, (sender, args) => _game.ChangeGameState(GameState.InGame)));
            UiComponents.Add(new UiButton(_game, new Vector2(), "Main menu", _font, (sender, args) => _game.ChangeGameState(GameState.Menu)));

            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void Update(GameTime gameTime)
        {
            if (_playing == false)
                MediaPlayer.Volume = 0.05f; _playing = true;

            if (Input.Instance.ClickPause())
            {
                _game.ChangeGameState(GameState.InGame);
                _playing = false;
            }

            if (Input.Instance.ClickUp())
                HighlightPreviusComponent();

            if (Input.Instance.ClickDown())
                HighlightNextComponent();

            if (Input.Instance.ClickSelect())
                UiComponents[HighlightedUiComponent].ClickEvent.Invoke(null, null);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        } 
        #endregion
    }
}