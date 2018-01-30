using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class UiTextbox : BaseUiComponent
    {
        #region Private fields
        private readonly Texture2D _texture;
        private readonly Texture2D _t; //base for the line texture
        private readonly StringBuilder _text = new StringBuilder();
        private const int _framesBetweenBlicks = 25;
        private int _currentFrame = 0;
        private bool _drawUnderScore = false;
        #endregion

        #region Public properties
#pragma warning disable 108,114
        public string Text
#pragma warning restore 108,114
        {
            get { return _text.ToString(); }
            set
            {
                _text.Clear();
                _text.Append(value);
            }
        }
        #endregion

        #region Public constructors
        public UiTextbox(AsteroidsGame game, Vector2 position, SpriteFont font) : base(game, position, true, null, font)
        {
            _texture = Game.Content.Load<Texture2D>("button");

            _t = new Texture2D(game.GraphicsDevice, 1, 1);
            _t.SetData<Color>(
                new Color[] { Color.White });
        }
        #endregion

        #region Private methods
        private void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(_t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Black, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            if (!IsHighlighted)
            {
                _drawUnderScore = false;
                return;
            }

            _currentFrame++;
            if (_currentFrame >= _framesBetweenBlicks)
            {
                _currentFrame -= _framesBetweenBlicks;
                _drawUnderScore = !_drawUnderScore;
            }

            foreach (string str in Input.Instance.GetKeyboardCharacters())
            {
                if (_text.Length < 10)
                    _text.Append(str);
            }

            if (_text.Length > 0 && Input.Instance.ClickBackSpace())
                _text.Remove(_text.Length - 1, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = Font.MeasureString(_text);
            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);

            spriteBatch.DrawString(Font, _text.ToString(), Position - (textSize / 2), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            if (!_drawUnderScore) return;

            Vector2 lineDrawPosition = new Vector2(textSize.X / 2, textSize.Y == 0 ? 16 : textSize.Y / 2);
            DrawLine(spriteBatch, Position + lineDrawPosition + new Vector2(0, 0),
                Position + lineDrawPosition + new Vector2(30, 0));
            DrawLine(spriteBatch, Position + lineDrawPosition + new Vector2(0, -1),
                Position + lineDrawPosition + new Vector2(30, -1));
        }
        #endregion
    }
}