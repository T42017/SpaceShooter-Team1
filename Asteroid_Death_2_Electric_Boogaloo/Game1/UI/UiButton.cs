using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class UiButton : BaseUiComponent
    {
        #region Private fields
        private Texture2D _texture;
        private Texture2D _highlightTexture;
        #endregion

        #region Public constructors
        public UiButton(AsteroidsGame game, Vector2 position, string text, SpriteFont font, EventHandler clickEvent) : base(game, position, true, clickEvent, text, font)
        {
            _texture = Game.Content.Load<Texture2D>("button");
            _highlightTexture = Game.Content.Load<Texture2D>("playerLife2_red");
            
        }
        #endregion

        #region Public overrides
        public override void Update() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = Font.MeasureString(Text);
            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);
            spriteBatch.DrawString(Font, Text, Position - (textSize/2f) , Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            if (IsHighlighted)
                spriteBatch.Draw(_highlightTexture, Position - new Vector2(_highlightTexture.Width, _highlightTexture.Height / 2f) - new Vector2(textSize.X / 2, 0), Color.White);
        } 
        #endregion
    }
}