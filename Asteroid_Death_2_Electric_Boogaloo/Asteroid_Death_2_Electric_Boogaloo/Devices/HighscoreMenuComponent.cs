using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class HighscoreMenuComponent : AstroidsComponent
    {
        private Texture2D _backGroundtexture;
        private SpriteFont menuFont, buttonFont;
        private AsteroidsGame Game;
        private Song song;
        private bool playing, hasloaded;
        private UiList _uiList;

        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame)game;
            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            menuFont = Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("HighScore");
            _backGroundtexture = Game.Content.Load<Texture2D>("background");
            _uiList = new UiList(Game, new Vector2(0, -300), menuFont, HighScore.GetHighScores(), 40);
            UiComponents.Add(_uiList);
            UiComponents.Add(new UiButton(Game, new Vector2(0, 140), "Play", buttonFont, (sender, args) => Game.Start()));
            UiComponents.Add(new UiButton(Game, new Vector2(0, 200), "Back", buttonFont, (sender, args) => Game.ChangeGameState(GameState.Menu)));

            HighlightNextComponent();
            base.LoadContent();
        }

        public override void ChangedState(GameState newState)
        {
            if (newState == GameState.highscoremenu)
            {
                HighlightedUiComponent = 0;
                HighlightNextComponent();
                _uiList.UpdateList(HighScore.GetHighScores());
            }
            base.ChangedState(newState);
        }

        public override void Update(GameTime gameTime)
        {
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
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

            for (int x = 0; x < 2000; x += _backGroundtexture.Width)
            {
                for (int y = 0; y < 2000; y += _backGroundtexture.Height)
                {
                    SpriteBatch.Draw(_backGroundtexture, new Vector2(x, y), Color.White);
                }
            }
            base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}
