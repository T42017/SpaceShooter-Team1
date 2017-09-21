using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.Particles
{
    public class ParticleEngine
    {
        #region Private fields
        private Random _random;
        private List<Particle> _particles;
        private List<Texture2D> _textures;
        #endregion

        #region Public properties
        public Vector2 EmitterLocation { get; set; }
        #endregion

        #region Public constructors
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            _textures = textures;
            _particles = new List<Particle>();
            _random = new Random();
        } 
        #endregion

        #region Private methods
        private Particle GenerateNewParticle()
        {
            Texture2D texture = _textures[_random.Next(_textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(_random.NextDouble() * 2 - 1),
                                    1f * (float)(_random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(_random.NextDouble() * 2 - 1);
            Color color = new Color(
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble());
            float size = (float)_random.NextDouble();
            int ttl = 20 + _random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
        #endregion

        #region Public methods
        public void Update()
        {
            int total = 100;

            for (int i = 0; i < total; i++)
            {
                _particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update();
                if (_particles[particle].TTL <= 0)
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int index = 0; index < _particles.Count; index++)
            {
                _particles[index].Draw(spriteBatch);
            }
        } 
        #endregion
    }
}