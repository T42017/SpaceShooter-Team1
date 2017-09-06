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
        private void CheckForCollision(GameObject thisObject)
        {
            // -- Removed temporarily to try other approaches
            //for (int i = Components.Count - 1; i >= 0; i--)
            //{
            //    var outerCurrent = Components[i];
            //    if (outerCurrent == null || !(outerCurrent is GameObject gameObject))
            //        continue;
            //    for (int j = Components.Count - 1; j >= 0; j--)
            //    {
            //        var innerCurrent = Components[j];
            //        if (innerCurrent == null || !(innerCurrent is GameObject otherGameObject))
            //            continue;

            //        if (gameObject.CollidesWith(otherGameObject))
            //        {
            //            Components.Remove(gameObject);
            //            Components.Remove(otherGameObject);
            //            return;
            //        }
            //    }
            //}

            for (int i = Components.Count - 1; i >= 0; i--)
            {
                if (Components[i] == null || !(Components[i] is GameObject otherGameObject) || otherGameObject == thisObject)
                    continue;

                if (thisObject.CollidesWith(otherGameObject))
                {
                    GameObjectManager.GameObjects.Remove(otherGameObject);
                    return;
                }
            }
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

            UpdateWindowSize();

            _camera.FollowPlayer(GameObjectManager.player);

            CheckForCollision(GameObjectManager.player);

            GameObjectManager.UpdateGameObjects();

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
