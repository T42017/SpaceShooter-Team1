﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Player : Ship
    {
        private KeyboardState lastKeyboardState;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private int _timeForLaserCooldownInMs = 100;
        private SoundEffect pewEffect;
        public Player(AsteroidsGame game) : base(game) { }
      
        public override void LoadContent()
        {
            pewEffect = Game.Content.Load<SoundEffect>("Blaster");
            LoadTexture("shipPlayer");
        }
        public GamePadDPad DPad { get; }
        public override void Update()
        {

            var gamePadState = GamePad.GetState(PlayerIndex.One);
            
            if ((gamePadState.Buttons.A == ButtonState.Pressed ||
                 Keyboard.GetState().IsKeyDown(Keys.Space)) && 
                 (DateTime.Now - _timeSenceLastShot).TotalMilliseconds > _timeForLaserCooldownInMs)
            {
                pewEffect.Play();
                Shoot(typeof(Player));
                _timeSenceLastShot = DateTime.Now;
            }

            KeyboardState state = Keyboard.GetState();

            //GamePadState m_pad; // create GamePadState struct
            //m_pad = GamePad.GetState(PlayerIndex.One); // retrieve current controller state
            //if (m_pad.DPad.Up == ButtonState.Pressed) AccelerateForward(0.25f); // do something if DPad up button pressed|
            //if (m_pad.DPad.Left == ButtonState.Pressed) // do something if DPad left button pressed 



            if (gamePadState.ThumbSticks.Left.Y== 1.0f || (state.IsKeyDown(Keys.Up))) 
                AccelerateForward(0.25f);
           
            if (gamePadState.ThumbSticks.Left.Y == -1.0f || (state.IsKeyDown(Keys.Down)))
                AccelerateForward(-0.07f);

            if (gamePadState.ThumbSticks.Left.X == -1.0f ||(state.IsKeyDown(Keys.Left)))
                Rotation -= 0.04f;

            else if (gamePadState.ThumbSticks.Left.X == 1.0f || (state.IsKeyDown(Keys.Right)))
                Rotation += 0.04f;

            lastKeyboardState = state;

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            
            StayInsideLevel(Game.Level);
            base.Update();
            //Debug.WriteLine($"Player position: ({Position.X}, {Position.Y})");
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            //bool collides = base.CollidesWith(otherGameObject) && !(otherGameObject is Laser laser && laser.ParentType == typeof(Player));
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || otherGameObject is Enemy);
            if (collides)
            {
                Game.ChangeGameState(GameState.gameover);
            }
            return collides;
        }
    }
}
