﻿using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    internal class MenuComponent : AstroidsComponent
    {
        public static SpriteFont menuFont, buttonFont;

        private SpriteBatch batch;
        private Texture2D Button, texture;
        private int choice;
        private bool hasMovedStick;
        private GamePadState lastGamePadState, lastPadState;
        private KeyboardState lastState, lastkState;
        
        
        private readonly AsteroidsGame pGame;
        private bool playing;
        private Song song;

        public MenuComponent(Game game) : base(game)
        {
            batch = SpriteBatch;
            pGame = (AsteroidsGame) game;

            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;

            playing = false;
            MediaPlayer.IsRepeating = true;
            choice = 0;
        }

        protected override void LoadContent()
        {
            menuFont = Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            Button = Game.Content.Load<Texture2D>("ButtonBlue");
            song = Game.Content.Load<Song>("Best");
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

            var gamePadState = GamePad.GetState(PlayerIndex.One);
            var Keyboardstate = Keyboard.GetState();

            if (gamePadState.DPad.Up == ButtonState.Pressed && lastGamePadState.DPad.Up == ButtonState.Released
                || gamePadState.ThumbSticks.Left.Y >= 0.3f && !hasMovedStick
                || Keyboardstate.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)
                || Keyboardstate.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W))
            {
                hasMovedStick = true;

                if (choice == 0)
                {
                }

                else
                {
                    choice--;
                }
            }

            if (gamePadState.DPad.Down == ButtonState.Pressed && lastGamePadState.DPad.Down == ButtonState.Released
                || gamePadState.ThumbSticks.Left.Y <= -0.3f && !hasMovedStick
                || Keyboardstate.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down)
                || Keyboardstate.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S))
            {
                hasMovedStick = true;

                if (choice == 2)
                {
                }

                else
                {
                    choice++;
                }
            }

            if (gamePadState.ThumbSticks.Left.Y <= 0.2 && gamePadState.ThumbSticks.Left.Y >= -0.2)
                hasMovedStick = false;

            lastState = Keyboardstate;
            lastGamePadState = gamePadState;

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released &&
                choice == 0
                || Keyboardstate.IsKeyDown(Keys.Space) && lastkState.IsKeyUp(Keys.Space) && choice == 0)
            {
                pGame.Start();
                pGame.GameObjectManager.LoadContent();
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
                choice = 0;
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released &&
                choice == 1
                || Keyboardstate.IsKeyDown(Keys.Space) && lastkState.IsKeyUp(Keys.Space) && choice == 1)
            {
                pGame.ChangeGameState(GameState.highscoremenu);
                playing = false;
                choice = 0;
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released &&
                choice == 2
                || Keyboardstate.IsKeyDown(Keys.Space) && lastkState.IsKeyUp(Keys.Space) && choice == 2)
            {
                pGame.Exit();
                playing = false;
                choice = 0;
            }

            lastPadState = gamePadState;
            lastkState = Keyboardstate;

            base.Update(gameTime);
        }

        

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            for (var x = 0; x < 2000; x += texture.Width)
            for (var y = 0; y < 2000; y += texture.Height)
                SpriteBatch.Draw(texture, new Vector2(x, y), Color.White);

            string
                Name = "Asteroid Death 2 Electric Boogaloo",
                button1 = "Start",
                button2 = "Highscore",
                button3 = "Quit";

            SpriteBatch.DrawString(menuFont, Name,
                new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - pGame.Graphics.PreferredBackBufferWidth / 6,
                    pGame.Graphics.PreferredBackBufferHeight / 4), Color.Fuchsia);
            if (choice == 0)
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8), Color.Red);
            else
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8), Color.Cyan);

            SpriteBatch.DrawString(buttonFont, button1,
                new Vector2(
                    pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 90,
                    pGame.Graphics.PreferredBackBufferHeight / 4 +
                    pGame.Graphics.PreferredBackBufferHeight / 8), Color.Black);
            if (choice == 1)
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 +
                        10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8 + 50), Color.Red);
            else
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8 + 50), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button2,
                new Vector2(
                    pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 60,
                    pGame.Graphics.PreferredBackBufferHeight / 4 +
                    pGame.Graphics.PreferredBackBufferHeight / 8 + 50), Color.Black);
            if (choice == 2)
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 +
                        10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8 + 100), Color.Red);
            else
                SpriteBatch.Draw(Button,
                    new Vector2(
                        pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 10,
                        pGame.Graphics.PreferredBackBufferHeight / 4 +
                        pGame.Graphics.PreferredBackBufferHeight / 8 + 100), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button3,
                new Vector2(
                    pGame.Graphics.PreferredBackBufferWidth / 4 + pGame.Graphics.PreferredBackBufferWidth / 6 + 90,
                    pGame.Graphics.PreferredBackBufferHeight / 4 +
                    pGame.Graphics.PreferredBackBufferHeight / 8 + 100), Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}