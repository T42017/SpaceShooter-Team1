using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class IngameComponent : AstroidsComponent
    {
        private bool hasaddedgameobjetcs,volume;
        public static bool playing;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame _Game;
        private MouseState oldState;
        private GamePadState lastgamePadState;
        private Song song;

        public IngameComponent(Game game) : base(game)
        {
            _Game = (AsteroidsGame) game;

            DrawableStates = GameState.ingame;
            UpdatableStates = GameState.ingame;
            volume = false;
            playing = false;
            MediaPlayer.IsRepeating = true;
        }
        
        protected override void LoadContent()
        {
            song = Game.Content.Load<Song>("Combat");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (volume == false)
            {
                MediaPlayer.Volume = 0.6f;
                volume = true;
            }
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
               
                playing =true;
            }

            if (Input.Instance.Pause())
            {
                _Game.ChangeGameState(GameState.paused);
                volume= false;

            }
            
          

            //for (int i = _Game.Components.Count - 1; i >= 0; i--)
            //{
            //    if (!(_Game.Components[i] is GameObject gameObject))
            //        continue;
            //    _Game.CheckForCollisionWith(gameObject);
            //}
            //_Game.GenerateRandomNewMeteor(gameTime, 5);
            base.Update(gameTime);
        }
    }
}
