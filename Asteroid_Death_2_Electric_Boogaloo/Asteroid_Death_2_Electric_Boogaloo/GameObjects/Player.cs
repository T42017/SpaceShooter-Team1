using System;
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
    public class Player : Ship
    {
        private KeyboardState lastKeyboardState;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private int _timeForLaserCooldownInMs = 100;

        public Player(AsteroidsGame game) : base(game) { }
      
        public override void LoadContent()
        {
            LoadTexture("shipPlayer");
        }
        
        public override void Update()
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
                AccelerateForward(0.25f);
            if (state.IsKeyDown(Keys.Down))
                AccelerateForward(-0.07f);
            if (state.IsKeyDown(Keys.Left))
                Rotation -= 0.07f;
            else if (state.IsKeyDown(Keys.Right))
                Rotation += 0.07f;
            lastKeyboardState = state;

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();

            StayInsideLevel(Game.Level);
        }
    }
}
