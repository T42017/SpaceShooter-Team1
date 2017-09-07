using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class PauseComponent : AstroidsComponent
    {
        private SpriteFont font;
        private AsteroidsGame pGame;
        public PauseComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                pGame.ChangeGameState(GameState.ingame);


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
