using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class EnemyFactory
    {

        private Random random = new Random();
        private AsteroidsGame _game;

        public EnemyFactory(AsteroidsGame game)
        {
            _game = game;
        }

        public Enemy GetRandomEnemy()
        {
            Enemy.Type enemyType = (Enemy.Type) random.Next(Enum.GetNames(typeof(Enemy.Type)).Length - 1);

            Enemy enemy = new Enemy(_game, enemyType)
            {
                Position = new Vector2(random.Next(Globals.ScreenWidth - 1), random.Next(Globals.ScreenHeight))
            };

            return enemy;
        }

    }
}
