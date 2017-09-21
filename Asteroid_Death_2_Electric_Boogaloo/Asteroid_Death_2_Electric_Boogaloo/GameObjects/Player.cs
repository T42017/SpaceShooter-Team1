﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Asteroid_Death_2_Electric_Boogaloo.Components;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Asteroid_Death_2_Electric_Boogaloo.Managers;
using Asteroid_Death_2_Electric_Boogaloo.Particles;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        #region Private fields
        private SoundEffect _pewEffect, alarm;
        private SoundEffectInstance alarm2;
        private DateTime _timeSinceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;
        private ParticleEngine particleEngine;
        private List<Texture2D> _textures = new List<Texture2D>();
        private bool _drawPlayerInRed;
        private int _framesBetweenBlick = 50;
        private int _currentFrame;
        #endregion

        #region Public properties
        public static int Score { get; set; } = 0;
        public bool HasDonePowerupEffect { get; } = false;
        public int EnemyKills { get; set; }
        public List<Powerup> Powerups { get; } = new List<Powerup>(); 

        public bool HasMariostar
        {
            get { return Powerups.Exists(powerup => powerup.PowerupType == PowerupType.Mariostar); }
        }
        #endregion

        #region Public constructors
        public Player(AsteroidsGame game) : base(game, new Weapon(game, Weapon.Type.Laser, Weapon.Color.Red), Globals.Health)
        {
            Health = 10;
            Boost = 60;
            ShootingSpeed = 200;
            _textures.Add(Game.Content.Load<Texture2D>("blackSmoke00"));
            _textures.Add(Game.Content.Load<Texture2D>("blackSmoke01"));
            _textures.Add(Game.Content.Load<Texture2D>("blackSmoke02"));
            _textures.Add(Game.Content.Load<Texture2D>("blackSmoke03"));
            Texture = TextureManager.Instance.PlayerShipTexture;
            _lifeTexture = TextureManager.Instance.PlayerLifeTexture;
            _pewEffect = TextureManager.Instance.ShootSoundEffect;
            alarm = game.Content.Load<SoundEffect>("Alarm");
            alarm2 = alarm.CreateInstance();
            alarm2.IsLooped = true;

            particleEngine = new ParticleEngine(_textures, new Vector2(400, 240));
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            if (!HasMariostar)
                _drawPlayerInRed = false;

            if (HasMariostar)
            {
                _currentFrame++;
                if (_currentFrame > _framesBetweenBlick)
                {
                    _currentFrame -= _framesBetweenBlick;
                    _drawPlayerInRed = !_drawPlayerInRed;
                }
            }

            if (Health <= 5)
            {
                particleEngine.EmitterLocation = Position;
                particleEngine.Update();
                alarm2.Play();
            }

            else
            {
                alarm2.Stop();
            }

            if (Input.Instance.HoldUp())
                AccelerateForward(0.45f);

            if (Input.Instance.HoldDown())
                AccelerateForward(-0.07f);

            if (Input.Instance.HoldLeft())
                Rotation -= 0.026f;

            if (Input.Instance.HoldRight())
                Rotation += 0.026f;

            if (Input.Instance.Boost() && (Boost > 0))
            {
                Boost--;
                MaxSpeed = 50;
                AccelerateForward(50f);
                MaxSpeed = 10;
            }

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            base.Update();

            if (Input.Instance.HoldSelect() && !IsWeaponOverheated())
            {
                Shoot(typeof(Player));
                _pewEffect.Play();
                _timeSinceLastShot = DateTime.Now;
            }

            StayInsideLevel();

            if (EnemyKills > 30)
            {
                EnemyKills -= 30;
                Game.GameObjectManager.AddEnemyBosses(Globals.RNG.Next(3) + 1);
            }

            foreach (var powerup in Powerups)
            {
                powerup.Update();

                if (powerup is PowerupMariostar)
                    Debug.WriteLine(powerup.Timer);
            }
            Powerups.RemoveAll(p => p.Timer <= 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Health <= 5)
                particleEngine.Draw(spriteBatch);

            // Draw player
            spriteBatch.Draw(Texture, Position, null, _drawPlayerInRed ? Color.Red : Color.White, Rotation - Microsoft.Xna.Framework.MathHelper.PiOver2,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f), Scale, SpriteEffects.None, 0f);

            //Draw player health
            spriteBatch.DrawString(MenuComponent.MenuFont, Health + "", Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2((Globals.ScreenWidth / 2f), Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(_lifeTexture, Position, null, Color.White, Rotation + MathHelper.DegreesToRadians(90),
                new Vector2(Globals.ScreenWidth / 2f + 45, Globals.ScreenHeight / 2f + 7), 1.0f, SpriteEffects.None, 0);

            //Draw score
            spriteBatch.DrawString(MenuComponent.MenuFont, "Score: " + Score, Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2(-Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            if (Powerups.Any(powerup => powerup.PowerupType == PowerupType.Mariostar))
            {
                return false;
            }

            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor
            || otherGameObject is Enemy
            || otherGameObject is Powerup
            || otherGameObject is Projectile projectile && projectile.ParentType == typeof(Enemy));

            if (collides)
            {
                if (otherGameObject is Powerup powerup)
                {
                    powerup.DoEffect(this);
                    Powerups.Add(powerup);
                    otherGameObject.IsDead = true;
                }

                if (otherGameObject is Projectile pro)
                {
                    Health -= pro.Damage;
                    pro.IsDead = true;
                }

                if (Health <= 0
                    || !(otherGameObject is Projectile
                    || otherGameObject is Powerup))
                {
                    IsDead = true;
                    MediaPlayer.Stop();
                    alarm2.Stop();
                    Game.ChangeGameState(GameState.GameOver);
                    InGameComponent.Playing = false;
                }
            }
            return collides;
        } 
        #endregion
    }
}