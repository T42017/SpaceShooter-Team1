using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    internal class DeathComponent : AstroidsComponent
    {
        private SpriteFont font, buttonFont;
        private readonly AsteroidsGame Game;
        private bool playing;
        private Song song;
        private Texture2D _backgroundTexture;
        private SoundEffect yes;
        private UiTextbox _textBox;
        private UiLabel _playerScoreLabel;

        public DeathComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame) game;
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("Laugh");
            _backgroundTexture = Game.Content.Load<Texture2D>("background");
            _textBox = new UiTextbox(Game, new Vector2(), font);
            _playerScoreLabel = new UiLabel(Game, new Vector2(0, -120), "", font);

            UiComponents.Add(_playerScoreLabel);
            UiComponents.Add(new UiLabel(Game, new Vector2(0, -70), "Enter your name", font));
            UiComponents.Add(_textBox);
            UiComponents.Add(new UiButton(Game, new Vector2(0, 60), "Done", font, delegate(object sender, EventArgs args) {
                HighScore.SaveScore(_textBox.Text.Equals("") ? "player" : _textBox.Text, Player.Score);
                Game.ChangeGameState(GameState.highscoremenu);
                Player.Score = 0;
            }));

            HighlightNextComponent();
            base.LoadContent();
        }

        public override void ChangedState(GameState newState)
        {
            if (newState == GameState.gameover)
            {
                _textBox.Text = "";
                _playerScoreLabel.Text = "Score " + Player.Score;
            }
            base.ChangedState(newState);
        }

        public override void Update(GameTime gameTime)
        {
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                playing = true;
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
    }
}