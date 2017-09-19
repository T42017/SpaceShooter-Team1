using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    class UiArrow : BaseUiComponent
    {

        private Texture2D textureLeft,textureRight;
        private SpriteFont font;
        private int highlight;
        private String difficulty;
    
        public UiArrow(AsteroidsGame game, Vector2 position) : base(game, position, false, null)
        {
            font = Game.Content.Load<SpriteFont>("diff");
            textureLeft = Game.Content.Load<Texture2D>("Left");
            textureRight = Game.Content.Load<Texture2D>("Right");
            highlight = 0;
        }

        public override void Update()
        {
            if (Input.Instance.ClickLeft())
            {
                if (highlight == 0)
                {
                    highlight = 3 ;
                }
                else
                {
                    highlight--;
                }

            }


            if (Input.Instance.ClickRight())
            {
                if (highlight == 3)
                {
                    highlight = 0;
                }
                else
                {
                    highlight++;
                }
            }

            switch (highlight)
            {
                case 0:
                    difficulty = "Normal";

                    break;

                case 1:
                    difficulty = "Overkill";
                    break;

                case 2:
                    difficulty = "Insane";
                    break;

                case 3:
                    difficulty = "GodTier";
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.DrawString(font, difficulty, Position - new Vector2((textureLeft.Width / 2f) +35f,(textureLeft.Height / 2f)-5f), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(textureLeft, Position - new Vector2((textureLeft.Width / 2f)+80f, textureLeft.Height / 2f), Color.White);
            spriteBatch.Draw(textureRight, Position - new Vector2((textureRight.Width / 2f)-80f, textureRight.Height / 2f), Color.White);

        }
    }
}
