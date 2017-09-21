using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.GameObjects;

namespace Asteroid_Death_2_Electric_Boogaloo.AI
{
    class BasicEnemyAI : BaseAi
    {
        #region Public enums
        public enum State
        {
            GoToPosition,
            MoveToPlayer
        }
        #endregion

        #region Private fields
        private State currentState;
        private Vector2 _positionGoTO = new Vector2();
        #endregion

        #region Protected fields
        protected Enemy _enemy;
        #endregion

        #region Public constructors
        public BasicEnemyAI(AsteroidsGame game, Enemy enemy) : base(game)
        {
            _enemy = enemy;
        }
        #endregion

        #region Private methods
        private Vector2 GetRandomPositionInLevel()
        {
            Vector2 vec = Vector2.Zero;

            while (Vector2.Distance(_enemy.Position, vec) < 100 || vec.Equals(Vector2.Zero))
            {
                vec = new Vector2(Globals.RNG.Next(_game.Level.SizeX), Globals.RNG.Next(_game.Level.SizeY));
            }
            return vec;
        } 
        #endregion

        #region Public overrides
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
        #endregion
    }
}