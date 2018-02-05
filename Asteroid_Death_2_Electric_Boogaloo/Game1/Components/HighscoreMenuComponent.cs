using Game1.Enums;
using Game1.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Game1.Components
{
    public class HighscoreMenuComponent : AsteroidsComponent
    {
        #region Private fields
        private Texture2D _backGroundtexture;
        private SpriteFont menuFont, buttonFont;
#pragma warning disable 108,114
        private AsteroidsGame Game;
#pragma warning restore 108,114
        private Song _song;
        private bool _playing;
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
            _song = Game.Content.Load<Song>("HighScore");
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
            _playing = false;
            base.ChangedState(newState);
        }

        public override void Update(GameTime gameTime)
        {
            if (_playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_song);
                MediaPlayer.Volume = Globals.universalMusicVolume;
                _playing = true;
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