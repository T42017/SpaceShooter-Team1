using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class IngameComponent : AstroidsComponent
    {
        private bool hasaddedgameobjetcs;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;
        public IngameComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame)game;
            
           
            DrawableStates = GameState.ingame;
            UpdatableStates = GameState.ingame;




        }

       public override void Update(GameTime gameTime)
        {
            if (!hasaddedgameobjetcs)
            {
                pGame.AddGameObjects();
                hasaddedgameobjetcs = true;
            }
            

            
            base.Update(gameTime);
        }
    }
}
