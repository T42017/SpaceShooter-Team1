using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    // TODO: make brown and gray meteors have different health
    public class Meteor : GameObject
    {
        private KeyboardState lastKeyboardState;
        public MeteorSize   MeteorSize   { get; }
        public MeteorColour MeteorColour { get; }
        public float RotationSpeed;
        public Meteor(Game game, Vector2 position, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
        {
            Position = position;
            Speed = new Vector2(
                (float) Globals.RNG.NextDouble(),
                (float) Globals.RNG.NextDouble()
            );
            RotationSpeed =(float) Globals.RNG.Next(12)/100;
            MaxSpeed = Globals.RNG.Next(250);
            MeteorSize = meteorSize;
            MeteorColour = meteorColour;
        }

        /// <summary>
        /// Calls LoadTexture from parent with a filename according to meteor's size and colour.
        /// </summary>
        private void SetAppropriateTexture()
        {
            string colour = string.Empty;
            string fileSuffix = string.Empty;

            switch (MeteorColour)
            {
                case MeteorColour.Gray:
                    colour = "Grey";
                    break;
                case MeteorColour.Brown:
                    colour = "Brown";
                    break;
            }

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
            if (!IsDead || MeteorSize == MeteorSize.Small)
                return null;

            var children = new List<Meteor>();
            int amountOfChildren = MeteorColour == MeteorColour.Brown ? 5 : 3;

            for (int i = 0; i < amountOfChildren; i++)
            {
                children.Add(new Meteor(Game, Position, MeteorSize - 1, MeteorColour));
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
            //requires further work to add a randomly generated speed of the meteors instead of a static speed
            Rotation += RotationSpeed; // Change fixed float to property later
            Position += Speed;

            if (Position.X < Globals.GameArea.Left)
                Position = new Vector2(Globals.GameArea.Right, Position.Y);
            if (Position.X > Globals.GameArea.Right)
                Position = new Vector2(Globals.GameArea.Left, Position.Y);
            if (Position.Y < Globals.GameArea.Top)
                Position = new Vector2(Position.X, Globals.GameArea.Bottom);
            if (Position.Y > Globals.GameArea.Bottom)
                Position = new Vector2(Position.X, Globals.GameArea.Top);

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
