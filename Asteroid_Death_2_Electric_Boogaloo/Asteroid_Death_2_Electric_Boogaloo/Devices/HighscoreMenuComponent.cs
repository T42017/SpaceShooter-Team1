using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class HighscoreMenuComponent : AstroidsComponent
    {
        private Texture2D _backGroundtexture;
        private SpriteFont menuFont, buttonFont;
        private AsteroidsGame Game;
        private MouseState oldState;
        private Song song;
        private SoundEffect deus,roasted,Tyrone,RE,Man;
        private bool playing, hasloaded;
        private String Mainmenu,startgame,Highscores;
        private String[] highscore1;
        private int highlight,size, rand,blink;
        private KeyboardState lastKeyboardState;
        private GamePadState lastGamePadState;
        private SpriteFont _font;
        
        private bool _hasMovedStick;


        private int _highlightedUiComponent;
        private List<BaseUiComponent> UiComponents;

        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame)game;
            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            playing = false;
            MediaPlayer.IsRepeating = true;
            hasloaded = false;

            Mainmenu = " Return";
            startgame = "Start";
        }

        protected override void LoadContent()
        {
            deus = Game.Content.Load<SoundEffect>("Deus");
            roasted = Game.Content.Load<SoundEffect>("roasted");
            Tyrone = Game.Content.Load<SoundEffect>("Tyrone");
            RE = Game.Content.Load<SoundEffect>("RE");
            Man = Game.Content.Load<SoundEffect>("man");
            menuFont = Game.Content.Load<SpriteFont>("Font");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("CantinaBand");
            _backGroundtexture = Game.Content.Load<Texture2D>("background");

            UiComponents = new List<BaseUiComponent>();
            UiComponents.Add(new UiButton(Game, new Vector2(0, -60), "Back", buttonFont, (sender, args) => Game.ChangeGameState(GameState.Menu)));
            UiComponents.Add(new UiButton(Game, new Vector2(), "Play", buttonFont, (sender, args) => Game.ChangeGameState(GameState.ingame)));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (hasloaded==false)
            {
                highscore1= HighScore.GetHighScores();
                hasloaded = true;
            }
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
                playing = true;
            }

            if (Input.Instance.ClickLeft())
            {
                if (highlight != 0)
                {
                    highlight--;
                }
            }

            if (Input.Instance.ClickRight())
            {
                if (highlight != 2)
                {
                    highlight++;
                }
            }
            
            if (Input.Instance.ClickSelect() && highlight==0)
            {
                Game.ChangeGameState(GameState.Menu);
                playing = false;
                hasloaded = false;
                highlight = 1;
            }

            if (Input.Instance.ClickSelect())
            {
                
                rand=Globals.RNG.Next(4);
                switch (rand)
                {
                    case 0:
                        deus.Play(0.4f, 0.0f, 0.0f);
                        break;

                    case 1:
                        roasted.Play(0.1f, 0.0f, 0.0f);
                        break;

                    case 2:
                        Man.Play(1.0f,0.0f,0.0f);
                        break;

                    case 3:
                        RE.Play(0.08f, 0.0f, 0.0f);
                        break;

                    case 4:
                        Tyrone.Play(1.0f, 0.0f, 0.0f);
                        break;
                        
                }

            }

            if (Input.Instance.ClickSelect())
            {
                Game.Start();
                Game.ChangeGameState(GameState.ingame);
                playing = false;
                hasloaded = false;
                highlight = 1;
            }

            foreach (BaseUiComponent component in UiComponents)
                component.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            int size=0;
            for (int x = 0; x < 2000; x += _backGroundtexture.Width)
            {
                for (int y = 0; y < 2000; y += _backGroundtexture.Height)
                {
                    SpriteBatch.Draw(_backGroundtexture, new Vector2(x, y), Color.White);
                }
            }
            
            if (hasloaded == true)
            {
                for (int i = 0;
                    i < highscore1.Length; i++)
                {
                    
                    SpriteBatch.DrawString(menuFont, highscore1[i], new Vector2(Game.Graphics.PreferredBackBufferWidth / 4, (Game.Graphics.PreferredBackBufferHeight / 8)+size), Color.Gold);
                    size = size + 30;
                }
            }

            foreach (BaseUiComponent component in UiComponents)
                component.Draw(SpriteBatch);


            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateHighlightedComponent()
        {
            if (_highlightedUiComponent > UiComponents.Count - 1)
                _highlightedUiComponent = UiComponents.Count - 1;

            for (int i = 0; i < UiComponents.Count; i++)
            {
                if (i == _highlightedUiComponent && !UiComponents[i].CanBeHighLighted)
                    _highlightedUiComponent++;

                UiComponents[i].IsHighlighted = i == _highlightedUiComponent;
            }
        }
    }
}
