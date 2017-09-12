using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class DeathComponent: AstroidsComponent
    {
        private Song song;
        private bool playing;
        private Texture2D texture,button1,button2;
        private SpriteFont font;
       

        
        public DeathComponent(Game game) : base(game)
        {
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("GameState");
            song = Game.Content.Load<Song>("Laugh");
            texture = Game.Content.Load<Texture2D>("background");
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
