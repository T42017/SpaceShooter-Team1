using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class UiArrow : BaseUiComponent
    {
        #region Private fields
        private int _highlight;
        private string _difficulty;
        private Texture2D _textureLeft;
        private Texture2D _textureRight;
        private Texture2D _texture, _highlightTexture;
        private SpriteFont _font;
        private AsteroidsGame _game;
        #endregion

        #region Public constructors
        public UiArrow(AsteroidsGame game, Vector2 position) : base(game, position, true, null)
        {
            _game = (AsteroidsGame)game;
            _font = _game.Content.Load<SpriteFont>("diff");
            _textureLeft = _game.Content.Load<Texture2D>("Left");
            _textureRight = _game.Content.Load<Texture2D>("Right");
            _texture = game.Content.Load<Texture2D>("button");
            _highlight = 0;

            _highlightTexture = Game.Content.Load<Texture2D>("playerLife2_red");
        }
        #endregion

        #region Public overrides
        public override void Update()
        {
            if (IsHighlighted)
            {
                
           
            if (Input.Instance.ClickLeft())
            {
                if (_highlight == 0)
                {
                    _highlight = 3;
                }
                else
                {
                    _highlight--;
                }
            }

            if (Input.Instance.ClickRight())
            {
                if (_highlight == 3)
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
                    _difficulty = "Normal";
                    _game.AmountOfEnemies = 10;
                    Globals.Health = 30;
                    Globals.Maxmeteors = 100;
                    Globals.MeteorsPerSecond = 5;
                    break;

                case 1:
                    _difficulty = "Overkill";
                    _game.AmountOfEnemies = 15;
                    Globals.Health = 25;
                    Globals.Maxmeteors = 100;
                    Globals.MeteorsPerSecond = 10;
                    break;

                case 2:
                    _difficulty = "Insane";
                    _game.AmountOfEnemies = 20;
                    Globals.Health = 20;
                    Globals.Maxmeteors = 200;
                    Globals.MeteorsPerSecond = 15;
                    break;

                case 3:
                    _difficulty = "GodTier";
                    _game.AmountOfEnemies = 30;
                    Globals.Health = 15;
                    Globals.Maxmeteors = 300;
                    Globals.MeteorsPerSecond = 15;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = _font.MeasureString(_difficulty);
            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);
            spriteBatch.DrawString(_font, _difficulty, Position - new Vector2((_textureLeft.Width / 2f) + 25f, (_textureLeft.Height / 2f)), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(_textureLeft, Position - new Vector2((_textureLeft.Width / 2f) + 120f, _textureLeft.Height / 2f), Color.White);
            spriteBatch.Draw(_textureRight, Position - new Vector2((_textureRight.Width / 2f) - 120f, _textureRight.Height / 2f), Color.White);
            if(IsHighlighted)
            spriteBatch.Draw(_highlightTexture,Position - new Vector2(_highlightTexture.Width, _highlightTexture.Height / 2f) - new Vector2(textSize.X / 2, 0), Color.White);
        } 
        #endregion
    }
}