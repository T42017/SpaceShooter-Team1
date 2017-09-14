using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SoundEffect yes;
        private AsteroidsGame pGame;
        private int choice;
        private MouseState newState,oldState;
        private KeyboardState lastK;
        private GamePadState lastGamePadState,lastPadState;
        private int yes1;
        
        public DeathComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame)game;
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
            yes1 = 0;
            choice = 1;
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
            var Keyboardstate = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.DPad.Up == ButtonState.Pressed && lastGamePadState.DPad.Up == ButtonState.Released || Keyboardstate.IsKeyDown(Keys.Up) && lastK.IsKeyUp(Keys.Up))
            {
                if (choice == 0)
                {
                }

                else
                {
                    choice--;
                }
            }
            if (gamePadState.DPad.Down == ButtonState.Pressed && lastGamePadState.DPad.Down == ButtonState.Released || Keyboardstate.IsKeyDown(Keys.Down) && lastK.IsKeyUp(Keys.Down))
            {
                if (choice == 1)
                {
                }

                else
                {
                    choice++;
                }
                lastGamePadState = gamePadState;
            }

            if (playing==false)
            {

                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                playing = true;
            }

           

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released && choice == 1 || Keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && choice == 1)
            {
                pGame.ChangeGameState(GameState.Menu);
                playing = false;
            }
            oldState = newState;
            lastK = Keyboardstate;
            lastPadState = gamePadState;
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
            if (choice == 1)
            {
                SpriteBatch.Draw(button1, new Vector2(((pGame.Graphics.PreferredBackBufferWidth / 2) - 80), (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 4)), Color.Red);
            }
            else
            {
                SpriteBatch.Draw(button1, new Vector2(((pGame.Graphics.PreferredBackBufferWidth / 2) - 80), (pGame.Graphics.PreferredBackBufferHeight) - (pGame.Graphics.PreferredBackBufferHeight / 4)), Color.Beige);
            }

           
           

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
