using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class VolumeArrows:BaseUiComponent
    {
        private int _highlight;
        private string _EffectVoume="100%";
        private Texture2D _textureLeft;
        private Texture2D _textureRight;
        private Texture2D _texture, _highlightTexture;
        private AsteroidsGame _game;
        public VolumeArrows(AsteroidsGame game, Vector2 position, bool canBeHighlighted, EventHandler clickEvent, SpriteFont font) : base(game, position, canBeHighlighted, clickEvent, font)
        {
            SpriteFont Font = font;
            _game =(AsteroidsGame) game;
            _textureLeft = _game.Content.Load<Texture2D>("Left");
            _textureRight = _game.Content.Load<Texture2D>("Right");
            _texture = game.Content.Load<Texture2D>("button");
            _highlight = 21;
            _highlightTexture = Game.Content.Load<Texture2D>("playerLife2_red");

        }

        public override void Update()
        {
            if (IsHighlighted)
            {


                if (Input.Instance.ClickLeft())
                {
                    if (_highlight == 0)
                    {
                        _highlight = 21;
                    }
                    else
                    {
                        _highlight--;
                    }
                }

                if (Input.Instance.ClickRight())
                {
                    if (_highlight == 21)
                    {
                        _highlight = 0;
                    }
                    else
                    {
                        _highlight++;
                    }
                }
                
            }
            switch (_highlight)
            {
                case 0:
                    _EffectVoume = "0%";
                    Globals._universalEffectVolume = 0;
                    break;

                case 1:
                    _EffectVoume = "5%";
                    Globals._universalEffectVolume = 0.05f;
                    break;

                case 2:
                    _EffectVoume = "10%";
                    Globals._universalEffectVolume = 0.1f;
                    break;

                case 4:
                    _EffectVoume = "15%";
                    Globals._universalEffectVolume = 0.15f;
                    break;
                case 5:
                    _EffectVoume = "20%";
                    Globals._universalEffectVolume = 0.2f;
                    break;
                case 6:
                    _EffectVoume = "25%";
                    Globals._universalEffectVolume = 0.25f;
                    break;
                case 7:
                    _EffectVoume = "30%";
                    Globals._universalEffectVolume = 0.3f;
                    break;
                case 8:
                    _EffectVoume = "35%";
                    Globals._universalEffectVolume = 0.35f;
                    break;
                case 9:
                    _EffectVoume = "40%";
                    Globals._universalEffectVolume = 0.4f;
                    break;
                case 10:
                    _EffectVoume = "45%";
                    Globals._universalEffectVolume = 0.45f;
                    break;
                case 11:
                    _EffectVoume = "50%";
                    Globals._universalEffectVolume = 0.5f;
                    break;
                case 12:
                    _EffectVoume = "55%";
                    Globals._universalEffectVolume = 0.55f;
                    break;
                case 13:
                    _EffectVoume = "60%";
                    Globals._universalEffectVolume = 0.6f;
                    break;
                case 14:
                    _EffectVoume = "65%";
                    Globals._universalEffectVolume = 0.55f;
                    break;
                case 15:
                    _EffectVoume = "70%";
                    Globals._universalEffectVolume = 0.7f;
                    break;
                case 16:
                    _EffectVoume = "75%";
                    Globals._universalEffectVolume = 0.75f;
                    break;
                case 17:
                    _EffectVoume = "80%";
                    Globals._universalEffectVolume = 0.8f;
                    break;
                case 18:
                    _EffectVoume = "85%";
                    Globals._universalEffectVolume = 0.85f;
                    break;
                case 19:
                    _EffectVoume = "90%";
                    Globals._universalEffectVolume = 0.9f;
                    break;
                case 20:
                    _EffectVoume = "95%";
                    Globals._universalEffectVolume = 0.95f;
                    break;
                case 21:
                    _EffectVoume = "100%";
                    Globals._universalEffectVolume = 1f;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);
            spriteBatch.DrawString(Font, _EffectVoume, Position - new Vector2((_textureLeft.Width / 2f) + 25f, (_textureLeft.Height / 2f)), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_textureLeft, Position - new Vector2((_textureLeft.Width / 2f) + 120f, _textureLeft.Height / 2f), Color.White);
            spriteBatch.Draw(_textureRight, Position - new Vector2((_textureRight.Width / 2f) - 120f, _textureRight.Height / 2f), Color.White);
            if (IsHighlighted)
            {

                Vector2 textSize = Font.MeasureString(_EffectVoume);
                spriteBatch.Draw(_highlightTexture, Position - new Vector2(_highlightTexture.Width, _highlightTexture.Height / 2f) - new Vector2(textSize.X+20, 0), Color.White);
            }
        }
    }
}
