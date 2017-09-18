using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.AI
{
    class BasicEnemyAI : BaseAi
    {
        public enum State
        {
            GoToPosition,
            MoveToPlayer
        }

        private State currentState;
        private Vector2 _positionGoTO = new Vector2();

        protected Enemy _enemy;

        public BasicEnemyAI(AsteroidsGame game, Enemy enemy) : base(game)
        {
            _enemy = enemy;
        }

        public override void Update()
        {
            Player player = _game.GameObjectManager.Player;

            if (Vector2.Distance(player.Position, _enemy.Position) < 800)
                currentState = State.MoveToPlayer;
            else if (Vector2.Distance(player.Position, _enemy.Position) > 800)
                currentState = State.GoToPosition;

            if (currentState == State.GoToPosition)
            {
                if (_positionGoTO.Equals(Vector2.Zero) || Vector2.Distance(_enemy.Position, _positionGoTO) < 200)
                    _positionGoTO = GetRandomPositionInLevel();

                _enemy.Rotation = MathHelper.LookAt(_enemy.Position, _positionGoTO);
                _enemy.MaxSpeed = 9;
                _enemy.AccelerateForward(0.2f);
                _enemy.Move();
            }
            else if (currentState == State.MoveToPlayer)
            {
                _enemy.Rotation = MathHelper.LookAt(_enemy.Position, player.Position);
                if (_enemy.Weapon != null && !_enemy.IsWeaponOverheated())
                    _enemy.Shoot(typeof(Enemy));
                _enemy.MaxSpeed = 3;
                _enemy.AccelerateForward(0.05f);
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
    }
}
