﻿using System;
using System.Text;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        private KeyboardState _lastKeyboardState;
        private GamePadState _lastGamePadState;
        private SoundEffect _pewEffect;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;

        public Player(AsteroidsGame game) : base(game, new Weapon(game, Weapon.Type.Laser, Weapon.Color.Red))
        {
            Health = 3;
            ShootingSpeed = 200;
        }
        
        public override void LoadContent()
        {
            LoadTexture("shipPlayer");
            _lifeTexture = Game.Content.Load<Texture2D>("playerLife2_red");
            _pewEffect = Game.Content.Load<SoundEffect>("Blaster");
        }

        public override void Update()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            
            KeyboardState state = Keyboard.GetState();
            
            //Movement using the left, right joystick and the Dpad on the Xbox controller or the arrows or WASD on the keyboard
            if ((gamePadState.ThumbSticks.Left.Y >= 0.3f)
                || (gamePadState.DPad.Up == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Up))
                || (state.IsKeyDown(Keys.W))) 
                AccelerateForward(0.25f);
           
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

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            
            base.Update();
            
            if (((gamePadState.Buttons.A == ButtonState.Pressed) ||
                (state.IsKeyDown(Keys.Space)) ||
                (gamePadState.Triggers.Right > 0.2)) &&
                !IsWeaponOverheated())
            {
                Shoot(typeof(Player));
                _pewEffect.Play();
                _timeSenceLastShot = DateTime.Now;
            }

            _lastKeyboardState = state;
            _lastGamePadState = gamePadState;
            
            StayInsideLevel();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(MenuComponent.menuFont, Health + " x ", Position,
                Color.HotPink, Rotation + MathHelper.DegreesToRadians(90), new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);
                
            spriteBatch.Draw(_lifeTexture, Position, null, Color.White, Rotation + MathHelper.DegreesToRadians(90),
                new Vector2(Globals.ScreenWidth / 2 - 70, Globals.ScreenHeight / 2), 1.0f, SpriteEffects.None, 0);
                
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || otherGameObject is Enemy || otherGameObject is Laser laser && laser.ParentType == typeof(Enemy));
            if (collides)
            {
                if (otherGameObject is Laser)
                {
                    Health--;
                    otherGameObject.IsDead = true;
                }
                if (ShouldBeDead() || !(otherGameObject is Laser))
                {
                    IsDead = true;
                    Game.ChangeGameState(GameState.gameover);
                }
            }
            return collides;
        }
    }
}
