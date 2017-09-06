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
            {
                if (Components[i] == null ||
                    !(Components[i] is GameObject otherGameObject) ||
                    otherGameObject == thisObject)
                    continue;

                if (thisObject.CollidesWith(otherGameObject))
                {
                    if (thisObject is Meteor && otherGameObject is Meteor)
                        continue;
                    Components.Remove(otherGameObject);
                    //if (thisObject is LaserRed laser)
                    //    Components.Remove(laser);
                    return;
                }
            }*/
        }

        private void GenerateRandomNewMeteor(GameTime gameTime, int intervalInSeconds)
        {
            var currentGameTimeModInterval = gameTime.TotalGameTime.TotalSeconds % intervalInSeconds;
            var respawnArea = new Rectangle(
                (int) player.Position.X - player.Width / 2 - 100,
                (int) player.Position.Y - player.Height / 2 - 100,
                player.Width + 100,
                player.Height + 100
            );
            
            if ((int) currentGameTimeModInterval == 0)
            {
                var meteor = new Meteor(
                    this,
                    new Vector2(
                        Globals.RNG.Next(0, 200),
                        Globals.RNG.Next(0, 200)
                    ),
                    (MeteorSize)Globals.RNG.Next(0, 3),
                    (MeteorColour)Globals.RNG.Next(0, 2)
                );
                Components.Add(meteor);
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
            
            Components.Add(new MenuComponent(this));
            Components.Add(new HighscoreMenuComponent(this));
            Components.Add(new IngameComponent(this));
            //Components.Add(new YouAreDead());
            ChangeGameState(GameState.Menu);

            

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

            for (int i = Components.Count - 1; i >= 0; i--)
            {
                if (!(Components[i] is GameObject gameObject))
                    continue;
                CheckForCollisionWith(gameObject);
            }

            GenerateRandomNewMeteor(gameTime, 5);

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
        }
    }
}
