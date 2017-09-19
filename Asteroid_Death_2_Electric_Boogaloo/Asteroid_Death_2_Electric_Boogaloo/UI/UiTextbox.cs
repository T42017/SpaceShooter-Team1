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
        private Texture2D _texture;
        
        public UiTextbox(AsteroidsGame game, Vector2 position) : base(game, position, true, null)
        {
            _texture = Game.Content.Load<Texture2D>("button");
        }

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);
        }
    }
}
