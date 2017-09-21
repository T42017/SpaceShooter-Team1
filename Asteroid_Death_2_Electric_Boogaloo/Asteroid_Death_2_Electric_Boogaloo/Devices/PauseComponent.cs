using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.UI;
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
        private SpriteFont font;
        private AsteroidsGame pGame;

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

            UiComponents.Add(new UiLabel(pGame, new Vector2(0, -120), "Paused", font));
            UiComponents.Add(new UiButton(pGame, new Vector2(0, -60), "Resume", font, (sender, args) => pGame.ChangeGameState(GameState.ingame)));
            UiComponents.Add(new UiButton(pGame, new Vector2(), "Main menu", font, (sender, args) => pGame.ChangeGameState(GameState.Menu)));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(playing==false)
                MediaPlayer.Volume = 0.05f; playing = true;

            if (Input.Instance.ClickPause())
            {
                pGame.ChangeGameState(GameState.ingame);
                playing = false;
            }

            if (Input.Instance.ClickUp())
                HighlightPreviusComponent();

            if (Input.Instance.ClickDown())
                HighlightNextComponent();

            if (Input.Instance.ClickSelect())
                UiComponents[HighlightedUiComponent].ClickEvent.Invoke(null, null);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}
