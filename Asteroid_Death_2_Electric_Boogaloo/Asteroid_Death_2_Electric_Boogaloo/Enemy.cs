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
            LoadTexture(GetRandomizeTexture());
            base.LoadContent();
        }

        public string GetRandomizeTexture()
        {
            string type = "enemy";

            string[] types = { "Red", "Green", "Black", "Blue" };

            int randomNumber1 = new Random().Next(types.Length - 1);
            int randomNumber2 = new Random().Next(4) + 1;

            type += types[randomNumber1] + randomNumber2;

            return type;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
