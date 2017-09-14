using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
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
        private int choice,nr,max;
        private string name;
        private Keys lastk,lastk1;
        private MouseState newState,oldState;
        private KeyboardState lastK,LastKey;
        private GamePadState lastGamePadState,lastPadState;
        private int yes1;
        private Keys[] keysToCheck = new Keys[] {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back, Keys.Space };
        KeyboardState lastKeyboardState;
        public DeathComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame)game;
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
            yes1 = 0;
            choice = 0;
            name = "";
            nr = 0;
            max = 12;
           
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
            foreach (Keys key in keysToCheck)
            {
                if (CheckKey(key))
                {
                    AddKeyToText(key);
                    break;
                }
            }
            //if (choice == 0)
            //{

            //    foreach (Keys key in Keyboardstate.GetPressedKeys() )
            //    {
            //        if (key == Keys.Back)
            //        {
            //            if (name.Length == 0)
            //            {

            //            }


            //            else
            //            {
            //                name = name.Remove(name.Length - 1, 1);
            //            }


            //        }

            //        else if(key == Keys.Up || key == Keys.Left || key == Keys.Down || key == Keys.Right )
            //        {

            //        }
            //        else if(lastk == key || lastk1 == key){

            //        }
            //      else if (name.Length>=max)
            //            {

            //            }
            //            else
            //            {
            //            if()
            //                name += key.ToString();


            //            }








            //}


            //}

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released && choice == 1 || Keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && choice == 1)
            {
                pGame.ChangeGameState(GameState.Menu);
                playing = false;
                choice = 0;
                HighScore.SaveScore(name,Player.score);
                name = "";
                Player.score = 0;
            }
            oldState = newState;
            lastK = Keyboardstate;
            lastPadState = gamePadState;
            lastKeyboardState = Keyboardstate;
            base.Update(gameTime);
            
        }

        private void AddKeyToText(Keys key)
        {
            var currentKeyboardState = Keyboard.GetState();
            string newChar = "";
            if (name.Length >= 12 && key != Keys.Back)
                return;
            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Back:
                    if (name.Length != 0)
                        name = name.Remove(name.Length - 1);
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            name += newChar;
        }
        private bool CheckKey(Keys theKey)
        {
            var currentKeyboardState = Keyboard.GetState();

            return lastKeyboardState.IsKeyUp(theKey) &&
                   currentKeyboardState.IsKeyDown(theKey);
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

            if (choice == 0)
            {
                SpriteBatch.DrawString(font,name,new Vector2(pGame.Graphics.PreferredBackBufferWidth/4,pGame.Graphics.PreferredBackBufferHeight/12),Color.Red);
            }
            else
            {
                SpriteBatch.DrawString(font, name, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 4, pGame.Graphics.PreferredBackBufferHeight / 12), Color.Cyan);
            }

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
