using System;
using System.Collections.Generic;
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
        private Texture2D Button,texture;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private Song song;
        private bool playing;
        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game.IsMouseVisible = true;
            pGame = (AsteroidsGame)game;

            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            playing = false;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("background");
            song = Game.Content.Load<Song>("CantinaBand");
           
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

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
