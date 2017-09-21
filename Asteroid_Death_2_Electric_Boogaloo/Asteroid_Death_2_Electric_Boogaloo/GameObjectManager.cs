using System.Collections.Generic;
using System.Linq;
using Asteroid_Death_2_Electric_Boogaloo.Factorys;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {
        
        public List<GameObject> GameObjects => _gameObjects;
        #region Private fields

        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private readonly AsteroidsGame _game;
        private EnemyFactory _enemyFactory;
        private PowerupFactory _powerupFactory;
        #endregion

        #region Public properties
        public Player Player { get; private set; }
        public List<GameObject> GameObjects { get; } = new List<GameObject>();
        public List<CollisionEffect> CollisionEffects { get; set; } = new List<CollisionEffect>();
        #endregion

        #region Public constructors
        public GameObjectManager(AsteroidsGame game)
        {
            _game = game;
            AddThePlayer();
        }
        #endregion

        #region Public adding methods
        public void AddPowerupFactory(PowerupFactory factory)
        {
            _powerupFactory = factory;
        }

        public void AddPowerups(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(_game.Level.SizeX),
                    Globals.RNG.Next(_game.Level.SizeY)
                );
                var powerup = _powerupFactory.GetRandomPowerup();
                GameObjects.Add(powerup);
            }
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

        {
            for (var i = 0; i < amount; i++)
            {
                Enemy enemy = _enemyFactory.GetRandomEnemy();
                enemy.Position = GetRandmPositionWithADistanceFromPlayer(1000);
                Add(enemy);
            }
        }

        public void AddEnemies(int amount, Vector2 position)
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
            Player = new Player(_game)
            {
                Position = new Vector2(_game.Level.SizeX / 2, _game.Level.SizeY / 2)
            };
            Add(Player);
        }
        
        public void AddMeteors(GameTime gameTime, int amountOfMeteorsToAdd, int intervalInMilliseconds)
        {
            int currentGameTimeModInterval = (int)gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0 || GameObjects.Count(obj => obj is Meteor) >= Globals.Maxmeteors)
                return;

            int hypothenuseSquared = (Globals.ScreenWidth * Globals.ScreenWidth) / 4 +
                                     (Globals.ScreenHeight * Globals.ScreenHeight) / 4;
            for (var i = 0; i < amountOfMeteorsToAdd; i++)
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
                        (MeteorSize)Globals.RNG.Next(1, 3),
                        (MeteorColour)Globals.RNG.Next(0, 2)
                    );
                } while (Player.DistanceToSquared(meteor) <= hypothenuseSquared);
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
        #endregion

        #region Public getting methods
        public List<Meteor> GetMeteors()
        {
            var meteors = new List<Meteor>();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is Meteor meteor)
                    meteors.Add(meteor);
            }

            return meteors;
        }

        public Enemy[] GetEnemies()
        {
            var enemys = new List<Enemy>();

            {
                if (GameObjects[i] is Enemy)
                    enemys.Add((Enemy)GameObjects[i]);
            }
            return enemys.ToArray();
        }
        #endregion

        #region Public removing methods
        public void RemoveDeadGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
                _gameObjects.Count(obj => obj is Meteor) >= 100)
            {
                if (GameObjects[i].IsDead)
                {
                    GameObjects.Remove(GameObjects[i]);
                }
                _gameObjects.Add(meteor);
            }
        }

        public Vector2 GetRandmPositionWithADistanceFromPlayer(int distance)
        {
            Vector2 position = new Vector2(Globals.RNG.Next(Globals.ScreenWidth), Globals.RNG.Next(Globals.ScreenHeight));

            while (Vector2.Distance(position, Player.Position) < distance)
                position = new Vector2(Globals.RNG.Next(Globals.ScreenWidth), Globals.RNG.Next(Globals.ScreenHeight));

            return position;
        }
        #endregion

        #region Public updating methods
        public void UpdateGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];
                gameObject.Update();
                CheckForCollisionWith(gameObject);
            }
        }

        public void UpdateCollisionEffects()
        {
            foreach (var otherGameObject in _gameObjects)
                collisionEffect.Update();
        }
        #endregion

        #region Public drawing methods
        public void DrawGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }
        }

        public void DrawGameObjects(SpriteBatch spriteBatch)
        {
            foreach (var collisionEffect in CollisionEffects)
                collisionEffect.Draw(spriteBatch);
        }
        #endregion

        #region Public methods
        public void CheckForCollisionWith(GameObject thisObject)
        {
           for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
                    !thisObject.CollidesWith(GameObjects[i]) || i < GameObjects.Count &&
                    Player.DistanceToSquared(GameObjects[i]) >= ((Globals.ScreenWidth * Globals.ScreenWidth) / 2 + (Globals.ScreenHeight * Globals.ScreenHeight) / 2)))
                    continue;
                return;
            }
        } 
        #endregion
    }
}