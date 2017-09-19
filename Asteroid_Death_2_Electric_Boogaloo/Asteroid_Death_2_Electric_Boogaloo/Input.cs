using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Input
    {

        public static Input Instance => _instance ?? (_instance = new Input());

        private static Input _instance;

        private KeyboardState _keyboardState = Keyboard.GetState();
        private KeyboardState _lastKeyboardState;

        private GamePadState _gamePadState = GamePad.GetState(PlayerIndex.One);
        private GamePadState _lastGamePadState;

        private bool _hasMovedLeftStick;
        
        private Input()
        {
            
        }

        public bool HoldUp()
        {
            return (_gamePadState.ThumbSticks.Left.Y >= 0.3f)
                   || (_gamePadState.DPad.Up == ButtonState.Pressed)
                   || (_keyboardState.IsKeyDown(Keys.Up))
                   || (_keyboardState.IsKeyDown(Keys.W));
        }

        public bool HoldDown()
        {
            return (_gamePadState.ThumbSticks.Left.Y <= -0.3f)
                || (_gamePadState.DPad.Down == ButtonState.Pressed)
                || (_keyboardState.IsKeyDown(Keys.Down))
                || (_keyboardState.IsKeyDown(Keys.S));
        }

        public bool HoldLeft()
        {
            return (_gamePadState.ThumbSticks.Left.X <= -0.3f)
                   || (_gamePadState.ThumbSticks.Right.X <= -0.3f)
                   || (_gamePadState.DPad.Left == ButtonState.Pressed)
                   || (_keyboardState.IsKeyDown(Keys.Left))
                   || (_keyboardState.IsKeyDown(Keys.A));
        }

        public bool HoldRight()
        {
            return (_gamePadState.ThumbSticks.Left.X >= 0.3f)
                   || (_gamePadState.ThumbSticks.Right.X >= 0.3f)
                   || (_gamePadState.DPad.Right == ButtonState.Pressed)
                   || (_keyboardState.IsKeyDown(Keys.Right))
                   || (_keyboardState.IsKeyDown(Keys.D));
        }
        
        public bool HoldSelect()
        {
            return ((_gamePadState.Buttons.A == ButtonState.Pressed) ||
             (_keyboardState.IsKeyDown(Keys.Space)) ||
             (_gamePadState.Triggers.Right > 0.2));
        }

        public bool ClickSelect()
        {
            return _gamePadState.Buttons.A == ButtonState.Pressed &&
                    _lastGamePadState.Buttons.A == ButtonState.Released
                    ||
                    _gamePadState.Triggers.Right > 0.2 &&
                    _lastGamePadState.Triggers.Right <= 0.2
                    ||
                    _keyboardState.IsKeyDown(Keys.Space) &&
                    _lastKeyboardState.IsKeyUp(Keys.Space);
        }

        public bool ClickLeft()
        {
            return _gamePadState.DPad.Left == ButtonState.Pressed &&
                    _lastGamePadState.DPad.Left == ButtonState.Released
                   || 
                   _gamePadState.ThumbSticks.Left.X <= -0.3f &&
                   !_hasMovedLeftStick
                   || 
                   _keyboardState.IsKeyDown(Keys.Left) &&
                   _lastKeyboardState.IsKeyUp(Keys.Left)
                   || 
                   _keyboardState.IsKeyDown(Keys.A) &&
                   _lastKeyboardState.IsKeyUp(Keys.A);
        }

        public bool ClickRight()
        {
            return _gamePadState.DPad.Right == ButtonState.Pressed &&
                    _lastGamePadState.DPad.Right == ButtonState.Released
                   || 
                   _gamePadState.ThumbSticks.Left.X >= 0.3f && 
                   !_hasMovedLeftStick
                   || 
                   _keyboardState.IsKeyDown(Keys.Right) && 
                   _lastKeyboardState.IsKeyUp(Keys.Right)
                   || 
                   _keyboardState.IsKeyDown(Keys.D) && 
                   _lastKeyboardState.IsKeyUp(Keys.D);
        }

        public bool ClickDown()
        {
            return _gamePadState.DPad.Down == ButtonState.Pressed &&
                   _lastGamePadState.DPad.Down == ButtonState.Released
                   ||
                   _gamePadState.ThumbSticks.Left.Y >= 0.3f &&
                   !_hasMovedLeftStick
                   ||
                   _keyboardState.IsKeyDown(Keys.Down) &&
                   _lastKeyboardState.IsKeyUp(Keys.Down)
                   ||
                   _keyboardState.IsKeyDown(Keys.S) &&
                   _lastKeyboardState.IsKeyUp(Keys.S);
        }

        public bool ClickUp()
        {
            return _gamePadState.DPad.Up == ButtonState.Pressed &&
                   _lastGamePadState.DPad.Up == ButtonState.Released
                   ||
                   _gamePadState.ThumbSticks.Left.Y <= -0.3f &&
                   !_hasMovedLeftStick
                   ||
                   _keyboardState.IsKeyDown(Keys.Up) &&
                   _lastKeyboardState.IsKeyUp(Keys.Up)
                   ||
                   _keyboardState.IsKeyDown(Keys.W) &&
                   _lastKeyboardState.IsKeyUp(Keys.W);
        }

        public bool Pause()
        {
            return _gamePadState.Buttons.Start == ButtonState.Pressed &&
                    _lastGamePadState.Buttons.Start == ButtonState.Released
                   || 
                   _keyboardState.IsKeyDown(Keys.Escape) &&
                   _lastKeyboardState.IsKeyUp(Keys.Escape);
        }

        public bool Boost()
        {
            return _gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                    _lastGamePadState.Buttons.Start == ButtonState.Released
                   ||
                   _keyboardState.IsKeyDown(Keys.E) &&
                   _lastKeyboardState.IsKeyUp(Keys.E);
        }
        public void Update()
        {
            _lastGamePadState = _gamePadState;
            _lastKeyboardState = _keyboardState;

            _gamePadState = GamePad.GetState(PlayerIndex.One);
            _keyboardState = Keyboard.GetState();

            if (_gamePadState.ThumbSticks.Left.Y <= 0.2 && _gamePadState.ThumbSticks.Left.Y >= -0.2)
                _hasMovedLeftStick = false;
        }
    }
}
