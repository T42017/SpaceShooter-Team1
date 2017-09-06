using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class PixelExplosion : GameObject
    {
        private string name = "pixelExplosion";
        private Texture2D[] _textures;
        private int _amountOfPictures = 9;
        private int _timeBetweenFramesMs = 200;
        private DateTime _timeLastFrame = DateTime.Now;
        private int currenTexture = 0;

        public PixelExplosion(AsteroidsGame game) : base(game)
        {
            
        }

        public override void LoadContent()
        {
            _textures = new Texture2D[_amountOfPictures];

            for (int i = 0; i < _amountOfPictures; i++)
            {
                LoadTexture(name + (i <= 9 ? "0" + i : "" + i));
                _textures[i] = Texture;
            }
        }

        public override void Update()
        {
            if ((DateTime.Now - _timeLastFrame).TotalMilliseconds > _timeBetweenFramesMs)
            {
                currenTexture++;
                if (currenTexture > _amountOfPictures - 1)
                    Game.GameObjectManager.GameObjects.Add(this);
                else
                    Texture = _textures[currenTexture];

                _timeLastFrame = DateTime.Now;
            }
        }
    }
}
