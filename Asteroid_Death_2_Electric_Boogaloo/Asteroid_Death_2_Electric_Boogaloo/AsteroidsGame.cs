using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class AsteroidsGame : Game
    {
        private GameState _gameState;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Player player;
        private Meteor[] meteors;

        public AsteroidsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;

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
                if (Components[i] == null || 
                    !(Components[i] is GameObject otherGameObject) || 
                    otherGameObject == thisObject)
                    continue;

                if (thisObject.CollidesWith(otherGameObject))
                {
                    Components.Remove(otherGameObject);
                    return;
                }
            }
        }

        
        protected override void Initialize()
        {
            // center window
            /*
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), 
                                        (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));

            // allow resizing
            Window.AllowUserResizing = true;
            //*/

            AddGameObjects();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("background");
            
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CheckForCollisionWith(player);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            for (int y = 0; y < Globals.ScreenHeight; y += backgroundTexture.Width)
            {
                for (int x = 0; x < Globals.ScreenWidth; x += backgroundTexture.Width)
                {
                    spriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddGameObjects()
        {
            player = new Player(this)
            {
                Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)
            };

            meteors = new Meteor[10];
            for (int i = 0; i < meteors.Length; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(Globals.ScreenWidth),
                    Globals.RNG.Next(Globals.ScreenHeight)
                );
                meteors[i] = new Meteor(this, position, MeteorSize.Big, MeteorColour.Gray)
                {
                    Rotation = (float) Globals.RNG.NextDouble()
                };
            }

            /*
            for (int i = 0; i < 5; i++)
            {
                Enemy e = new Enemy(this)
                {
                    Position = new Vector2(i * 140 + 100, 100)
                };
                Components.Add(e);
            }
            //*/

            Components.Add(player);
            foreach (var meteor in meteors)
                Components.Add(meteor);
        }
    }
}
