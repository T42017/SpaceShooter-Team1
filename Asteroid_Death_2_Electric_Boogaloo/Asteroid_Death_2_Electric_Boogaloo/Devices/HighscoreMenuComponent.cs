using System;
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
        private bool playing;
        private String Mainmenu,startgame;
        private int highlight;
        private KeyboardState lastK;
        private GamePadState lastGamePadState,lasPadState;
        public static StreamReader highscoreReader;

        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game.IsMouseVisible = true;
            pGame = (AsteroidsGame)game;
            //highscoreReader = new StreamReader("Highscore.txt");
            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            playing = false;
            MediaPlayer.IsRepeating = true;
            //highscore = highscoreReader.ReadToEnd();

            string path = @"Content/Highscore.txt";
            Mainmenu = "Go back to main menu";
            startgame = "Start";
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
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
                playing = true;
            }
            var keyboardstate = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.DPad.Left == ButtonState.Pressed && lastGamePadState.DPad.Left == ButtonState.Released || keyboardstate.IsKeyDown(Keys.Left) && lastK.IsKeyUp(Keys.Left))
            {
                if (highlight == 0)
                {
                }

                else
                {
                    highlight--;
                }
            }
            if (gamePadState.DPad.Right == ButtonState.Pressed && lastGamePadState.DPad.Right == ButtonState.Released || keyboardstate.IsKeyDown(Keys.Right) && lastK.IsKeyUp(Keys.Right))
            {
                if (highlight == 2)
                {
                }

                else
                {
                    highlight++;
                }

            }
            lastGamePadState = gamePadState;

            if (gamePadState.Buttons.A == ButtonState.Pressed && lasPadState.Buttons.A ==ButtonState.Released && highlight == 0 || keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && highlight==0)
            {
                pGame.ChangeGameState(GameState.Menu);
                playing = false;
            }
            if (gamePadState.Buttons.A == ButtonState.Pressed && lasPadState.Buttons.A == ButtonState.Released && highlight == 1 || keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && highlight == 1)
            {
               
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lasPadState.Buttons.A == ButtonState.Released && highlight == 2 || keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && highlight == 2)
            {
                pGame.Start();
                pGame.GameObjectManager.LoadContent();
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
            }
            lasPadState = gamePadState;
            lastK = keyboardstate;
           
            base.Update(gameTime);
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
            if (highlight == 0)
            {
                SpriteBatch.Draw(button1, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) - 100, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Red);
            }
            else
            {
                SpriteBatch.Draw(button1, new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) - 100, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Cyan);
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

           SpriteBatch.DrawString(menuFont,startgame,new Vector2((pGame.Graphics.PreferredBackBufferWidth / 8) - 20, (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)),Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
