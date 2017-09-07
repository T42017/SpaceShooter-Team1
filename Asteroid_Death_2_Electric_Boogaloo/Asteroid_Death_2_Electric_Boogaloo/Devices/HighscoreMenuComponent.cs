using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class HighscoreMenuComponent : AstroidsComponent

    {
        private SpriteFont menuFont, buttonFont;
        private Texture2D Button;
        private AsteroidsGame pGame;
        private MouseState oldState;
        private Song song;
        private bool playing;
        public HighscoreMenuComponent(Game game) : base(game)
        {
            Game.IsMouseVisible = true;
            pGame = (AsteroidsGame)game;

            DrawableStates = GameState.highscoremenu;
            UpdatableStates = GameState.highscoremenu;
            playing = false;


        }

        protected override void LoadContent()
        {
            
            song = Game.Content.Load<Song>("CantinaBand");
           
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
            
            base.Update(gameTime);
        }
    }
}
