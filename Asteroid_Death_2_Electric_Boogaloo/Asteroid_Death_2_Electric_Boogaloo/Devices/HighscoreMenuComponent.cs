using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Song song;

        private int _highlightedUiComponent;
        private List<BaseUiComponent> UiComponents;

        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame)game;
            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            menuFont = Game.Content.Load<SpriteFont>("Font");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("CantinaBand");
            _backGroundtexture = Game.Content.Load<Texture2D>("background");

            UiComponents = new List<BaseUiComponent>();
            UiComponents.Add(new UiList(Game, new Vector2(0, -300), menuFont, HighScore.GetHighScores(), 40));
            UiComponents.Add(new UiButton(Game, new Vector2(0, 140), "Play", buttonFont, (sender, args) => Game.Start()));
            UiComponents.Add(new UiButton(Game, new Vector2(0, 200), "Back", buttonFont, (sender, args) => Game.ChangeGameState(GameState.Menu)));

            HighlightNextComponent();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (Input.Instance.ClickUp())
                HighlightPreviusComponent();

            if (Input.Instance.ClickDown())
                HighlightNextComponent();

            if (Input.Instance.ClickSelect())
                UiComponents[_highlightedUiComponent].ClickEvent?.Invoke(null, null);

            foreach (BaseUiComponent component in UiComponents)
                component.Update();

            UpdateHighlightMarker();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            for (int x = 0; x < 2000; x += _backGroundtexture.Width)
            {
                for (int y = 0; y < 2000; y += _backGroundtexture.Height)
                {
                    SpriteBatch.Draw(_backGroundtexture, new Vector2(x, y), Color.White);
                }
            }
            
            foreach (var component in UiComponents)
                component.Draw(SpriteBatch);


            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateHighlightMarker()
        {
            for (int i = 0; i < UiComponents.Count; i++)
            {
                UiComponents[i].IsHighlighted = i == _highlightedUiComponent;
            }
        }

        public void HighlightPreviusComponent()
        {
            int previusComponent = _highlightedUiComponent;
            for (int i = _highlightedUiComponent; i > 0; i--)
            {
                previusComponent--;
                if (UiComponents[previusComponent].CanBeHighLighted)
                {
                    _highlightedUiComponent = previusComponent;
                    return;
                }
            }
        }

        public void HighlightNextComponent()
        {
            for (int nextComponent = _highlightedUiComponent + 1; nextComponent < UiComponents.Count; nextComponent++)
            {
                if (UiComponents[nextComponent].CanBeHighLighted)
                {
                    _highlightedUiComponent = nextComponent;
                    return;
                }
            }
        }
    }
}
