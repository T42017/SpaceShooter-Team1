using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    internal class MenuComponent : AstroidsComponent
    {
        public static SpriteFont menuFont, buttonFont;
        
        private Texture2D _backGroundtexture, left, right;
        private int difficulty;     
        private readonly AsteroidsGame Game;
        private bool playing;
        private Song song;

        public MenuComponent(AsteroidsGame game) : base(game)
        {
            Game = (AsteroidsGame) game;
            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;
            playing = false;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            left = Game.Content.Load<Texture2D>("Left");
            right = Game.Content.Load<Texture2D>("Right");
            menuFont = Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("Best");
            _backGroundtexture = Game.Content.Load<Texture2D>("background");
            difficulty = 0;
            
            UiComponents.Add(new UiLabel(Game, new Vector2(0, -260), Game.Window.Title, menuFont));
            UiComponents.Add(new UiButton(Game, new Vector2(0, -150), "Play", buttonFont, (sender, args) => Game.Start()));
            UiComponents.Add(new UiButton(Game, new Vector2(0, -90), "Highscore", buttonFont, ButtonHghiscoreEvent));
            UiComponents.Add(new UiButton(Game, new Vector2(0, -30), "Quit", buttonFont, (sender, args) => Game.Exit()));
            UiComponents.Add(new UiArrow(Game, new Vector2(0,30)));

            HighlightNextComponent();
            base.LoadContent();
        }

        private void ButtonHghiscoreEvent(object sender, EventArgs eventArgs)
        {
            Game.ChangeGameState(GameState.highscoremenu);
            playing = false;
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