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
    class PauseComponent : AstroidsComponent
    {
        private Song song;
        private bool playing;
        private KeyboardState lastKeyboardState;
        private SpriteFont font;
        private AsteroidsGame pGame;
        private GamePadState lastGamePadState,laststate;
        public PauseComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;
            playing = false;
            UpdatableStates = GameState.paused;
            DrawableStates = GameState.paused;
        }
        
        protected override void LoadContent()
        {
            song = Game.Content.Load<Song>("Chameleon");
            font = Game.Content.Load<SpriteFont>("Text");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(playing==false)
                MediaPlayer.Volume = 0.05f; playing = true;
              
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            var KeyboardState = Keyboard.GetState();
            if (gamePadState.Buttons.Start==ButtonState.Pressed && laststate.Buttons.Start== ButtonState.Released || KeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape))
                pGame.ChangeGameState(GameState.ingame); playing = false;
            laststate = gamePadState;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && lastGamePadState.Buttons.Back==ButtonState.Released
                || KeyboardState.IsKeyDown(Keys.M) && lastKeyboardState.IsKeyUp(Keys.M))
                pGame.ChangeGameState(GameState.Menu); playing = false;

            lastGamePadState = gamePadState;

            lastKeyboardState = KeyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            String Text1,text2;
            Text1 = "Game is paused";
            text2 = "Return to the main menu by pressing M on the keyboard or Back on your gamepad";

            SpriteBatch.Begin();

            SpriteBatch.DrawString(font, Text1,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 2) - (pGame.Graphics.PreferredBackBufferWidth / 16),
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Gold);

            SpriteBatch.DrawString(font, text2,
                new Vector2(
                    pGame.Graphics.PreferredBackBufferWidth / 2 - (pGame.Graphics.PreferredBackBufferWidth / 3),
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8) + 30), Color.Goldenrod );
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
