using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    internal class DeathComponent : AstroidsComponent
    {
        private int choice, nr, max;
        private SpriteFont font;

        private readonly Keys[] keysToCheck =
        {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back
        };

        private GamePadState lastGamePadState, lastPadState;
        private KeyboardState lastK;
        private KeyboardState lastKeyboardState;
        private string name;
        private MouseState oldState;
        private SpriteEffect none;
        private readonly AsteroidsGame pGame;
        private bool playing;
        private Song song;
        private Texture2D texture, button1, button2;
        private SoundEffect yes;
        private int  blink, time;

        public DeathComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
           
            choice = 0;
            name = "";
            nr = 0;
            max = 12;
            blink = 0;
            time = 0;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Font");
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
            if (gamePadState.DPad.Up == ButtonState.Pressed && lastGamePadState.DPad.Up == ButtonState.Released ||
                Keyboardstate.IsKeyDown(Keys.Up) && lastK.IsKeyUp(Keys.Up))
                if (choice == 0)
                {
                }

                else
                {
                    choice--;
                }
            if (gamePadState.DPad.Down == ButtonState.Pressed && lastGamePadState.DPad.Down == ButtonState.Released ||
                Keyboardstate.IsKeyDown(Keys.Down) && lastK.IsKeyUp(Keys.Down))
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

            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                playing = true;
            }
            foreach (var key in keysToCheck)
                if (CheckKey(key))
                {
                    AddKeyToText(key);
                    break;
                }

            if (gamePadState.Buttons.A == ButtonState.Pressed && lastPadState.Buttons.A == ButtonState.Released &&
                choice == 1 || Keyboardstate.IsKeyDown(Keys.Space) && lastK.IsKeyUp(Keys.Space) && choice == 1)
            {
                pGame.ChangeGameState(GameState.Menu);
                playing = false;
                choice = 0;
                if (name == "" || name.Length == 0)
                {
                    name = "Player";
                    HighScore.SaveScore(name, Player.score);
                }
                else
                {
                    HighScore.SaveScore(name, Player.score);
                }

                name = "";
                Player.score = 0;
            }
            time++;
            if (time == 15)
            {
                switch (blink)
                {
                    case 0:
                        blink = 1;
                        break;

                    case 1:
                        blink = 0;
                        break;
                }
                time = 0;
            }
           
            lastK = Keyboardstate;
            lastPadState = gamePadState;
            lastKeyboardState = Keyboardstate;
            base.Update(gameTime);
        }

        private void AddKeyToText(Keys key)
        {
            var currentKeyboardState = Keyboard.GetState();
            var newChar = "";
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
                case Keys.Back:
                    if (name.Length != 0)
                        name = name.Remove(name.Length - 1);
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
                newChar = newChar.ToUpper();
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
            
            for (var x = 0; x < 2000; x += texture.Width)
            for (var y = 0; y < 2000; y += texture.Height)
                SpriteBatch.Draw(texture, new Vector2(x, y), Color.White);
            if (choice == 1)
            {
                SpriteBatch.Draw(button1,
                    new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 80,
                        pGame.Graphics.PreferredBackBufferHeight - pGame.Graphics.PreferredBackBufferHeight / 4),
                    Color.Red);
                SpriteBatch.DrawString(font, "Return to Menu",
                    new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 75,
                        pGame.Graphics.PreferredBackBufferHeight - pGame.Graphics.PreferredBackBufferHeight / 4),
                    Color.Black);
                if (blink == 0)
                {
                    SpriteBatch.Draw(button1,
                        new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 80,
                            pGame.Graphics.PreferredBackBufferHeight / 12), Color.Cyan);
                }
                else
                {
                    SpriteBatch.Draw(button1,
                        new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 80,
                            pGame.Graphics.PreferredBackBufferHeight / 12), Color.Red);
                    SpriteBatch.DrawString(font, "_",
                        new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 75,
                            pGame.Graphics.PreferredBackBufferHeight / 12), Color.Black);
                }
            }

            if (choice == 0)
            {
                SpriteBatch.Draw(button1,
                    new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 80,
                        pGame.Graphics.PreferredBackBufferHeight - pGame.Graphics.PreferredBackBufferHeight / 4),
                    Color.Cyan);
                SpriteBatch.DrawString(font, "Return to Menu",
                    new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 75,
                        pGame.Graphics.PreferredBackBufferHeight - pGame.Graphics.PreferredBackBufferHeight / 4),
                    Color.Black);

                SpriteBatch.Draw(button1,
                    new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 80,
                        pGame.Graphics.PreferredBackBufferHeight / 12), Color.Red);
            }

            SpriteBatch.DrawString(font, name,
                new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - 75,
                    pGame.Graphics.PreferredBackBufferHeight / 12), Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}