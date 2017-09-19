﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Asteroid_Death_2_Electric_Boogaloo.Factorys;
using System.Runtime.CompilerServices;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class GameObjectManager
    {
        public Player Player { get; private set; }
        public List<GameObject> GameObjects { get; } = new List<GameObject>();
        public List<Explosion> Explosions { get; set; } = new List<Explosion>();
        public List<Hitmarker> Hitmarkers { get; set; } = new List<Hitmarker>();

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

            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i] is Meteor meteor)
                    meteors.Add(meteor);
            }

            return meteors;
        }

        internal void RemoveDeadGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].IsDead)
                {
                    GameObjects.Remove(GameObjects[i]);
        }

        public void RemoveDeadExplosions()
        {
            Explosions.RemoveAll(explosion => explosion.IsDead);
        }

        public void RemoveDeadHitmarkers()
        {
            Hitmarkers.RemoveAll(hitmarker => hitmarker.IsDead);
        }

        public void AddNewMeteors(GameTime gameTime, int amountOfMeteorsToAdd, int intervalInMilliseconds)
        {
            int currentGameTimeModInterval = (int) gameTime.TotalGameTime.TotalMilliseconds % intervalInMilliseconds;
            if (currentGameTimeModInterval != 0 || GameObjects.Count(obj => obj is Meteor) >= 100)
                return;

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
                }
                while (Player.DistanceToSquared(meteor) <= hypothenuseSquared);
                GameObjects.Add(meteor);
            }
        }

        public Enemy[] GetEnemys()
        {
            var enemys = new List<Enemy>();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i] is Enemy)
                    enemys.Add((Enemy)GameObjects[i]);
            }

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

        public void UpdateExplosions()
        {
            foreach (var explosion in Explosions)
                explosion.Update();
        }

        public void UpdateHitmarkers()
        {
            foreach (var hitmarker in Hitmarkers)
                hitmarker.Update();
        }

        public void CheckForCollisionWith(GameObject thisObject)
        {
            foreach (var otherGameObject in GameObjects)
            {
                if (thisObject == otherGameObject ||
                    !thisObject.CollidesWith(otherGameObject))
                    continue;
                return;
            }
        }

        internal void DrawGameObjects(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
        }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {
            foreach (var explosion in Explosions)
                explosion.Draw(spriteBatch);
        }

        public void DrawHitmarkers(SpriteBatch spriteBatch)
        {
            foreach (var hitmarker in Hitmarkers)
                hitmarker.Draw(spriteBatch);
    }
}