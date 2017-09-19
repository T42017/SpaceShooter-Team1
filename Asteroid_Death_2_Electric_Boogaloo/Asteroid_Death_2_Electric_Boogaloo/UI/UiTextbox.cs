using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    class UiTextbox : BaseUiComponent
    {
        public UiTextbox(AsteroidsGame game, Vector2 position, string startText) : base(game, position, true, null, startText)
        {

        }

        public UiTextbox(AsteroidsGame game, Vector2 position) : base(game, position, true, null)
        {

        }

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
