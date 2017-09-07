using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Asteroid_Death_2_Electric_Boogaloo.Devices
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
            ////if (!hasaddedgameobjetcs)
            ////{
            ////    pGame.AddGameObjects();
            ////    hasaddedgameobjetcs = true;
            ////}

            //for (int i = pGame.Components.Count - 1; i >= 0; i--)
            //{
            //    if (!(pGame.Components[i] is GameObject gameObject))
            //        continue;
            //    pGame.CheckForCollisionWith(gameObject);
            //}
            //pGame.GenerateRandomNewMeteor(gameTime, 5);

            base.Update(gameTime);
        }
    }
}
