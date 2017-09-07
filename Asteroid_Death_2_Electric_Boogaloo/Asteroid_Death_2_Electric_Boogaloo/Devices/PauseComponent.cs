using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class PauseComponent : AstroidsComponent
    {
        private SpriteFont font;
        public PauseComponent(Game game) : base(game)
        {
            

            UpdatableStates = GameState.paused;
            DrawableStates = GameState.paused;
        }


        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("GameState");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            String Text;
            Text = "Game is paused";


            base.Draw(gameTime);
        }
    }
}
