using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    // TODO: make brown and gray meteors have different health
    public class Meteor : GameObject
    {
        public MeteorSize   MeteorSize   { get; }
        public MeteorColour MeteorColour { get; }
        public float RotationSpeed { get; }
        private SoundEffect yea;
        public Meteor(AsteroidsGame game, Vector2 position, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
        {
            Position = position;
            Speed = new Vector2(
                (float) Globals.RNG.NextDouble(),
                (float) Globals.RNG.NextDouble()
            );
            Rotation = (float) Globals.RNG.NextDouble();
            RotationSpeed =(float) Globals.RNG.Next(12)/100;
            MaxSpeed = Globals.RNG.Next(250);
            MeteorSize = meteorSize;
            MeteorColour = meteorColour;
            SetAppropriateTexture();
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
            Texture = TextureManager.Instance.LoadByName(Game.Content, fileName);
        }

        public IEnumerable<Meteor> SpawnChildren()
        {
            if (MeteorSize == MeteorSize.Small)
                return null;
            return ShatterIntoSmallerMeteors();
        }

        private IEnumerable<Meteor> ShatterIntoSmallerMeteors()
        {
            int amountOfSmallerMeteors = MeteorColour == MeteorColour.Gray ? 5 : 3;
            
            for (int i = 0; i < amountOfSmallerMeteors; i++)
            {
                var offset = new Vector2(
                    Globals.RNG.Next(10, 20),
                    Globals.RNG.Next(10, 20));
            
                yield return new Meteor(Game, Position + offset, MeteorSize - 1, MeteorColour)
                {
                    Speed = new Vector2(
                        Speed.X * Globals.RNG.Next(2, 3) * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1),
                        Speed.Y * Globals.RNG.Next(2, 3) * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1)
                    )
                };
            }

        }

        public override void LoadContent()
        {
            
        }

        public override void Update()
        {
            //requires further work to add a randomly generated speed of the meteors instead of a static speed
            Rotation += RotationSpeed;
            Move();
            base.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
           
            bool collides = base.CollidesWith(otherGameObject) && otherGameObject is Laser;
            if (collides)
            {
               
                var smallerMeteors = SpawnChildren();
                if (smallerMeteors != null)
                {
                    foreach (var meteor in smallerMeteors)
                        Game.GameObjectManager.GameObjects.Add(meteor);
                }
               
                IsDead = true;
                Game.GameObjectManager.GameObjects.Remove(otherGameObject); // To remove laser at correct time
            }
            return collides;
        }
    }
}
