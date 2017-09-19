using Asteroid_Death_2_Electric_Boogaloo.Components;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.Factorys;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class AsteroidsGame : Game
    {
        #region Private fields
        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private Camera _camera;
        #endregion

        #region Public properties
        public GraphicsDeviceManager Graphics { get; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public GameObjectManager GameObjectManager { get; set; }
        public Level Level { get; set; }
        #endregion

        #region Constructors
        public int AmountOfEnemys = 10;

        public AsteroidsGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Globals.ScreenHeight,
                PreferredBackBufferWidth = Globals.ScreenWidth
            };

            Content.RootDirectory = "Content";
            Window.Title = "Asteroid Death 2 Electric Boogaloo";
        }
        #endregion

        #region Public methods
        public void ChangeGameState(GameState desiredState)
        {
            _gameState = desiredState;

            foreach (var component in Components)
            {
                if (!(component is AstroidsComponent astroidsComponent))
                    continue;
                astroidsComponent.Visible = astroidsComponent.DrawableStates.HasFlag(_gameState);
                astroidsComponent.Enabled = astroidsComponent.UpdatableStates.HasFlag(_gameState);
            }
        }

        public void UpdateWindowSize()
        {
            WindowWidth = Graphics.PreferredBackBufferWidth;
            WindowHeight = Graphics.PreferredBackBufferHeight;
        }

        public void Start()
        {
            Level = new Level(this, 20, 20);
            GameObjectManager = new GameObjectManager(this);
            GameObjectManager.AddEnemyFactory(new EnemyFactory(this));
            GameObjectManager.AddEnemys(10);
            GameObjectManager.AddPowerupFactory(new PowerupFactory(this));
            GameObjectManager.AddPowerups(20);
        } 
        #endregion

        #region Protected overrides
        protected override void Initialize()
        {
            // center window
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (Graphics.PreferredBackBufferWidth / 2),
                                        (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (Graphics.PreferredBackBufferHeight / 2));

            // allow resizing
            //Window.AllowUserResizing = true;
            UpdateWindowSize();

            Components.Add(new MenuComponent(this));
            Components.Add(new HighscoreMenuComponent(this));
            Components.Add(new IngameComponent(this));
            Components.Add(new PauseComponent(this));
            Components.Add(new DeathComponent(this));
            ChangeGameState(GameState.Menu);

            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //GameObjectManager.LoadContent();
            TextureManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.ScreenWidth = Graphics.PreferredBackBufferWidth;
            if (_gameState == GameState.ingame)
            {
                GameObjectManager.RemoveDeadGameObjects();
                GameObjectManager.RemoveDeadExplosions();
                GameObjectManager.RemoveDeadHitmarkers();
                GameObjectManager.UpdateGameObjects();
                GameObjectManager.UpdateExplosions();
                GameObjectManager.UpdateHitmarkers();
                _camera.FollowPlayer(GameObjectManager.Player);
                GameObjectManager.AddNewMeteors(gameTime, 10, 1000);
                ControlMaxEnemies();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // if using XNA 4.0
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _camera.get_transformation(GraphicsDevice, WindowWidth, WindowHeight));

            if (_gameState == GameState.ingame || _gameState == GameState.paused)
            {
                Level.DrawBackground(_spriteBatch);
                GameObjectManager.DrawGameObjects(_spriteBatch);
                GameObjectManager.DrawExplosions(_spriteBatch);
                GameObjectManager.DrawHitmarkers(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        } 
        #endregion
        public void ControlMaxEnemies()
        {
            Enemy[] enemys = GameObjectManager.GetEnemys();

            if (enemys.Length < AmountOfEnemys)
            {
                GameObjectManager.AddEnemys(AmountOfEnemys - enemys.Length);
            }
        }
    }
}