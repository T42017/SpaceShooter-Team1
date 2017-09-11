﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {

        public Player Player { get; private set; }
        public List<GameObject> GameObjects { get; } = new List<GameObject>();
        
        private EnemyFactory _enemyFactory;
        private readonly AsteroidsGame _game;

        public GameObjectManager(AsteroidsGame game)
        {
            _game = game;
        }

        public void AddEnemyFactory(EnemyFactory factory)
        {
            _enemyFactory = factory;
        }

        public void AddEnemys(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var enemy = _enemyFactory.GetRandomEnemy();
                GameObjects.Add(enemy);
            }
        }

        public void AddNewPlayer()
        {
            this.Player = new Player(_game)
            {
                Position = new Vector2(_game.Level.SizeX / 2, _game.Level.SizeY / 2)
            };
            GameObjects.Add(Player);
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

        public void GenerateRandomNewMeteor(GameTime gameTime, int intervalInMilliseconds)
        {
            var currentGameTimeModInterval = (int)gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0) return;

            const int a = 100;
            var respawnArea = new Rectangle(
                (int)Player.Position.X - Player.Width / 2 - a,
                (int)Player.Position.Y - Player.Height / 2 - a,
                Player.Width + 2 * a,
                Player.Height + 2 * a
            );

            Meteor meteor;
            do
            {
                meteor = new Meteor(
                    _game,
                    new Vector2(
                        Globals.RNG.Next(0, Globals.ScreenWidth),
                        Globals.RNG.Next(0, Globals.ScreenHeight)
                    ),
                    (MeteorSize)Globals.RNG.Next(0, 3),
                    (MeteorColour)Globals.RNG.Next(0, 2)
                );
            }
            while (respawnArea.Contains(meteor.Bounds));
            GameObjects.Add(meteor);
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
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                var gameObject = GameObjects[i];
                gameObject.Update();
                CheckForCollisionWith(gameObject);
            }
        }

        public void CheckForCollisionWith(GameObject thisObject)
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                var otherGameObject = GameObjects[i];
                if (thisObject.DistanceToSquared(otherGameObject) <= 100 * 100)
                    continue;
                if (thisObject == otherGameObject || !thisObject.CollidesWith(otherGameObject))
                    continue;
                Debug.WriteLine($"{thisObject} collided with {otherGameObject}");
                return;
            }
        }

        internal void DrawGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                GameObjects[i].Draw(spriteBatch);
            }
        }
    }
}
