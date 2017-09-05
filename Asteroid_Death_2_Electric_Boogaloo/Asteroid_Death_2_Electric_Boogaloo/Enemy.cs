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

        public Enemy(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            LoadTexture(GetRandomizedTexture());
        }

        public string GetRandomizedTexture()
        {
            int randomNumber = Globals.RNG.Next(Enum.GetNames(typeof(Type)).Length - 1);

            return Enum.GetName(typeof(Type), randomNumber);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
