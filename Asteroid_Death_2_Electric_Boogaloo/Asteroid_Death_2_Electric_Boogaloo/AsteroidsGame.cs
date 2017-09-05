using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class AsteroidsGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState lastKeyboardState;
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
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up))
                player.Accelerate();
            if (state.IsKeyDown(Keys.Down))
                player.Retardation();
            if (state.IsKeyDown(Keys.Left))
                player.Rotation -= 0.05f; 
            else if (state.IsKeyDown(Keys.Right))
                player.Rotation += 0.05f;
            
            lastKeyboardState = state;
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
                    X = i * 140 + 100,
                    Y = 100
                };
                Components.Add(e);
            }
            //*/

            Components.Add(player);
        }
    }
}
