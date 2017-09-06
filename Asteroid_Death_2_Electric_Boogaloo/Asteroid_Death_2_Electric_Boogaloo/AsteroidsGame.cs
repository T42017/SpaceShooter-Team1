using System;
using System.Collections.Generic;
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
        public List<GameObject> _gameObjects = new List<GameObject>();

        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private Texture2D _backgroundTexture;
        private Player _player;
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
                    _gameObjects.Remove(otherGameObject);
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

            _camera = new Camera();
            AddGameObjects();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("background");

            foreach (var gameObject in _gameObjects)
                gameObject.LoadContent();
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _camera.Pos = _player.Position;
            CheckForCollision(_player);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //// if using XNA 4.0
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _camera.get_transformation(GraphicsDevice));

            for (int y = 0; y < Globals.ScreenHeight; y += _backgroundTexture.Height)
            {
                for (int x = 0; x < Globals.ScreenWidth; x += _backgroundTexture.Width)
                {
                    _spriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);
                }
            }

            foreach (var gameObject in _gameObjects)
                gameObject.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddGameObjects()
        {
            _player = new Player(this)
            {
                Position = new Vector2(Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight / 2)
            };

            //*
            for (int i = 0; i < 10; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(Globals.ScreenWidth),
                    Globals.RNG.Next(Globals.ScreenHeight)
                );
                Meteor meteor = new Meteor(this, position, MeteorSize.Big, MeteorColour.Gray)
                {
                    Rotation = (float) Globals.RNG.NextDouble()
                };
                _gameObjects.Add(meteor);
            }
            //*/

            //*
            EnemyFactory enemyFactory = new EnemyFactory(this);
            for (int i = 0; i < 4; i++)
            {
                _gameObjects.Add(enemyFactory.GetRandomEnemy());
            }
            //*/
            _gameObjects.Add(_player);
        }
    }
}
