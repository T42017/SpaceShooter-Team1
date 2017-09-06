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
            none,
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

        public Type type = Type.none;

        public Enemy(Game game) : base(game)
        {
        }

        public Enemy(Game game, Type type) : base(game)
        {
            this.type = type;
        }

        protected override void LoadContent()
        {
            LoadTexture(type == Type.none ? GetRandomizedTexture() : Enum.GetName(typeof(Type), type));
        }

        public string GetRandomizedTexture()
        {
            var randomNumber = Globals.RNG.Next(Enum.GetNames(typeof(Type)).Length - 1);

            return Enum.GetName(typeof(Type), randomNumber);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
