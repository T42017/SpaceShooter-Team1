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
            FollowPlayer
        }

        private State currentState = State.MoveAround;
        private readonly AsteroidsGame _game;
        private Enemy _enemy;

        public AI(AsteroidsGame game, Enemy enemy)
        {
            _game = game;
            this._enemy = enemy;
        }

        public void Update()
        {
            Player player = _game.GameObjectManager.Player;

            if (Vector2.Distance(player.Position, _enemy.Position) < 300)
                currentState = State.FollowPlayer;
            else if (Vector2.Distance(player.Position, _enemy.Position) > 300)
                currentState = State.MoveAround;

            if (currentState == State.MoveAround)
            {

                if (GetDistanceToClosestMeteor() < 200)
                {
                    _enemy.Rotation = Physic.LookAt(_enemy.Position, GetClosestMeteor().Position) +
                                      Physic.DegreesToRadians(180);
                    _enemy.AccelerateForward(0.1f);
                }
                else
                {
                    _enemy.AccelerateForward(0.25f);
                }

                _enemy.Move();
            }
            else if (currentState == State.FollowPlayer)
            {
                _enemy.Rotation = Physic.LookAt(_enemy.Position, player.Position);
                _enemy.Shoot();
                _enemy.AccelerateForward(0.25f);
                _enemy.Move();
            }

            _enemy.StayInsideLevel();
        }

        private void RandomizeRotation()
        {
            _enemy.Rotation = (float)(Globals.RNG.NextDouble() * (2 * Math.PI));
        }

        private List<Meteor> GetMeteors()
        {
            List<Meteor> meteors = new List<Meteor>();

            foreach (var gameObject in _game.GameObjectManager.GameObjects)
            {
                if (gameObject is Meteor meteor)
                    meteors.Add(meteor);
            }
            return meteors;
        }

        private float GetDistanceToClosestMeteor()
        {
            List<Meteor> meteors = GetMeteors();
           return Vector2.Distance(_enemy.Position, GetClosestMeteor().Position);
        }

        private Meteor GetClosestMeteor()
        {
            List<Meteor> meteors = GetMeteors();
            Meteor meteor = null;

            for (int i = 0; i < meteors.Count; i++)
            {
                if (meteor == null)
                    meteor = meteors[i];

                if (Vector2.Distance(_enemy.Position, meteors[i].Position) < Vector2.Distance(_enemy.Position, meteor.Position))
                    meteor = meteors[i];
            }
            return meteor;
        }

    }
}
