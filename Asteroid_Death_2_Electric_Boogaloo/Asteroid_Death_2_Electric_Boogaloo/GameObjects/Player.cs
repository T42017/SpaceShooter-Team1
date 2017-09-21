using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleAsteroid_Death_2_Electric_Boogaloo.ParticlesEngine2D;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;


namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        private SoundEffect _pewEffect,alarm;
        private SoundEffectInstance alarm2;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;
        private ParticleEngine particleEngine;
        public int EnemyKills;
        private List<Texture2D> _textures = new List<Texture2D>();

        public static int Score = 0;
        public bool hasDonePowerupEffect = false;

        public List<Powerup> Powerups = new List<Powerup>();
        public bool HasMariostar { get; set; }
        
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
        
        public override void Update()
        {

            if (Health <= 5) {
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
                MaxSpeed = 50;
                AccelerateForward(50f);
                Boost--;
                MaxSpeed = 10;
            }

            

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();          
            base.Update();
            
            if (Input.Instance.HoldSelect() && !IsWeaponOverheated())
            {
                Shoot(typeof(Player));
                _pewEffect.Play();
                _timeSenceLastShot = DateTime.Now;
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

                if (powerup.Timer <= 0)
                {
                    powerup.Remove(this);
                }
            }
            Powerups.RemoveAll(p => p.Timer <=0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Health<=5)
            particleEngine.Draw(spriteBatch);
            base.Draw(spriteBatch);

            //Draw player health
            spriteBatch.DrawString(MenuComponent.menuFont, Health + "", Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2((Globals.ScreenWidth / 2)+35, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(_lifeTexture, Position, null, Color.White, Rotation + MathHelper.DegreesToRadians(90),
                new Vector2(Globals.ScreenWidth / 2 + 40, Globals.ScreenHeight / 2), 1.0f, SpriteEffects.None, 0);

            //Draw score
            spriteBatch.DrawString(MenuComponent.menuFont, "Score: " + Score, Position,
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
                    otherGameObject.IsDead = true;
                    //((Powerup) otherGameObject).DoEffect(this); 
                    Powerups.Add((Powerup) otherGameObject);
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
                    Game.ChangeGameState(GameState.gameover);
                    IngameComponent.playing = false;
                }
            }
            return collides;
        }
    }
}
