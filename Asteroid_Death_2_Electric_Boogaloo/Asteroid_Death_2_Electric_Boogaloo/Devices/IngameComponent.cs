using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class IngameComponent : AstroidsComponent
    {
        private KeyboardState lastKeyboardState, KeyboardState;
        private GamePadState lastGamePadState;
        private bool hasaddedgameobjetcs,volume;
        public static bool playing;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private GamePadState lastgamePadState;
        private Song song;

        public IngameComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;

            DrawableStates = GameState.ingame;
            UpdatableStates = GameState.ingame;
            volume = false;
            playing = false;
            MediaPlayer.IsRepeating = true;
        }

        

        protected override void LoadContent()
        {
            song = Game.Content.Load<Song>("Combat");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (volume == false)
            {
                MediaPlayer.Volume = 0.6f;
                volume = true;
            }
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
               
                playing =true;
            }
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState = Keyboard.GetState();
            if (gamePadState.Buttons.Start == ButtonState.Pressed   &&lastgamePadState.Buttons.Start== ButtonState.Released
                || KeyboardState.IsKeyDown(Keys.Escape)&& lastKeyboardState.IsKeyUp(Keys.Escape))
            {
                pGame.ChangeGameState(GameState.paused);
                volume= false;

            }
            
          

            //for (int i = pGame.Components.Count - 1; i >= 0; i--)
            //{
            //    if (!(pGame.Components[i] is GameObject gameObject))
            //        continue;
            //    pGame.CheckForCollisionWith(gameObject);
            //}
            //pGame.GenerateRandomNewMeteor(gameTime, 5);
            lastgamePadState = gamePadState;
            lastKeyboardState = KeyboardState;
            base.Update(gameTime);
        }
    }
}
