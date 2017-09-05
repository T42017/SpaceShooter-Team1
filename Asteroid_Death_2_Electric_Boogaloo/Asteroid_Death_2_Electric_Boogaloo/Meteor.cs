using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    // TODO: make brown and gray meteors have different health
    public class Meteor : GameObject
    {
        public MeteorSize   MeteorSize   { get; }
        public MeteorColour MeteorColour { get; }

        public Meteor(Game game, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
        {
            MeteorSize = meteorSize;
            MeteorColour = meteorColour;
            X = 200;
            Y = 200;
        }

        private void SetAppropriateTexture()
        {
            string colour = string.Empty;

            switch (MeteorColour)
            {
                case MeteorColour.Gray:
                    colour = "Grey";
                    break;
                case MeteorColour.Brown:
                    colour = "Brown";
                    break;
            }

            string fileSuffix = string.Empty;

            switch (MeteorSize)
            {
                case MeteorSize.Small:
                    fileSuffix = "small1";
                    break;
                case MeteorSize.Medium:
                    fileSuffix = "med1";
                    break;
                case MeteorSize.Big:
                    fileSuffix = "big1";
                    break;
            }
            string fileName = $"meteor{colour}_{fileSuffix}";
            LoadTexture(fileName);
        }

        protected override void LoadContent()
        {
            SetAppropriateTexture();
            base.LoadContent();
        }
    }
}
