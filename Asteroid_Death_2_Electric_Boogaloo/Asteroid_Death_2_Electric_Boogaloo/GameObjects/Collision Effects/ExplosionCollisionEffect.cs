using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class ExplosionCollisionEffect : BaseCollisionEffect
    {
        #region Constructors
        public ExplosionCollisionEffect(AsteroidsGame game, Vector2 position) : base(game, position)
        {
            Texture = TextureManager.Instance.LoadByName(Game.Content, "pixelExplosion");
        }
        #endregion

        #region Public methods
        public bool NoExplosionsNearby()
        {
            return Game.GameObjectManager.Explosions.All(explosion => DistanceToSquared(explosion) > 25 * 25);
        }
        #endregion

        #region Public overrides
        public override void LoadContent() {}
        #endregion
    }
}