using System;
using System.Linq;
using Game1.Enums;
using Game1.GameObjects;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Collision_effects
{
    public class CollisionEffect : GameObject
    {
        #region Private fields
        private readonly Texture2D[] _textures;
        private readonly int _amountOfPictures;
        private readonly int _timeBetweenFramesMs;
        private DateTime _timeLastFrame;
        private int _currentTextureIndex;
        private GameObject _collidedGameObject;
        #endregion

        #region Constructors
        public CollisionEffect(AsteroidsGame game, Vector2 position, CollisionEffectType collisionEffectType) : base(game)
        {
            Position = position;
            _amountOfPictures = 9;
            _timeBetweenFramesMs = 100;
            _timeLastFrame = DateTime.Now;
            if (collisionEffectType == CollisionEffectType.Explosion)
                _textures = TextureManager.Instance.PixelExplosionTextures;
            else if (collisionEffectType == CollisionEffectType.Hitmarker)
                _textures = TextureManager.Instance.HitmarkerTextures;
            Texture = _textures[_textures.Length - 1];
        }
        #endregion

        #region Public methods
        public bool NoCollisionEffectsNearby()
        {
            return Game.GameObjectManager.CollisionEffects.All(collisionEffect => DistanceToSquared(collisionEffect) > 25 * 25);
        }
        #endregion

        #region Public overrides

        public override void Update()
        {
            if ((DateTime.Now - _timeLastFrame).TotalMilliseconds <= _timeBetweenFramesMs)
                return;

            _currentTextureIndex++;
            if (_currentTextureIndex > _amountOfPictures - 1)
                IsDead = true;
            else
                Texture = _textures[_currentTextureIndex];

            _timeLastFrame = DateTime.Now;
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            _collidedGameObject = otherGameObject;
            return false; // TODO (?) Make explosions hurt other GameObjects.
        }
        #endregion
    }
}