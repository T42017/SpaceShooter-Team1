using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class DeathComponent: AstroidsComponent
    {
        private Song song;
        private bool playing;
        private Texture2D texture,button1,button2;
        private SpriteFont font;
        private AsteroidsGame pGame;
        private MouseState newState,oldState;
        
        public DeathComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame)game;
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

            newState = Mouse.GetState();
            int x = newState.X, y = newState.Y;
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                if (x >= (pGame.Graphics.PreferredBackBufferWidth / 2)-80 &&
                    x <= (pGame.Graphics.PreferredBackBufferWidth / 2) +
                    (152) &&
                    y >= (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 2) && y <=
                    ((pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 2)) +
                    50)
                {
                    pGame.ChangeGameState(GameState.Menu);
                    playing = false;
                }
            }
            oldState = newState;
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
            SpriteBatch.Draw(button1, new Vector2(((pGame.Graphics.PreferredBackBufferWidth / 2)-80), (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 2)),Color.Beige);
           

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
