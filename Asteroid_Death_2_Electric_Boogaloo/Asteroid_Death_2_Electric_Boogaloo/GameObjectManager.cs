using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {
        public Player Player { get; private set; }
        public List<GameObject> GameObjects => _gameObjects;

        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private EnemyFactory _enemyFactory;
        private readonly AsteroidsGame _game;

        public GameObjectManager(AsteroidsGame game)
        {
            _game = game;
            AddThePlayer();
        }

        public void AddEnemyFactory(EnemyFactory factory)
        {
            _enemyFactory = factory;
        }

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void AddAtRandomPosition(GameObject gameObject)
        {
            gameObject.Position = GetRandmPositionWithADistanceFromPlayer(1000);
            _gameObjects.Add(gameObject);
        }

        public void AddEnemys(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Enemy enemy = _enemyFactory.GetRandomEnemy();
                enemy.Position = GetRandmPositionWithADistanceFromPlayer(1000);
                Add(enemy);
            }
        }

        public void AddEnemys(int amount, Vector2 position)
        {
            for (int i = 0; i < amount; i++)
            {
                Enemy enemy = _enemyFactory.GetRandomEnemy();
                enemy.Position = position;
                Add(enemy);
            }
        }

        public void AddEnemyBosses(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddAtRandomPosition(new EnemyBoss(_game));
            }
        }

        public void AddThePlayer()
        {
            this.Player = new Player(_game)
            {
                Position = new Vector2(_game.Level.SizeX / 2, _game.Level.SizeY / 2)
            };
            Add(Player);
        }

        public void AddMeteors(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(_game.Level.SizeX),
                    Globals.RNG.Next(_game.Level.SizeY)
                );
                Meteor meteor = new Meteor(_game, position, MeteorSize.Big, MeteorColour.Brown)
                {
                    Rotation = (float)Globals.RNG.NextDouble()
                };
                Add(meteor);
            }
        }

        public Enemy[] GetEnemys()
        {
            List<Enemy> enemys = new List<Enemy>();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is Enemy)
                    enemys.Add((Enemy) _gameObjects[i]);
            }

            return enemys.ToArray();
        }

        public EnemyBoss[] GetEnemyBosses()
        {
            List<EnemyBoss> bosses = new List<EnemyBoss>();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is EnemyBoss)
                    bosses.Add((EnemyBoss) _gameObjects[i]);
            }

            return bosses.ToArray();
        }

        public List<Meteor> GetMeteors()
        {
            List<Meteor> meteors = new List<Meteor>();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is Meteor meteor)
                    meteors.Add(meteor);
            }

            return meteors;
        }

        public void RemoveDeadGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].IsDead)
                {
                    _gameObjects.Remove(_gameObjects[i]);
                }
            }
        }

        public void AddNewMeteors(GameTime gameTime, int amountOfMeteorsToAdd, int intervalInMilliseconds)
        {
            int currentGameTimeModInterval = (int) gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0 ||
                _gameObjects.Count(obj => obj is Meteor) >= 100)
                return;
            int hypothenuseSquared = (Globals.ScreenWidth * Globals.ScreenWidth) / 4 + (Globals.ScreenHeight * Globals.ScreenHeight) / 4;
            for (int i = 0; i < amountOfMeteorsToAdd; i++)
            {
                Meteor meteor;
                do
                {
                    meteor = new Meteor(
                        _game,
                        new Vector2(
                            Globals.RNG.Next(0, _game.Level.SizeX),
                            Globals.RNG.Next(0, _game.Level.SizeY)
                        ),
                        (MeteorSize) Globals.RNG.Next(1, 3),
                        (MeteorColour) Globals.RNG.Next(0, 2)
                    );
                } while (Player.DistanceToSquared(meteor) <= hypothenuseSquared);
                _gameObjects.Add(meteor);
            }
        }

        public Vector2 GetRandmPositionWithADistanceFromPlayer(int distance)
        {
            Vector2 position = new Vector2(Globals.RNG.Next(Globals.ScreenWidth), Globals.RNG.Next(Globals.ScreenHeight));

            while (Vector2.Distance(position, Player.Position) < distance)
            {
                position = new Vector2(Globals.RNG.Next(Globals.ScreenWidth), Globals.RNG.Next(Globals.ScreenHeight));
            }

            return position;
        }

        internal void UpdateGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];
                gameObject.Update();
                CheckForCollisionWith(gameObject);
            }
        }

        public void CheckForCollisionWith(GameObject thisObject)
        {
            foreach (var otherGameObject in _gameObjects)
            {
                if (thisObject == otherGameObject ||
                    !thisObject.CollidesWith(otherGameObject))
                    continue;
                Debug.WriteLine($"{thisObject} collided with {otherGameObject}");
                return;
            }
        }

        public void DrawGameObjects(SpriteBatch spriteBatch)
        {
           for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
            }
        }
    }
}
