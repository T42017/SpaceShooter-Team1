using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.AI
{
    public abstract class BaseAi
    {
        protected readonly AsteroidsGame _game;

        protected BaseAi(AsteroidsGame game)
        {
            _game = game;
        }

        public abstract void Update();
        
    }
}
