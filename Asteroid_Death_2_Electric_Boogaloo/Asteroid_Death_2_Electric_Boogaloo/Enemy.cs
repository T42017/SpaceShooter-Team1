using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Enemy : Ship
    {

        public Enemy(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            LoadTexture("enemyBlack1");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
