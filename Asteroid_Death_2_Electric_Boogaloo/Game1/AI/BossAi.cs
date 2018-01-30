using System;
using Game1.GameObjects;
using Microsoft.Xna.Framework;

namespace Game1.AI
{
    class BossAi : BaseAi
    {
        #region Private fields
        private EnemyBoss _boss;
        private Enemy _enemy;
        private int _maxTimeBetweenSpawnsMs = 6000;
        private DateTime _lastSpawnTime = DateTime.Today;
        #endregion

        #region Public constructors
        public BossAi(AsteroidsGame game, EnemyBoss boss) : base(game)
        {
            _boss = boss;
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            Player player = _game.GameObjectManager.Player;

            if (Vector2.Distance(player.Position, _boss.Position) < 900)
            {
                if ((_enemy == null || _enemy.IsDead) && (DateTime.Now - _lastSpawnTime).TotalMilliseconds > _maxTimeBetweenSpawnsMs)
                {
                    Enemy enemy = new Enemy(_game, Enemy.Type.enemyBlack1);
                    enemy.Position = _boss.Position;
                    enemy.Ai = new FollowPlayerAi(_game, enemy);

                    _enemy = enemy;
                    _game.GameObjectManager.Add(enemy);

                    _lastSpawnTime = DateTime.Now;
                }
            }
        } 
        #endregion
    }
}