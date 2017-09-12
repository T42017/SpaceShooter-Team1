﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class MenuComponent : AstroidsComponent
    {
        private SpriteBatch batch;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button, texture;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private Song song;
        private Level level;
        private bool playing;

        public MenuComponent(Game game) : base(game)
        {
            batch = SpriteBatch;
            pGame = (AsteroidsGame) game;

            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;

            pGame.IsMouseVisible = true;

            playing = false;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            menuFont = Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            Button = Game.Content.Load<Texture2D>("ButtonBlue");
            song = Game.Content.Load<Song>("roasted");
            texture = Game.Content.Load<Texture2D>("background");

            base.LoadContent();
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

            MouseState newState = Mouse.GetState();
            int x = newState.X, y = newState.Y;
            if(newState.LeftButton == ButtonState.Pressed && oldState.LeftButton== ButtonState.Released) { 
            if (x >= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10 &&
                x <= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 232 &&
                y >= (pGame.Graphics.PreferredBackBufferHeight / 4) +
                (pGame.Graphics.PreferredBackBufferHeight / 8) && y <=
                ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)) +
                39)
            {
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
            }

            else if (x >= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10 &&
                     x <= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 232 &&
                     y >= ((pGame.Graphics.PreferredBackBufferHeight / 4) +
                           (pGame.Graphics.PreferredBackBufferHeight / 8) + 50) && y <=
                     ((pGame.Graphics.PreferredBackBufferHeight / 4) +
                      (pGame.Graphics.PreferredBackBufferHeight / 8)) +
                     89)
            {
                pGame.ChangeGameState(GameState.highscoremenu);
                    SoundEffect beep = Game.Content.Load<SoundEffect>("roasted");
                    beep.Play();
                }

            if (x >= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10 &&
                x <= (pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 232 &&
                y >= (pGame.Graphics.PreferredBackBufferHeight / 4) +
                (pGame.Graphics.PreferredBackBufferHeight / 8) && y <=
                ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)) +
                39)
            {
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
            }
            }
            oldState = newState;
            base.Update(gameTime);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            for (int x = 0; x < 2000; x += texture.Width)
            {
                for (int y = 0; y < 2000; y += texture.Height)
                {
                    SpriteBatch.Draw(texture, new Vector2(x, y), Color.White);
                }
            }

            String
                    Name = "Asteroid Death 2 Electric Boogaloo",
                    button1 = "Start",
                    button2 = "Highscore",
                    button3 = "Quit";

                SpriteBatch.DrawString(menuFont, Name,
                    new Vector2((pGame.Graphics.PreferredBackBufferWidth / 2)-(pGame.Graphics.PreferredBackBufferWidth/6),
                        pGame.Graphics.PreferredBackBufferHeight / 4), Color.Fuchsia);

            SpriteBatch.Draw(Button,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button1,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 90,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Black);

            SpriteBatch.Draw(Button,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8) + 50), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button2,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 60,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8) + 50), Color.Black);

            SpriteBatch.Draw(Button,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 10,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8) + 100), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button3,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 4) + (pGame.Graphics.PreferredBackBufferWidth / 6) + 90,
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8) + 100), Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
