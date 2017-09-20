﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    class UiArrow : BaseUiComponent
    {

        private Texture2D textureLeft, textureRight, _texture;
        private SpriteFont font;
        private int highlight;
        private String difficulty;
        private AsteroidsGame Game;

        public UiArrow(AsteroidsGame game, Vector2 position) : base(game, position, false, null)
        {
            Game = (AsteroidsGame) game;
            font = Game.Content.Load<SpriteFont>("diff");
            textureLeft = Game.Content.Load<Texture2D>("Left");
            textureRight = Game.Content.Load<Texture2D>("Right");
            _texture = game.Content.Load<Texture2D>("button");
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
                    Game.AmountOfEnemys = 10;
                    Globals.Health = 30;
                    Globals.Maxmeteors = 50;
                    Globals.perSecMeteors = 5;
                    break;

                case 1:
                    difficulty = "Overkill";
                    Game.AmountOfEnemys = 15;
                    Globals.Health = 25;
                    Globals.Maxmeteors = 100;
                    Globals.perSecMeteors = 10;
                    break;

                case 2:
                    difficulty = "Insane";
                    Game.AmountOfEnemys = 20;
                    Globals.Health = 20;
                    Globals.Maxmeteors = 200;
                    Globals.perSecMeteors =15;
                    break;

                case 3:
                    difficulty = "GodTier";
                    Game.AmountOfEnemys = 30;
                    Globals.Health = 15;
                    Globals.Maxmeteors = 300;
                    Globals.perSecMeteors =15;
                    break;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position - new Vector2(_texture.Width / 2f, _texture.Height / 2f), Color.White);
            spriteBatch.DrawString(font, difficulty, Position - new Vector2((textureLeft.Width / 2f) +25f,(textureLeft.Height / 2f)), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(textureLeft, Position - new Vector2((textureLeft.Width / 2f)+120f, textureLeft.Height / 2f), Color.White);
            spriteBatch.Draw(textureRight, Position - new Vector2((textureRight.Width / 2f)-120f, textureRight.Height / 2f), Color.White);

        }
    }
}
