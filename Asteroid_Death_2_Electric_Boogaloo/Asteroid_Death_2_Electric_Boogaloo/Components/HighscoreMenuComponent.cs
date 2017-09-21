using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.UI;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    public class HighscoreMenuComponent : AsteroidsComponent
    {
        #region Private fields
        private Texture2D _backGroundtexture;
        private SpriteFont menuFont, buttonFont;
        private AsteroidsGame Game;
        private Song song;
        private bool playing, hasloaded;
        private UiList _uiList;
        #endregion

        #region Public constructors
        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame)game;
            DrawableStates = GameState.HighscoreMenu;
            UpdatableStates = GameState.HighscoreMenu;
            MediaPlayer.IsRepeating = true;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            menuFont = Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            song = Game.Content.Load<Song>("CantinaBand");
            _backGroundtexture = Game.Content.Load<Texture2D>("background");
            _uiList = new UiList(Game, new Vector2(0, -300), menuFont, HighScore.GetHighScores(), 40);
            UiComponents.Add(_uiList);
            UiComponents.Add(new UiButton(Game, new Vector2(0, 140), "Play", buttonFont, (sender, args) => Game.Start()));
            UiComponents.Add(new UiButton(Game, new Vector2(0, 200), "Back", buttonFont, (sender, args) => Game.ChangeGameState(GameState.Menu)));

            HighlightNextComponent();
            base.LoadContent();
        } 
        #endregion

        #region Public overrides
        public override void ChangedState(GameState newState)
        {
            if (newState == GameState.HighscoreMenu)
            {
                HighlightedUiComponent = 0;
                HighlightNextComponent();
                _uiList.UpdateList(HighScore.GetHighScores());
            }
            base.ChangedState(newState);
        }

        public override void Update(GameTime gameTime)
        {
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = 0.4f;
                playing = true;
            }

            if (Input.Instance.ClickUp())
                HighlightPreviusComponent();

            if (Input.Instance.ClickDown())
                HighlightNextComponent();

            if (Input.Instance.ClickSelect())
                UiComponents[HighlightedUiComponent].ClickEvent?.Invoke(null, null);
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
            base.Draw(gameTime);
            SpriteBatch.End();
        } 
        #endregion
    }
}