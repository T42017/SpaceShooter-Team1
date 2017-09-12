﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        private KeyboardState lastKeyboardState;
        private GamePadState lastGamePadState;
        private SoundEffect pewEffect;
        private DateTime _timeSenceLastShot = DateTime.Today;
        public Player(AsteroidsGame game) : base(game) { }
      
        public override void LoadContent()
        {
            pewEffect = Game.Content.Load<SoundEffect>("Blaster");
            LoadTexture("shipPlayer");
            ShootingSpeed = 200;
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

            // Fire all lasers!
            base.Update();
            //Debug.WriteLine($"Player position: ({Position.X}, {Position.Y})");
            if ((gamePadState.Buttons.A == ButtonState.Pressed)
                || (state.IsKeyDown(Keys.Space))
                || (gamePadState.Triggers.Right > 0.2))
            {
                Shoot(typeof(Player));
                if (!((DateTime.Now - _timeSenceLastShot).TotalMilliseconds >= ShootingSpeed))
                    return;

                pewEffect.Play();
                _timeSenceLastShot = DateTime.Now;
            }

            lastKeyboardState = state;
            lastGamePadState = gamePadState;
            
            StayInsideLevel();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            //bool collides = base.CollidesWith(otherGameObject) && !(otherGameObject is Laser laser && laser.ParentType == typeof(Player));
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || otherGameObject is Enemy);
            if (collides)
            {
                IsDead = true;
                Game.ChangeGameState(GameState.gameover);
            }
            return collides;
        }
    }
}
