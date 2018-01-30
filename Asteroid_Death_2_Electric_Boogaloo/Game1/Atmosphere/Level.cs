using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Atmosphere
{
    public class Level
    {
        #region Private fields
        private Texture2D _texture;
        private AsteroidsGame _game;
        #endregion

        #region Public properties
        // Level size in pixels
        public int SizeX { get; }
        public int SizeY { get; }
        #endregion

        #region Public constructors
        public Level(AsteroidsGame game, int sizeX, int sizeY)
        {
            _game = game;
            _texture = TextureManager.Instance.BackGroundTexture;
            SizeX = sizeX * _texture.Width;
            SizeY = sizeY * _texture.Height;
            SizeX = sizeX * _texture.Width;
            SizeY = sizeY * _texture.Height;
        }
        #endregion

        #region Public methods
        public void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < SizeX; x += _texture.Width)
            {
                for (int y = 0; y < SizeY; y += _texture.Height)
                {
                    spriteBatch.Draw(_texture, new Vector2(x, y), Color.White);
                }
            }
        } 
        #endregion
    }
}