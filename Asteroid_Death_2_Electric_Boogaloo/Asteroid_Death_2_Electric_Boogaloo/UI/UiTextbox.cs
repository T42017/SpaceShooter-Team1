using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    class UiTextbox : BaseUiComponent
    {
        private readonly Texture2D _texture;
        private readonly Texture2D _t; //base for the line texture
        private readonly StringBuilder _text = new StringBuilder();

        private const int _framesBetweenBlicks = 25;
        private int _currentFrame = 0;
        private bool _drawUnderScore = false;

        public string Text
        {
            get => _text.ToString();
            set
            {
                _text.Clear();
                _text.Append(value);
            }
        }

        public UiTextbox(AsteroidsGame game, Vector2 position, SpriteFont font) : base(game, position, true, null, font)
        {
            _texture = Game.Content.Load<Texture2D>("button");

            _t = new Texture2D(game.GraphicsDevice, 1, 1);
            _t.SetData<Color>(
                new Color[] { Color.White });
        }

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

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
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
    }
}
