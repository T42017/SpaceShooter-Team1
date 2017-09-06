﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Player : Ship
    {
        private KeyboardState lastKeyboardState;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private int _timeForLaserCooldownInMs = 210;

        public Player(Game game) : base(game) { }
      
        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
        }
        
        public override void Update(GameTime gameTime)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                 Keyboard.GetState().IsKeyDown(Keys.Space)) && 
                 (DateTime.Now - _timeSenceLastShot).TotalMilliseconds > _timeForLaserCooldownInMs)
            {
                Shoot();
                _timeSenceLastShot = DateTime.Now;
            }

            KeyboardState state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Up))
                Accelerate(0.25f);
           if (state.IsKeyDown(Keys.Down))
                Accelerate(-0.07f);
            if (state.IsKeyDown(Keys.Left))
                Rotation -= 0.07f;
            else if (state.IsKeyDown(Keys.Right))
                Rotation += 0.07f;
            lastKeyboardState = state;
            
            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Position += Speed;

            if(Position.X < Globals.GameArea.Left)
                Position = new Vector2(Globals.GameArea.Right, Position.Y);
            if (Position.X > Globals.GameArea.Right)
                Position = new Vector2(Globals.GameArea.Left, Position.Y);
            if (Position.Y <Globals.GameArea.Top)
                Position = new Vector2(Position.X, Globals.GameArea.Bottom);
            if (Position.Y > Globals.GameArea.Bottom)
                Position = new Vector2(Position.X, Globals.GameArea.Top);
        }      
    }
}
