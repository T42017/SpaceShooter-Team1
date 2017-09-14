using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Level
    {
        private Texture2D texture;
        private AsteroidsGame _game;
        
        //Level size in pixels
        public int SizeX, SizeY;

        public Level(AsteroidsGame game, int SizeX, int SizeY)
        {
            _game = game;
            texture = TextureManager.Instance.BackGroundTexture;
            this.SizeX = SizeX * texture.Width;
            this.SizeY = SizeY * texture.Height;
            this.SizeX = SizeX*texture.Width;
            this.SizeY = SizeY*texture.Height;
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < SizeX; x += texture.Width)
            {
                for (int y = 0; y < SizeY; y += texture.Height)
                {
                    spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
                }
            }
        }

    }
}
