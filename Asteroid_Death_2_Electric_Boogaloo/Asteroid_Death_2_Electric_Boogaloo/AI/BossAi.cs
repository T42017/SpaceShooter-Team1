using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.AI
{
    class BossAi : BaseAi
    {
        private EnemyBoss _boss;
        private Enemy _enemy;
        private int _maxTimeBetweenSpawnsMs = 6000;
        private DateTime _lastSpawnTime = DateTime.Today;

        public BossAi(AsteroidsGame game, EnemyBoss boss) : base(game)
        {
            _boss = boss;
        }

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
    }
}
