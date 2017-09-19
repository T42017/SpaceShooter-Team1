using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Asteroid_Death_2_Electric_Boogaloo.Factorys;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {
        private readonly AsteroidsGame _game;

        private EnemyFactory _enemyFactory;
        private PowerupFactory _powerupFactory;

        public GameObjectManager(AsteroidsGame game)
        {
            _game = game;
            AddThePlayer();
        }

        public Player Player { get; private set; }
        public List<GameObject> GameObjects { get; } = new List<GameObject>();

        public void AddPowerupFactory(PowerupFactory factory)
        {
            _powerupFactory = factory;
        }

        public void AddEnemyFactory(EnemyFactory factory)
        {
            _enemyFactory = factory;
        }

        public void AddEnemys(int amount)
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

        public void AddMeteors(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var position = new Vector2(
                    Globals.RNG.Next(_game.Level.SizeX),
                    Globals.RNG.Next(_game.Level.SizeY)
                );
                var meteor = new Meteor(_game, position, MeteorSize.Big, MeteorColour.Brown)
                {
                    Rotation = (float) Globals.RNG.NextDouble()
                };
                GameObjects.Add(meteor);
            }
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

        public List<Meteor> GetMeteors()
        {
            var meteors = new List<Meteor>();

            for (var i = 0; i < GameObjects.Count; i++)
                if (GameObjects[i] is Meteor meteor)
                    meteors.Add(meteor);

            return meteors;
        }

        internal void RemoveDeadGameObjects()
        {
            for (var i = 0; i < GameObjects.Count; i++)
                if (GameObjects[i].IsDead)
                    GameObjects.Remove(GameObjects[i]);
        }

        public void AddNewMeteors(GameTime gameTime, int amountOfMeteorsToAdd, int intervalInMilliseconds)
        {
            var currentGameTimeModInterval = (int) gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0 ||
                GameObjects.Count(obj => obj is Meteor) >= 100)
                return;
            var hypothenuseSquared = Globals.ScreenWidth * Globals.ScreenWidth / 4 +
                                     Globals.ScreenHeight * Globals.ScreenHeight / 4;
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
                        (MeteorSize) Globals.RNG.Next(1, 3),
                        (MeteorColour) Globals.RNG.Next(0, 2)
                    );
                } while (Player.DistanceToSquared(meteor) <= hypothenuseSquared);
                GameObjects.Add(meteor);
            }
        }

        public Enemy[] GetEnemys()
        {
            var enemys = new List<Enemy>();

            for (var i = 0; i < GameObjects.Count; i++)
                if (GameObjects[i] is Enemy)
                    enemys.Add((Enemy) GameObjects[i]);

            return enemys.ToArray();
        }

        internal void UpdateGameObjects()
        {
            for (var i = 0; i < GameObjects.Count; i++)
            {
                var gameObject = GameObjects[i];
                gameObject.Update();
                CheckForCollisionWith(gameObject);
            }
        }

        public void CheckForCollisionWith(GameObject thisObject)
        {
            foreach (var otherGameObject in GameObjects)
            {
                if (thisObject == otherGameObject ||
                    !thisObject.CollidesWith(otherGameObject))
                    continue;
                Debug.WriteLine($"{thisObject} collided with {otherGameObject}");
                return;
            }
        }

        internal void DrawGameObjects(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < GameObjects.Count; i++)
                GameObjects[i].Draw(spriteBatch);
        }
    }
}