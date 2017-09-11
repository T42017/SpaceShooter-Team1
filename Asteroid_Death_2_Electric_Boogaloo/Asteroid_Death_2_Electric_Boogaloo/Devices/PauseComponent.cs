﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices
{
    class PauseComponent : AstroidsComponent
    {
        private KeyboardState lastKeyboardState;
        private SpriteFont font;
        private AsteroidsGame pGame;

        public PauseComponent(Game game) : base(game)
        {
            pGame = (AsteroidsGame) game;

            UpdatableStates = GameState.paused;
            DrawableStates = GameState.paused;
        }
        
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Text");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape))
                pGame.ChangeGameState(GameState.ingame);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.M))
                pGame.ChangeGameState(GameState.Menu);

            lastKeyboardState =Keyboard.GetState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            String Text1,text2;
            Text1 = "Game is paused";
            text2 = "press M to go back to main menu";

            SpriteBatch.Begin();

            SpriteBatch.DrawString(font, Text1,
                new Vector2((pGame.Graphics.PreferredBackBufferWidth / 2) - (pGame.Graphics.PreferredBackBufferWidth / 16),
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8)), Color.Black);
            SpriteBatch.DrawString(font, text2,
                new Vector2(pGame.Graphics.PreferredBackBufferWidth / 2 - (pGame.Graphics.PreferredBackBufferWidth / 12),
                    (pGame.Graphics.PreferredBackBufferHeight / 4) +
                    (pGame.Graphics.PreferredBackBufferHeight / 8)+30), Color.Black);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
