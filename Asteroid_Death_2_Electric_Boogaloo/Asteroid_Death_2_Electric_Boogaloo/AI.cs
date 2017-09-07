using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class AI
    {
        public enum State
        {
            MoveAround,
            FoundPlayer
        }

        private State currentState = State.MoveAround;
        private readonly AsteroidsGame _game;
        private Enemy _enemy;

        public AI(AsteroidsGame game, Enemy enemy)
        {
            _game = game;
            this._enemy = enemy;
            RandomizeRotation();
        }

        public void Update()
        {
            if (currentState == State.MoveAround)
            {
                List<Meteor> meteors = GetMeteors();
                _enemy.AccelerateForward(0.25f);
                _enemy.Move();
            }
        }

        private void RandomizeRotation()
        {
            _enemy.Rotation = (float)(Globals.RNG.NextDouble() * (2 * Math.PI));
        }

        private List<Meteor> GetMeteors()
        {
            List<Meteor> meteors = new List<Meteor>();

            foreach (var component in _game.Components)
            {
                if (!(component is Meteor))
                    continue;

                meteors.Add((Meteor)component);
            }
            return meteors;
        }

    }
}
