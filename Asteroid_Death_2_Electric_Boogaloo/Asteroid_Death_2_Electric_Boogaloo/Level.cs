using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
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
        public Level(AsteroidsGame game, int SizeX, int SizeY)
        {
            _game = game;
            _texture = TextureManager.Instance.BackGroundTexture;
            this.SizeX = SizeX * _texture.Width;
            this.SizeY = SizeY * _texture.Height;
            this.SizeX = SizeX * _texture.Width;
            this.SizeY = SizeY * _texture.Height;
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