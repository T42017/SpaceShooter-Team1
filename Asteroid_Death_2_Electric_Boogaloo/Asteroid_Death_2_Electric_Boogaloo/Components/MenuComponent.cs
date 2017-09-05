using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class MenuComponent : AstroidsComponent
    {
        private SpriteFont menuFont;
        private Astroids pGame;


        public MenuComponent(Game game) : base(game)
        {
            pGame = (Astroids) game; 

            DrawableStates = GameState.Menu;
            UpdatableStates = GameState.Menu;
        }


        protected override void LoadContent()
        {
            menuFont= Game.Content.Load<SpriteFont>("GameState");

            Song song = Game.Content.Load<Song>("Best");
            MediaPlayer.Play(song);

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();

            String Name = "Asteroid Death 2 Electric Boogaloo";
            

            SpriteBatch.DrawString(menuFont, Name, new Vector2(pGame.graphics.PreferredBackBufferWidth/10,pGame.graphics.PreferredBackBufferHeight/4),Color.HotPink );



            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}
