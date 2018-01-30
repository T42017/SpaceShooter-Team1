﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Enums;
using Game1.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Game1.Components
{
    public class OptionMenu: AsteroidsComponent
    {
        #region Private fields
        private Song _song;
        private bool _playing;
        private SpriteFont _font;
        private AsteroidsGame _game;
        private Texture2D _backGroundtexture;
        #endregion

        #region Public static properties
        public static SpriteFont MenuFont { get; set; }
        public static SpriteFont ButtonFont { get; set; }
        #endregion

        public OptionMenu(Game game) : base(game)
        {
            _game = (AsteroidsGame)game;
            DrawableStates = GameState.OptionMenu;
            UpdatableStates = GameState.OptionMenu;
            _playing = false;
            MediaPlayer.IsRepeating = true;
        }

        #region Private methods
        private void ButtonMainMenuEvent(object sender, EventArgs eventArgs)
        {
            _game.ChangeGameState(GameState.Menu);
            _playing = false;
        }
        #endregion

        protected override void LoadContent()
        {
            MenuFont = _game.Content.Load<SpriteFont>("GameState");
            ButtonFont = _game.Content.Load<SpriteFont>("Text");
            _song = _game.Content.Load<Song>("MainTheme");
            _backGroundtexture = _game.Content.Load<Texture2D>("background");

            UiComponents.Add(new UiLabel(_game, new Vector2(0, -260), _game.Window.Title, MenuFont));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -150), "Play", ButtonFont, (sender, args) => _game.Start()));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -90), "Highscore", ButtonFont, ButtonMainMenuEvent));
            UiComponents.Add(new UiButton(_game, new Vector2(0, -30), "Quit", ButtonFont, (sender, args) => _game.Exit()));
            UiComponents.Add(new UiLabel(_game, new Vector2(0, 150), "Press arrows to controll player\nPress Space to shoot\nHold E to use boost", MenuFont));
            UiComponents.Add(new UiArrow(_game, new Vector2(0, 30)));
            base.LoadContent();
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
    }
}
