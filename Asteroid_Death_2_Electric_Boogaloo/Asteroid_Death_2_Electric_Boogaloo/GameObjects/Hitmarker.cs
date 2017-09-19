using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Hitmarker : GameObject
    {
        private int _aliveTimeInFrames;
        private readonly Texture2D[] _textures;
        private readonly int _amountOfPictures;
        private readonly int _timeBetweenFramesMs;
        private DateTime _timeLastFrame;
        private int _currentTextureIndex;

        public Hitmarker(AsteroidsGame game, Vector2 position) : base(game)
        {
            Position = position;
            Texture = TextureManager.Instance.LoadByName(Game.Content, "hitmarker");
            _amountOfPictures = 9;
            _timeBetweenFramesMs = 100;
            _timeLastFrame = DateTime.Now;
            _textures = TextureManager.Instance.HitmarkerTextures;
            Texture = _textures[_textures.Length - 1];

            _aliveTimeInFrames = (int) ((1.0/3) * 60);
        }

        public override void Update()
        {
            if (_aliveTimeInFrames > 0)
                _aliveTimeInFrames--;
            if (_aliveTimeInFrames == 0)
                IsDead = true;

            if ((DateTime.Now - _timeLastFrame).TotalMilliseconds <= _timeBetweenFramesMs)
                return;

            _currentTextureIndex++;
            if (_currentTextureIndex > _amountOfPictures - 1)
                IsDead = true;
            else
                Texture = _textures[_currentTextureIndex];

            _timeLastFrame = DateTime.Now;
            base.Update();
        }
    }
}
