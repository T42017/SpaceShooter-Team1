using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class MissilePowerup : GameObject
    {
        
        public MissilePowerup(AsteroidsGame game, Vector2 position) : base(game)
        {
            Position = position;
        }

        public override void LoadContent()
        {
            Game.Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("PowerupBlueMissile");
            throw new NotImplementedException();
        }
    }
}
