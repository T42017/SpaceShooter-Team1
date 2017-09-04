﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Player : Ship
    {
        
        private DateTime _timeSenceLastShot = DateTime.Today;
        private int _timeForLaserCooldownInMs = 100;

        public Player(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
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
            
            base.Update(gameTime);
        }

        
    }
}
