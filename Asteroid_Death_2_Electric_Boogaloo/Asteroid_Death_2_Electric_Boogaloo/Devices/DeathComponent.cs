using System.Collections.Generic;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    internal class DeathComponent : AstroidsComponent
    {
        private SpriteFont font;

        private readonly Keys[] keysToCheck =
        {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back
        };
        
        private readonly AsteroidsGame Game;
        private bool playing;
        private Song song;
        private Texture2D _backgroundTexture;
        private SoundEffect yes;

        public DeathComponent(Game game) : base(game)
        {
            Game = (AsteroidsGame) game;
            playing = false;
            UpdatableStates = GameState.gameover;
            DrawableStates = GameState.gameover;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Font");
            song = Game.Content.Load<Song>("Laugh");
            _backgroundTexture = Game.Content.Load<Texture2D>("background");

            UiComponents = new List<BaseUiComponent>();
            UiComponents.Add(new UiTextbox(Game, new Vector2()));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (playing == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                playing = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            for (var x = 0; x < 2000; x += _backgroundTexture.Width)
                for (var y = 0; y < 2000; y += _backgroundTexture.Height)
                    SpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}