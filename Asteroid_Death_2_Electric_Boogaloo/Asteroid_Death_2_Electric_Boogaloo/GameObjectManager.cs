using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {

        public Player player { get; private set; }
        public List<GameObject> GameObjects = new List<GameObject>();

        
        private EnemyFactory _enemyFactory;
        private readonly AsteroidsGame _game;

        public GameObjectManager(AsteroidsGame game)
        {
            _game = game;
            AddNewPlayer();
        }

        public void AddEnemyFactory(EnemyFactory factory)
        {
            _enemyFactory = factory;
        }

        public void AddEnemys(int amount)
        {
            for (int i = 0; i < amount; i++)
                GameObjects.Add(_enemyFactory.GetRandomEnemy());
        }

        public void AddNewPlayer()
        {
            this.player = new Player(_game)
            {
                Position = new Vector2(_game.Level.SizeX / 2, _game.Level.SizeY / 2)
            };
            GameObjects.Add(player);
        }

        public void AddMeteors(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(_game.Level.SizeX),
                    Globals.RNG.Next(_game.Level.SizeY)
                );
                Meteor meteor = new Meteor(_game, position, MeteorSize.Big, MeteorColour.Gray)
                {
                    Rotation = (float)Globals.RNG.NextDouble()
                };
                GameObjects.Add(meteor);
            }
        }

        internal void RemoveDeadGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].IsDead)
                    GameObjects.Remove(GameObjects[i]);
            }
        }

        internal void LoadContent()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].LoadContent();
            }
        }

        internal void UpdateGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }
        }

        internal void DrawGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }
        }
    }
}
