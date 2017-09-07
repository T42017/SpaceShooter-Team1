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
        private bool hasaddedgameobjetcs, playing;
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private Song song;

        public IngameComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame)game;
            
           
            DrawableStates = GameState.ingame;
            UpdatableStates = GameState.ingame;

            playing = false;


        }

        protected override void LoadContent()
        {

            song = Game.Content.Load<Song>("Combat");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (playing==false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                playing = true;
            }

            //for (int i = pGame.Components.Count - 1; i >= 0; i--)
            //{
            //    if (!(pGame.Components[i] is GameObject gameObject))
            //        continue;
            //    pGame.CheckForCollisionWith(gameObject);
            //}
            //pGame.GenerateRandomNewMeteor(gameTime, 5);

            base.Update(gameTime);
        }
    }
}
