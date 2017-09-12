using System.Collections.Generic;
using System.Diagnostics;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class AI
    {
        public enum State
        {
            GoToPosition,
            FollowPlayer
        }

        private State currentState = State.GoToPosition;
        private readonly AsteroidsGame _game;
        private Enemy _enemy;

        private Vector2 _positionGoTO = new Vector2();

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
                currentState = State.GoToPosition;

            if (currentState == State.GoToPosition)
            {
                if (_positionGoTO.Equals(Vector2.Zero) || Vector2.Distance(_enemy.Position, _positionGoTO) < 200)
                    _positionGoTO = GetRandomPositionInLevel();

                _enemy.Rotation = MathHelper.LookAt(_enemy.Position, _positionGoTO);
                _enemy.AccelerateForward(0.2f);
                _enemy.Move();
            }
            else if (currentState == State.FollowPlayer)
            {
                _enemy.Rotation = MathHelper.LookAt(_enemy.Position, player.Position);
                _enemy.Shoot(typeof(Enemy));
                _enemy.AccelerateForward(0.1f);
                _enemy.Move();
            }

            _enemy.StayInsideLevel();
        }

        private Vector2 GetRandomPositionInLevel()
        {
            Vector2 vec = Vector2.Zero;

            while (Vector2.Distance(_enemy.Position, vec) < 100 || vec.Equals(Vector2.Zero))
            {
                vec = new Vector2(Globals.RNG.Next(_game.Level.SizeX), Globals.RNG.Next(_game.Level.SizeY));
            }

            return vec;
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
