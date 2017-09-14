﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class HighscoreMenuComponent : AstroidsComponent
    {
        private SpriteFont menuFont, buttonFont;
        private Texture2D texture, button1, button2;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private Song song;
        private bool playing,hasloaded;
        private String Mainmenu,startgame,Highscores;
        private String[] highscore1;
        private int highlight,size;
        private KeyboardState lastKeyboardState;
        private GamePadState lastGamePadState;
        private SpriteFont Text;
        public static StreamReader highscoreReader;
        private bool hasMovedStick;

        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game.IsMouseVisible = true;
            pGame = (AsteroidsGame)game;
            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            playing = false;
            MediaPlayer.IsRepeating = true;
            hasloaded = false;
            
            string path = @"Content/Highscore.txt";

            Mainmenu = "Return";
            startgame = "Start";
            
            //highscoreReader = new StreamReader("Highscore.txt");
            //highscore = highscoreReader.ReadToEnd();
            // This text is always added, making the file longer over time unless the text is deleted manually
            string appendText = "" + Environment.NewLine;
            File.AppendAllText(path, appendText);

            highlight = 1;

            // Open the file to read from.
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                Console.WriteLine(s);
            }
        }

        protected override void LoadContent()
        {
            
            menuFont = Game.Content.Load<SpriteFont>("Text");
            texture = Game.Content.Load<Texture2D>("background");
            song = Game.Content.Load<Song>("CantinaBand");
            button1 = Game.Content.Load<Texture2D>("buttonBlue");
            button2 = Game.Content.Load<Texture2D>("buttonRed");
            base.LoadContent();
           
        }

        public override void Update(GameTime gameTime)
        {

            if (hasloaded==false)
            {
                highscore1= HighScore.GetHighScores();
                hasloaded = true;
            }
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
                playing = true;
            }

            var keyboardstate = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.DPad.Left == ButtonState.Pressed && lastGamePadState.DPad.Left == ButtonState.Released
                || gamePadState.ThumbSticks.Left.X <= -0.3f && !hasMovedStick
                || keyboardstate.IsKeyDown(Keys.Left) && lastKeyboardState.IsKeyUp(Keys.Left)
                || keyboardstate.IsKeyDown(Keys.A) && lastKeyboardState.IsKeyUp(Keys.A))
            {
                hasMovedStick = true;
                if (highlight == 0)
                {
                }

                else
                {
                    highlight--;
                }
            }

            if (gamePadState.DPad.Right == ButtonState.Pressed && lastGamePadState.DPad.Right == ButtonState.Released
                || gamePadState.ThumbSticks.Left.X >= 0.3f && !hasMovedStick
                || keyboardstate.IsKeyDown(Keys.Right) && lastKeyboardState.IsKeyUp(Keys.Right)
                || keyboardstate.IsKeyDown(Keys.D) && lastKeyboardState.IsKeyUp(Keys.D))
            {
                hasMovedStick = true;
                if (highlight == 2)
                {
                }

                else
                {
                    highlight++;
                }
            }

            if (gamePadState.ThumbSticks.Left.X <=0.2 && gamePadState.ThumbSticks.Left.X >=-0.2)
            {
                hasMovedStick = false;
            }
            
            if (gamePadState.Buttons.A == ButtonState.Pressed && lastGamePadState.Buttons.A ==ButtonState.Released && highlight == 0
                || keyboardstate.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space) && highlight==0)
            {
                pGame.ChangeGameState(GameState.Menu);
                playing = false;
                hasloaded = false;
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastGamePadState.Buttons.A == ButtonState.Released && highlight == 1
                || keyboardstate.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space) && highlight == 1)
            {
               
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastGamePadState.Buttons.A == ButtonState.Released && highlight == 2
                || keyboardstate.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space) && highlight == 2)
            {
                pGame.Start();
                pGame.GameObjectManager.LoadContent();
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
                hasloaded = false;
            }


           
            lastGamePadState = gamePadState;
            lastKeyboardState = keyboardstate;
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            int size=0;
            for (int x = 0; x < 2000; x += texture.Width)
            {
                for (int y = 0; y < 2000; y += texture.Height)
                {
                    SpriteBatch.Draw(texture, new Vector2(x, y), Color.White);
                }
            }

            if (highlight == 0)
            {
                SpriteBatch.Draw(button1, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) - 180, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Red);
            }

            else
            {
                SpriteBatch.Draw(button1, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) - 180, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Cyan);
            }

            if (highlight == 1)
            {
                SpriteBatch.Draw(button2, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) + 600, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Red);
            }

            else
            {
                SpriteBatch.Draw(button2, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) + 600, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Cyan);
            }

            if (highlight == 2)
            {
                SpriteBatch.Draw(button2, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) + 1300, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Red);
            }

            else
            {
                SpriteBatch.Draw(button2, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) + 1300, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Cyan);
            }

            SpriteBatch.DrawString(menuFont, Mainmenu, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 6) - 180, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)),Color.Black);

            SpriteBatch.DrawString(menuFont, startgame, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 6) + 1380, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Black);
            if (hasloaded == true)
            {
                for (int i = 0;
                    i < highscore1.Length; i++)
                {
                    
                    SpriteBatch.DrawString(menuFont, highscore1[i], new Vector2(pGame.Graphics.PreferredBackBufferWidth / 4, (pGame.Graphics.PreferredBackBufferHeight / 8)+size), Color.Gold);
                    size = size + 30;
                }
            }
            

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
