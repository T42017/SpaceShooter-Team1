using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Explosion : GameObject
    {
        #region Private fields
        private readonly Texture2D[] _textures;
        private readonly int _amountOfPictures;
        private readonly int _timeBetweenFramesMs;
        private DateTime _timeLastFrame;
        private int _currentTexture;
        #endregion

        #region Constructors
        public Explosion(AsteroidsGame game, Vector2 position) : base(game)
        {
            Position = position;
            _amountOfPictures = 9;
            _timeBetweenFramesMs = 100;
            _timeLastFrame = DateTime.Now;

            _textures = TextureManager.Instance.PixelExplosionTextures;
            Texture = _textures[_textures.Length - 1];
        }
        #endregion

        #region Static methods
        public bool NoExplosionsNearby()
        {
            foreach (var explosion in Game.GameObjectManager.Explosions)
            {
                if (DistanceToSquared(explosion) < 50)
                    return false;
            }
            return true;
        }
        #endregion

        #region Overrides
        public override void LoadContent() { }

        public override void Update()
        {
            if (!((DateTime.Now - _timeLastFrame).TotalMilliseconds > _timeBetweenFramesMs))
                return;

            _currentTexture++;
            if (_currentTexture > _amountOfPictures - 1)
                IsDead = true;
            else
                Texture = _textures[_currentTexture];

            _timeLastFrame = DateTime.Now;
        }

        public override bool CollidesWith(GameObject otherGameObject) => false; // TODO (?) Make explosions hurt other GameObjects.
        #endregion

    }
}