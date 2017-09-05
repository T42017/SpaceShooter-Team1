using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class MenuComponent : AstroidsComponent
    {
        private SpriteFont menuFont;



        public MenuComponent(Game game) : base(game)
        {



        }


        protected override void LoadContent()
        {
            menuFont= Game.Content.Load<SpriteFont>("GameState");
            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(menuFont, men);



            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}
