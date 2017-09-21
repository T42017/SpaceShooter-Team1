using System;
using Microsoft.Xna.Framework;

using Asteroid_Death_2_Electric_Boogaloo.GameObjects;

namespace Asteroid_Death_2_Electric_Boogaloo.Factories
{
    public class EnemyFactory
    {
        #region Private fields
        private AsteroidsGame _game;
        #endregion

        #region Public constructors
        public EnemyFactory(AsteroidsGame game)
        {
            _game = game;
        }
        #endregion

        #region Public methods
        public Enemy GetRandomEnemy()
        {
            Enemy.Type enemyType = (Enemy.Type)Globals.RNG.Next(Enum.GetNames(typeof(Enemy.Type)).Length - 1);

            Vector2 position = Vector2.Zero;
            while (Vector2.Distance(position, _game.GameObjectManager.Player.Position) < 1000 || position.Equals(Vector2.Zero))
                position = new Vector2(Globals.RNG.Next(_game.Level.SizeX - 1), Globals.RNG.Next(_game.Level.SizeY - 1));

            Enemy enemy = new Enemy(_game, enemyType)
            {
                Position = position
            };
            return enemy;
        } 
        #endregion
    }
}