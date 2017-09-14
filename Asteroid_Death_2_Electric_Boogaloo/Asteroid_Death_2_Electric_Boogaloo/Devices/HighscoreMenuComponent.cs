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
        private String highscore;

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

            // This text is always added, making the file longer over time unless the text is deleted manually
         
            string appendText = "" + Environment.NewLine;
            File.AppendAllText(path, appendText);

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
           
            if (playing==false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
                playing = true;
            }
            
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
            
            SpriteBatch.Draw(button1,new Vector2(pGame.Graphics.PreferredBackBufferWidth/8,(pGame.Graphics.PreferredBackBufferHeight)-(pGame.Graphics.PreferredBackBufferHeight/8)),Color.Cyan);
            SpriteBatch.Draw(button2, new Vector2((pGame.Graphics.PreferredBackBufferWidth) -(pGame.Graphics.PreferredBackBufferHeight / 3), (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 8)),Color.IndianRed);

            //SpriteBatch.DrawString(menuFont,highscore,new Vector2(200,200),Color.Gold);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
