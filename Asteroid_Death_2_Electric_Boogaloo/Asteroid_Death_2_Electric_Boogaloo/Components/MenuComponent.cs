using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.UI;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    public class MenuComponent : AsteroidsComponent
    {
        #region Private fields
        private Texture2D _backGroundtexture, _left, _right;
        private int _difficulty;
        private readonly AsteroidsGame _game;
        private bool _playing;
        private Song _song;
        #endregion

        #region Public static properties
        public static SpriteFont MenuFont { get; set; }
        public static SpriteFont ButtonFont { get; set; }
        #endregion

        #region Public constructors
        public MenuComponent(AsteroidsGame game) : base(game)
        {
            _game = (AsteroidsGame)game;
            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;
            _playing = false;
            MediaPlayer.IsRepeating = true;
        }
        #endregion

        #region Private methods
        private void ButtonHghiscoreEvent(object sender, EventArgs eventArgs)
        {
            _game.ChangeGameState(GameState.HighscoreMenu);
            _playing = false;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            _left = _game.Content.Load<Texture2D>("Left");
            _right = _game.Content.Load<Texture2D>("Right");
            MenuFont = _game.Content.Load<SpriteFont>("GameState");
            ButtonFont = _game.Content.Load<SpriteFont>("Text");
            _song = _game.Content.Load<Song>("Best");
            _backGroundtexture = _game.Content.Load<Texture2D>("background");
            _difficulty = 0;

            UiComponents.Add(new UiLabel(_game, new Vector2(0, -260), _game.Window.Title, MenuFont));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -150), "Play", ButtonFont, (sender, args) => _game.Start()));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -90), "Highscore", ButtonFont, ButtonHghiscoreEvent));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -30), "Quit", ButtonFont, (sender, args) => _game.Exit()));
            UiComponents.Add(new UiArrow(_game, new Vector2(0, 30)));

            HighlightNextComponent();
            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void ChangedState(GameState newState)
        {

            _playing = false;
            base.ChangedState(newState);
        }
        public override void Update(GameTime gameTime)
        {
            if (_playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_song);
                MediaPlayer.Volume = 0.4f;
                _playing = true;
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

            for (var x = 0; x < 2000; x += _backGroundtexture.Width)
                for (var y = 0; y < 2000; y += _backGroundtexture.Height)
                    SpriteBatch.Draw(_backGroundtexture, new Vector2(x, y), Color.White);

            base.Draw(gameTime);
            SpriteBatch.End();
        } 
        #endregion
    }
}