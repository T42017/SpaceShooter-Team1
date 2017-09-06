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

        private Random random = new Random();

        private State currentState = State.MoveAround;
        private Game _game;

        public AI(Game game)
        {
            _game = game;
        }

        public void Update(Enemy enemy)
        {
            if (currentState == State.MoveAround)
            {
                List<Meteor> meteors = GetMeteors();
                enemy.AccelerateForward(0.25f);
                enemy.Move();
            }
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
