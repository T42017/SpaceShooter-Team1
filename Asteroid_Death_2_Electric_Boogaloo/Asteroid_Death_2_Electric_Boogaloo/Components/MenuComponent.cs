using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class MenuComponent : AstroidsComponent
    {
        
        private SpriteFont menuFont,buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;


        public MenuComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;
            
            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;

            pGame.IsMouseVisible = true;
        }
        
        

        protected override void LoadContent()
        {
            menuFont= Game.Content.Load<SpriteFont>("GameState");
            buttonFont = Game.Content.Load<SpriteFont>("Text");
            Button=Game.Content.Load<Texture2D>("ButtonBlue");
            Song song = Game.Content.Load<Song>("Best");
            MediaPlayer.Play(song);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            MouseState newState = Mouse.GetState();
            int x = newState.X, y = newState.Y;
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                

                if (x >= pGame.Graphics.PreferredBackBufferWidth / 3 &&
                    x <= (pGame.Graphics.PreferredBackBufferWidth / 3) + 222 &&
                    y >= (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8) && y<= ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)) +39)
                pGame.ChangeGameState(GameState.loading);
                
                else if (x >= pGame.Graphics.PreferredBackBufferWidth / 3 &&
                         x <= (pGame.Graphics.PreferredBackBufferWidth / 3) + 222 &&
                         y >= ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8) +50)&& y <= ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)) + 89)
                    pGame.ChangeGameState(GameState.highscoremenu);

                else if (x >= pGame.Graphics.PreferredBackBufferWidth / 3 &&
                         x <= (pGame.Graphics.PreferredBackBufferWidth / 3) + 222 &&
                         y >= ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8) + 100) && y <= ((pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)) + 139)
                    pGame.Exit();


            }





            oldState = newState;
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            pGame.GraphicsDevice.Reset();
            GraphicsDevice.Clear(Color.Gold);
            


            SpriteBatch.Begin();

                String 
                Name = "Asteroid Death 2 Electric Boogaloo",
                button1 = "start",
                button2 = "Highscore",
                button3 = "Quit";
            

            SpriteBatch.DrawString(menuFont, Name, new Vector2(pGame.Graphics.PreferredBackBufferWidth/8,pGame.Graphics.PreferredBackBufferHeight/4),Color.Fuchsia);

            SpriteBatch.Draw(Button,new Vector2(pGame.Graphics.PreferredBackBufferWidth/3,(pGame.Graphics.PreferredBackBufferHeight/4)+(pGame.Graphics.PreferredBackBufferHeight/8)),Color.Cyan);
            SpriteBatch.DrawString(buttonFont,button1, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 3+80, (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)),Color.Black);

            SpriteBatch.Draw(Button, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 3, (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)+50), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button2, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 3 + 50, (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8)+50), Color.Black);

            SpriteBatch.Draw(Button, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 3, (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8) +100), Color.Cyan);
            SpriteBatch.DrawString(buttonFont, button3, new Vector2(pGame.Graphics.PreferredBackBufferWidth / 3 + 80, (pGame.Graphics.PreferredBackBufferHeight / 4) + (pGame.Graphics.PreferredBackBufferHeight / 8) + 100), Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}
