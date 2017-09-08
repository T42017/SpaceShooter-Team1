using System;
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
                Shoot();
                _timeSenceLastShot = DateTime.Now;
            }

            KeyboardState state = Keyboard.GetState();

            GamePadState m_pad;
            m_pad = GamePad.GetState(PlayerIndex.One);

            //Movement using the DPad on the Xbox controller
            if (m_pad.DPad.Up == ButtonState.Pressed) AccelerateForward(0.25f); 

            if (m_pad.DPad.Down == ButtonState.Pressed) AccelerateForward(-0.07f);

            if (m_pad.DPad.Left == ButtonState.Pressed)  Rotation -= 0.04f; 

            if (m_pad.DPad.Right == ButtonState.Pressed) Rotation += 0.04f;


            //Movement using the left joystick on the Xbox controller or the Arrows
            if (gamePadState.ThumbSticks.Left.Y>= 0.3f || (state.IsKeyDown(Keys.Up))) 
                AccelerateForward(0.25f);
           
            if (gamePadState.ThumbSticks.Left.Y <= -0.3f || (state.IsKeyDown(Keys.Down)))
                AccelerateForward(-0.07f);

            if (gamePadState.ThumbSticks.Left.X <= -0.3f ||(state.IsKeyDown(Keys.Left)))
                Rotation -= 0.04f;

            else if (gamePadState.ThumbSticks.Left.X >= 0.3f || (state.IsKeyDown(Keys.Right)))
                Rotation += 0.04f;

            lastKeyboardState = state;

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            
            StayInsideLevel(Game.Level);
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            var collides = base.CollidesWith(otherGameObject);
            if (collides)
            {
                Game.ChangeGameState(GameState.gameover);
            }

            return collides;
        }
    }
}
