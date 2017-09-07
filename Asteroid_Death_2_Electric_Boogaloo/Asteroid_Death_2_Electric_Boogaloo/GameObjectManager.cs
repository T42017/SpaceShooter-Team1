﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {

        public Player Player { get; private set; }
        public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();

        
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

        internal void RemoveDeadGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].IsDead)
                {
                    GameObjects.Remove(GameObjects[i]);
                }
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
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }

            for (int i = GameObjects.Count - 1; i >= 0; i--) // htf does this work??
            {
                CheckForCollisionWith(GameObjects[i]);
            }
        }

        public void CheckForCollisionWith(GameObject thisObject)
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                var otherGameObject = GameObjects[i];
                if (otherGameObject == null ||
                    otherGameObject == thisObject)
                    continue;

                if (thisObject.CollidesWith(otherGameObject))
                {
                    otherGameObject.IsDead = true;
                    //if (thisObject is LaserRed laser)
                    //    Components.Remove(laser);
                    return;
                }
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
