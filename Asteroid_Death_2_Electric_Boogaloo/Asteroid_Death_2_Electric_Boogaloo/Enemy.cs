using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    internal class Enemy : Ship
    {

        public enum Type
        {
            enemyRed1,
            enemyRed2,
            enemyRed3,
            enemyRed4,
            enemyRed5,
            enemyBlue1,
            enemyBlue2,
            enemyBlue3,
            enemyBlue4,
            enemyBlue5,
            enemyGreen1,
            enemyGreen2,
            enemyGreen3,
            enemyGreen4,
            enemyGreen5,
            enemyBlack1,
            enemyBlack2,
            enemyBlack3,
            enemyBlack4,
            enemyBlack5
        }

        public Type type;
        private AI _ai;
        
        public Enemy(Game game, Type type) : base(game)
        {
            this.type = type;
            _ai = new AI(game);
            this.type = type;
        }

        protected override void LoadContent()
        {
            LoadTexture(Enum.GetName(typeof(Type), type));
        }

        public override void Update(GameTime gameTime)
        {
            _ai.Update(this);

            base.Update(gameTime);
        }

    }
}
