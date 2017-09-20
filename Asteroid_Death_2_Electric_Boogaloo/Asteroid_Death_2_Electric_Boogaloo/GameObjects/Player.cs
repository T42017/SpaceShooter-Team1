﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.Components;
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
        private KeyboardState _lastKeyboardState;
        private GamePadState _lastGamePadState;
        private SoundEffect alarm;
        private SoundEffectInstance alarm2;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;
        ParticleEngine particleEngine;
        private List<Texture2D> textures = new List<Texture2D>();
        public static int score = 0;
        public bool HasMariostar { get; set; }

        List<Powerup> Powerups = new List<Powerup>();

        public Player(AsteroidsGame game) : base(game, new Weapon(game, Weapon.Type.Laser, Weapon.Color.Green),
            Globals.Health)
        {
            Boost = 180;



            ShootingSpeed = 200;
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke00"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke01"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke02"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke03"));
            Texture = TextureManager.Instance.PlayerShipTexture;
            _lifeTexture = Game.Content.Load<Texture2D>("playerLife2_red");
           
            alarm = Game.Content.Load<SoundEffect>("Alarm");
            alarm2 = alarm.CreateInstance();
            alarm2.IsLooped = true;

            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));
        }

       
        
        public override void Update()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            KeyboardState state = Keyboard.GetState();
            if (Health <= 5) {
                particleEngine.EmitterLocation = Position;
            particleEngine.Update();
                alarm2.Play();
            }
            else
            {
               alarm2.Stop(); 
            }
            
           
            //Movement using the left, right joystick and the Dpad on the Xbox controller or the arrows or WASD on the keyboard
            if ((gamePadState.ThumbSticks.Left.Y >= 0.3f)
                || (gamePadState.DPad.Up == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Up))
                || (state.IsKeyDown(Keys.W)))
                AccelerateForward(0.45f);

            if ((gamePadState.ThumbSticks.Left.Y <= -0.3f)
                || (gamePadState.DPad.Down == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Down))
                || (state.IsKeyDown(Keys.S)))
                AccelerateForward(-0.07f);

            if ((gamePadState.ThumbSticks.Left.X <= -0.3f)
                || (gamePadState.ThumbSticks.Right.X <= -0.3f)
                || (gamePadState.DPad.Left == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Left))
                || (state.IsKeyDown(Keys.A)))
                Rotation -= 0.026f;

            if ((gamePadState.ThumbSticks.Left.X >= 0.3f)
                || (gamePadState.ThumbSticks.Right.X >= 0.3f)
                || (gamePadState.DPad.Right == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Right))
                || (state.IsKeyDown(Keys.D)))
                Rotation += 0.026f;

            if ((gamePadState.Buttons.RightShoulder == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.E))
                && (Boost > 0))
            {
                MaxSpeed = 50;
                AccelerateForward(50f);
                Boost--;
                MaxSpeed = 10; 
            }

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            
            base.Update();
            
            if (((gamePadState.Buttons.A == ButtonState.Pressed) ||
                (state.IsKeyDown(Keys.Space)) ||
                (gamePadState.Triggers.Right > 0.2)) &&
                !IsWeaponOverheated())
            {
                Shoot(typeof(Player));
               
                _timeSenceLastShot = DateTime.Now;
            }

            _lastKeyboardState = state;
            _lastGamePadState = gamePadState;
            
            StayInsideLevel();

            foreach (var powerup in Powerups)
            {
                powerup.DoEffect(this);
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
                new Vector2(Globals.ScreenWidth / 2 - 70, Globals.ScreenHeight / 2), 1.0f, SpriteEffects.None, 0);

            //Draw score
            spriteBatch.DrawString(MenuComponent.menuFont, "Score: " + score, Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2(-Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);

        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            if (HasMariostar)
            {
                return false;
            }

            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor
            || otherGameObject is Enemy
            || otherGameObject is Powerup
            || otherGameObject is Projectile projectile && projectile.ParentType == typeof(Enemy));

            if (collides)
            {
                if (otherGameObject is Powerup)
                {
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
