using System.Linq;
using System.Runtime.CompilerServices;
using Asteroid_Death_2_Electric_Boogaloo.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Astroids : Game
    {
       

        
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private GameState _state;
        private Player player;

        public Astroids()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void ChangeGameStates(GameState desiredState)
        {
            _state = desiredState;

            foreach (var component in Components.Cast<AstroidsComponent>())
            {
                component.Visible = component.DrawableStates.HasFlag(_state);
                component.Enabled = component.UpdatableStates.HasFlag(_state);
            }

        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            Components.Add(new MenuComponent(this));
            ChangeGameStates(GameState.Menu);


            player = new Player(this);

            this.Components.Add(player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("background");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int y = 0; y < graphics.PreferredBackBufferHeight; y += backgroundTexture.Height)
                for (int x = 0; x < graphics.PreferredBackBufferWidth; x += backgroundTexture.Width)
                    spriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
