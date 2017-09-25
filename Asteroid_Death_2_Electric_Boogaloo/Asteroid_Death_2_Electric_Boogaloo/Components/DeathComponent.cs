using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Asteroid_Death_2_Electric_Boogaloo.UI;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    internal class DeathComponent : AsteroidsComponent
    {
        #region Private fields
        private SpriteFont _font;
        private readonly AsteroidsGame _game;
        private bool _playing;
        private Song _song;
        private Texture2D _backgroundTexture;
        private UiTextbox _textBox;
        private UiLabel _playerScoreLabel;
        #endregion

        #region Public constructors
        public DeathComponent(Game game) : base(game)
        {
            _game = (AsteroidsGame)game;
            _playing = false;
            UpdatableStates = GameState.GameOver;
            DrawableStates = GameState.GameOver;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            _font = _game.Content.Load<SpriteFont>("Text");
            _song = _game.Content.Load<Song>("Laugh");
            _backgroundTexture = _game.Content.Load<Texture2D>("background");
            _textBox = new UiTextbox(_game, new Vector2(), _font);
            _playerScoreLabel = new UiLabel(_game, new Vector2(0, -120), "", _font);

            UiComponents.Add(_playerScoreLabel);
            UiComponents.Add(new UiLabel(_game, new Vector2(0, -70), "Enter your name", _font));
            UiComponents.Add(_textBox);
            UiComponents.Add(new UiButton(_game, new Vector2(0, 60), "Done", _font, delegate (object sender, EventArgs args)
            {
                HighScore.SaveScore(_textBox.Text.Equals("") ? "player" : _textBox.Text, Player.Score);
                _game.ChangeGameState(GameState.HighscoreMenu);
                Player.Score = 0;
            }));

            HighlightNextComponent();
            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void ChangedState(GameState newState)
        {
            if (newState == GameState.GameOver)
            {
                _textBox.Text = "";
                _playerScoreLabel.Text = "Score " + Player.Score;
            }
            _playing = false;
            base.ChangedState(newState);
        }

        public override void Update(GameTime gameTime)
        {
            if (_playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_song);
                _playing = true;
            }

            if (Input.Instance.ClickUp())
                HighlightPreviusComponent();

            if (Input.Instance.ClickDown())
                HighlightNextComponent();

            if (Input.Instance.ClickSelect())
                UiComponents[HighlightedUiComponent].ClickEvent?.Invoke(null, null);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            for (var x = 0; x < 2000; x += _backgroundTexture.Width)
                for (var y = 0; y < 2000; y += _backgroundTexture.Height)
                    SpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

            base.Draw(gameTime);
            SpriteBatch.End();
        } 
        #endregion
    }
}