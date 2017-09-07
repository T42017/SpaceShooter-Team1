using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class AsteroidsGame : Game
    {
        public GraphicsDeviceManager Graphics;
        public int WindowWidth, Windowheight;
        public GameObjectManager GameObjectManager;
        public Level Level;

        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private Camera _camera;
        
        public AsteroidsGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            Graphics.PreferredBackBufferWidth = Globals.ScreenWidth;

            Content.RootDirectory = "Content";
            Window.Position = new Point(300, 300);
            Window.Title = "Asteroid Death 2 Electric Boogaloo";
        }

        public void ChangeGameState(GameState desiredState)
        {
            _gameState = desiredState;

            foreach (var component in Components.Cast<AstroidsComponent>())
            {
                component.Visible = component.DrawableStates.HasFlag(_gameState);
                component.Enabled = component.UpdatableStates.HasFlag(_gameState);
            }
        }

	    private void CheckForCollisionWith(GameObject thisObject)	
        {
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                if (Components[i] == null ||
                    !(Components[i] is GameObject otherGameObject) ||
                    otherGameObject == thisObject)
                    continue;

                if (thisObject.CollidesWith(otherGameObject))
                {
                    GameObjectManager.GameObjects.Remove(otherGameObject);
                        continue;
                    //if (thisObject is LaserRed laser)
                    //    Components.Remove(laser);
                    return;
                }
            }
        }
        
        private void GenerateRandomNewMeteor(GameTime gameTime, int intervalInMilliseconds)
        {
            var currentGameTimeModInterval = (int)gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0) return;

            const int a = 100;
            var respawnArea = new Rectangle(
                (int) player.Position.X - player.Width / 2 - a,
                (int) player.Position.Y - player.Height / 2 - a,
                player.Width + 2 * a,
                player.Height + 2 * a
            );

            Meteor meteor;
            do
            { 
                meteor = new Meteor(
                    this,
                    new Vector2(
                        Globals.RNG.Next(0, Globals.ScreenWidth),
                        Globals.RNG.Next(0, Globals.ScreenHeight)
                    ),
                    (MeteorSize) Globals.RNG.Next(0, 3),
                    (MeteorColour) Globals.RNG.Next(0, 2)
                );
            }
            while (respawnArea.Contains(meteor.Bounds)) ;
            Components.Add(meteor);
        }

        
        protected override void Initialize()
        {
            // center window
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (Graphics.PreferredBackBufferWidth / 2), 
                                        (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (Graphics.PreferredBackBufferHeight / 2));

            // allow resizing
            //Window.AllowUserResizing = true;
            UpdateWindowSize();

            Level = new Level(this, 3000, 3000);

            GameObjectManager = new GameObjectManager(this);
            GameObjectManager.AddEnemyFactory(new EnemyFactory(this));

            _camera = new Camera();
            GameObjectManager.AddNewPlayer();
            GameObjectManager.AddEnemys(4);
            GameObjectManager.AddMeteors(10);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Level.LoadContent();
            GameObjectManager.LoadContent();
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = Components.Count - 2; i >= 0; i--) // htf does this work??
            {
                if (!(Components[i] is GameObject gameObject))
                    continue;
                CheckForCollisionWith(gameObject);
            }

            GenerateRandomNewMeteor(gameTime, 1000);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);

            //// if using XNA 4.0
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _camera.get_transformation(GraphicsDevice, WindowWidth, Windowheight));
            
            Level.DrawBackground(_spriteBatch);

            GameObjectManager.DrawGameObjects(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void UpdateWindowSize()
        {
            WindowWidth = Graphics.PreferredBackBufferWidth;
            Windowheight = Graphics.PreferredBackBufferHeight;
        }
    }
}
