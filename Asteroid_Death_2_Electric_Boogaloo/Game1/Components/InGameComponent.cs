﻿using Game1.Enums;
using Game1.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Game1.Components
{
    public class InGameComponent : AsteroidsComponent
    {
        #region Private fields
        private AsteroidsGame _Game;
        private Song _song;
        #endregion

        #region Public static properties
        public static bool Playing { get; set; }
        #endregion

        #region Public constructors
        public InGameComponent(Game game) : base(game)
        {
            _Game = (AsteroidsGame)game;
            DrawableStates = GameState.InGame;
            UpdatableStates = GameState.InGame;
            Playing = false;
            MediaPlayer.IsRepeating = true;
        }
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            _song = Game.Content.Load<Song>("InGameTheme");
            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void ChangedState(GameState newState)
        {
            Playing = false;
            base.ChangedState(newState);
        }
        public override void Update(GameTime gameTime)
        {
            
            if (Playing == false)
            {
                MediaPlayer.Volume = Globals.universalMusicVolume;
                MediaPlayer.Stop();
                MediaPlayer.Play(_song);

                Playing = true;
            }

            if (Input.Instance.ClickPause())
            {
                Player.mariostar.Stop();
                Player.alarm2.Stop();
                _Game.ChangeGameState(GameState.Paused);
            }
            base.Update(gameTime);
        } 
        #endregion
    }
}