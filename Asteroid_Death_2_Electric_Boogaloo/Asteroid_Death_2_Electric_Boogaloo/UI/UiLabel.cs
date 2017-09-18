using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    public class UiLabel : BaseUiComponent
    {
        public string Text;


        private SpriteFont _font;

        public UiLabel(AsteroidsGame game, Vector2 position, string text, SpriteFont font) : base(game, position, false, null)
        {
            Text = text;
            _font = font;
        }

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Text, Position - (_font.MeasureString(Text) / 2), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
