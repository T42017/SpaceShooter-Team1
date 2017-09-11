﻿using System;
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
        public MeteorSize   MeteorSize   { get; }
        public MeteorColour MeteorColour { get; }
        public float RotationSpeed;
        public Meteor(AsteroidsGame game, Vector2 position, MeteorSize meteorSize, MeteorColour meteorColour) : base(game)
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

        public override void LoadContent()
        {
            
        }

        public override void Update()
        {
            //requires further work to add a randomly generated speed of the meteors instead of a static speed
            Rotation += RotationSpeed;
            Move();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            var children = SpawnChildren();
            if (children != null)
            {
                foreach (var child in children)
                    Game.GameObjectManager.GameObjects.Add(child);
            }
            bool collides = base.CollidesWith(otherGameObject);
            if (collides) IsDead = true;
            return collides;
        }
    }
}
