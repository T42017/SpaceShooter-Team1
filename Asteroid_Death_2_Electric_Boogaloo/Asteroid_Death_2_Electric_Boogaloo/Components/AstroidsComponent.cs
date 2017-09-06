using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.Components
{
    class AstroidsComponent : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public Astroids AstroidGame { get; private set; }

        public GameState DrawableStates { get; protected set; }
        public GameState UpdatableStates { get; protected set; }
        public AstroidsComponent(Game game) : base(game)
        {
            AstroidGame = (Astroids)game;
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

    }
}
