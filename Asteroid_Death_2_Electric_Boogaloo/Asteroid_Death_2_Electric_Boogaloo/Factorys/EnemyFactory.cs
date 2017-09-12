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
        
        private AsteroidsGame _game;

        public EnemyFactory(AsteroidsGame game)
        {
            _game = game;
        }

        public Enemy GetRandomEnemy()
        {
            Enemy.Type enemyType = (Enemy.Type)Globals.RNG.Next(Enum.GetNames(typeof(Enemy.Type)).Length - 1);

            Vector2 position = Vector2.Zero;
            while (Vector2.Distance(position, _game.GameObjectManager.Player.Position) < 400 || position.Equals(Vector2.Zero))
                position = new Vector2(Globals.RNG.Next(_game.Level.SizeX - 1), Globals.RNG.Next(_game.Level.SizeY - 1));
            
            Enemy enemy = new Enemy(_game, enemyType)
            {
                Position = position
                //Position = new Vector2(2000, 2000)
            };

            return enemy;
        }

    }
}
