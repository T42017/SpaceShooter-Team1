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
        public bool IsShot { get; private set; }

        public Meteor(Game game, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
        {
            MeteorSize = meteorSize;
            MeteorColour = meteorColour;
        }

        /// <summary>
        /// Calls LoadTexture from parent with a filename according to meteor's size and colour.
        /// </summary>
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

        public List<Meteor> SpawnChildren()
        {
            if (!IsShot || MeteorSize == MeteorSize.Small)
                return null;

            var children = new List<Meteor>();
            int amountOfChildren = MeteorColour == MeteorColour.Brown ? 5 : 3;

            for (int i = 0; i < amountOfChildren; i++)
            {
                children.Add(new Meteor(Game, MeteorSize + 1, MeteorColour));
            }
            return children;
        }

        protected override void LoadContent()
        {
            SetAppropriateTexture();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Rotation += 0.05f; // Change fixed float to property later
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position,
                null,
                Color.White,
                Rotation,
                new Vector2(Texture.Width * .5f, Texture.Height * .5f),
                1.0f,
                SpriteEffects.None,
                0f);
            SpriteBatch.End();
            // Check if base.Draw() should be called
        }
    }
}
