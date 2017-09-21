using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.GameObjects;

namespace Asteroid_Death_2_Electric_Boogaloo.AI
{
    class FollowPlayerAi : BaseAi
    {
        #region Private fields
        private Enemy _enemy;
        #endregion

        #region Public constructors
        public FollowPlayerAi(AsteroidsGame game, Enemy enemy) : base(game)
        {
            _enemy = enemy;
            _enemy.MaxSpeed = 7;
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            Player player = _game.GameObjectManager.Player;

            _enemy.Rotation = MathHelper.LookAt(_enemy.Position, player.Position);

            if (Vector2.Distance(_enemy.Position, player.Position) < 500 && !_enemy.IsWeaponOverheated())
                _enemy.Shoot(typeof(Enemy));

            _enemy.AccelerateForward(0.25f);
            _enemy.Move();
        } 
        #endregion
    }
}