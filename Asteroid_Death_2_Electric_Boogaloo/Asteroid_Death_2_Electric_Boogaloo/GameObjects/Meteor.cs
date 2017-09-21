﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Asteroid_Death_2_Electric_Boogaloo.Managers;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    /// <summary>
    /// Represents a meteor as a <see cref="GameObject"/> (TODO: make brown and gray meteors have different health)
    /// </summary>
    public class Meteor : GameObject
    {
        #region Public properties
        public MeteorSize MeteorSize { get; }
        public MeteorColour MeteorColour { get; }
        public float RotationSpeed { get; }
        #endregion

        #region Constructors
        public Meteor(AsteroidsGame game, Vector2 position, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
        {
            Position = position;
            Speed = new Vector2(
                (float) Globals.RNG.NextDouble(),
                (float) Globals.RNG.NextDouble()
            );
            Rotation = (float) Globals.RNG.NextDouble();
            RotationSpeed = (float) Globals.RNG.Next(12)/100;
            MaxSpeed = Globals.RNG.Next(250);
            MeteorSize = meteorSize;
            MeteorColour = meteorColour;
            SetAppropriateTexture();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Sets a filename appropriate to the meteor's size and colour, then loads it via the <see cref="TextureManager"/>
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

        private IEnumerable<Meteor> ShatterIntoSmallerMeteors()
        {
            int amountOfSmallerMeteors = MeteorColour == MeteorColour.Gray ? 5 : 3;

            for (int i = 0; i < amountOfSmallerMeteors; i++)
            {
                var offset = new Vector2(
                    Globals.RNG.Next(-20, 20),
                    Globals.RNG.Next(-20, 20));

                var speed = new Vector2(
                    Speed.X * Globals.RNG.Next(2, 3) * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1) + 5 * (float) Globals.RNG.NextDouble() * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1),
                    Speed.Y * Globals.RNG.Next(2, 3) * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1) + 5 * (float) Globals.RNG.NextDouble() * (Globals.RNG.Next(-1, 2) < 0 ? -1 : 1)
                );

                yield return new Meteor(Game, Position + offset, MeteorSize - 1, MeteorColour)
                {
                    Speed = MeteorColour == MeteorColour.Brown ? speed : speed * 2
                };
            }
        }
        #endregion

        #region Public methods

        public IEnumerable<Meteor> SpawnChildren()
        {
            if (MeteorSize == MeteorSize.Small)
            {
                return null;
            }
            return ShatterIntoSmallerMeteors();
        }

        #endregion

        #region Public overrides
        public override void Update()
        {
            Rotation += RotationSpeed;
            StayInsideLevel();
            Move();
            base.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && otherGameObject is Projectile;
            if (collides)
            {
                Player.Score = Player.Score + 25;
                var smallerMeteors = SpawnChildren();
                if (smallerMeteors != null)
                {
                    foreach (var meteor in smallerMeteors)
                        Game.GameObjectManager.Add(meteor);
                }
                IsDead = true;
            }
            return collides;
        }
        #endregion
    }
}