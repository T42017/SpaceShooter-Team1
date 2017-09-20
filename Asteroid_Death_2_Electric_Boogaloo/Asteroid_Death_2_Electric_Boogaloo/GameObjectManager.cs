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
        #region Private fields
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

        public void AddEnemies(int amount)
        {
            for (var i = 0; i < amount; i++)
                GameObjects.Add(_enemyFactory.GetRandomEnemy());
        }

        public void AddThePlayer()
        {
            Player = new Player(_game)
            {
                Position = new Vector2(_game.Level.SizeX / 2, _game.Level.SizeY / 2)
            };
            GameObjects.Add(Player);
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
                GameObjects.Add(meteor);
            }
        }
        #endregion

        #region Public getting methods
        public List<Meteor> GetMeteors()
        {
            var meteors = new List<Meteor>();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i] is Meteor meteor)
                    meteors.Add(meteor);
            }

            return meteors;
        }

        public Enemy[] GetEnemies()
        {
            var enemys = new List<Enemy>();

            for (int i = 0; i < GameObjects.Count; i++)
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
            {
                if (GameObjects[i].IsDead)
                {
                    GameObjects.Remove(GameObjects[i]);
                }
            }
        }

        public void RemoveDeadCollisionEffects()
        {
            CollisionEffects.RemoveAll(collisionEffect => collisionEffect.IsDead);
        }
        #endregion

        #region Public updating methods
        public void UpdateGameObjects()
        {
            for (var i = 0; i < GameObjects.Count; i++)
            {
                var gameObject = GameObjects[i];
                gameObject.Update();
                CheckForCollisionWith(gameObject);
            }
        }

        public void UpdateCollisionEffects()
        {
            foreach (var collisionEffect in CollisionEffects)
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

        public void DrawCollisionEffects(SpriteBatch spriteBatch)
        {
            foreach (var collisionEffect in CollisionEffects)
                collisionEffect.Draw(spriteBatch);
        }
        #endregion

        #region Public methods
        public void CheckForCollisionWith(GameObject thisObject)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (i < GameObjects.Count && (thisObject == GameObjects[i] ||
                    !thisObject.CollidesWith(GameObjects[i]) || i < GameObjects.Count &&
                    Player.DistanceToSquared(GameObjects[i]) >= ((Globals.ScreenWidth * Globals.ScreenWidth) / 2 + (Globals.ScreenHeight * Globals.ScreenHeight) / 2)))
                    continue;
                return;
            }
        } 
        #endregion
    }
}