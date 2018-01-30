using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class UiLabel : BaseUiComponent
    {
        #region Public constructors
        public UiLabel(AsteroidsGame game, Vector2 position, string text, SpriteFont font) : base(game, position, false, null, text, font) { }
        #endregion

        #region Public overrides
        public override void Update() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position - (Font.MeasureString(Text) / 2), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        } 
        #endregion
    }
}