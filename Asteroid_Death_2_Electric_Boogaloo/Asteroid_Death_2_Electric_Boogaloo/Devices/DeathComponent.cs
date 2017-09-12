using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class DeathComponent: AstroidsComponent
    {
        private Song song;
        private bool playing;
        public DeathComponent(Game game) : base(game)
        {
            playing = false;
            UpdatableStates =GameState.gameover;
            DrawableStates=GameState.gameover;
        }

        protected override void LoadContent()
        {
            song = Game.Content.Load<Song>("Laugh");

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
