using System.Windows.Forms;
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
        public int AmountOfEnemies = 10;

        public int AmountOfEnemys = 10;
        public int AmountOfBosses = 0;
        
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
                astroidsComponent.ChangedState(desiredState);
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
            ChangeGameState(GameState.ingame);
            Level = new Level(this, 20, 20);
            GameObjectManager = new GameObjectManager(this);
            GameObjectManager.AddEnemyFactory(new EnemyFactory(this));
            GameObjectManager.AddEnemies(10);
            GameObjectManager.AddPowerupFactory(new PowerupFactory(this));
            GameObjectManager.AddPowerups(20);
        }

        public void ControlMaxEnemies()
        {
            Enemy[] enemies = GameObjectManager.GetEnemies();

            if (enemies.Length < AmountOfEnemies)
            {
                GameObjectManager.AddEnemies(AmountOfEnemies - enemies.Length);
            }
        }
        #endregion

        #region Protected overrides
        protected override void Initialize()
        {      
            var form = (Form) Control.FromHandle(Window.Handle);
            form.WindowState = FormWindowState.Maximized;
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
            TextureManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Instance.Update();
            if (_gameState == GameState.ingame)
            {
                GameObjectManager.RemoveDeadGameObjects();
                GameObjectManager.RemoveDeadCollisionEffects();
                GameObjectManager.UpdateGameObjects();
                GameObjectManager.UpdateCollisionEffects();
                _camera.FollowPlayer(GameObjectManager.Player);
                GameObjectManager.AddMeteors(gameTime, Globals.MeteorsPerSecond, 1000);
                ControlMaxEnemies();
                ControlMaxEnemyBosses();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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
                GameObjectManager.DrawCollisionEffects(_spriteBatch);
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

        public void ControlMaxEnemyBosses()
        {
            EnemyBoss[] bosses = GameObjectManager.GetEnemyBosses();

            if (bosses.Length < AmountOfBosses)
            {
                GameObjectManager.AddEnemyBosses(AmountOfBosses - bosses.Length);
            }
        }

    }
}