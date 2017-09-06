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
    class HighscoreMenuComponent : AstroidsComponent

    {
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;
        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game.IsMouseVisible = true;
            pGame = (AsteroidsGame)game;

            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;



        }
    }
}
