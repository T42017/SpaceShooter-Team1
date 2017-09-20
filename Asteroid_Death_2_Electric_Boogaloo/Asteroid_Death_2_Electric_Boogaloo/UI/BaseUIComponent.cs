using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    public abstract class BaseUiComponent
    {

        protected AsteroidsGame Game;

        public Vector2 Position;
        public bool IsHighlighted = false;
        public bool CanBeHighLighted = false;
        public bool HasClickEvent = false;
        public string Text = "";
        public SpriteFont Font;

        public EventHandler ClickEvent;

        protected BaseUiComponent(AsteroidsGame game, Vector2 position, bool canBeHighlighted, EventHandler clickEvent, string text, SpriteFont font)
        {
            Game = game;
            Position = position + new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            CanBeHighLighted = canBeHighlighted;
            ClickEvent = clickEvent;
            Text = text;
            Font = font;

            if (ClickEvent != null)
                HasClickEvent = true;
        }

        protected BaseUiComponent(AsteroidsGame game, Vector2 position, bool canBeHighlighted, EventHandler clickEvent, SpriteFont font)
        {
            Game = game;
            Position = position + new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            CanBeHighLighted = canBeHighlighted;
            ClickEvent = clickEvent;
            Font = font;

            if (ClickEvent != null)
                HasClickEvent = true;
        }

        protected BaseUiComponent(AsteroidsGame game, Vector2 position, bool canBeHighlighted, EventHandler clickEvent)
        {
            Game = game;
            Position = position + new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            CanBeHighLighted = canBeHighlighted;
            ClickEvent = clickEvent;

            if (ClickEvent != null)
                HasClickEvent = true;
        }

        protected BaseUiComponent(AsteroidsGame game, Vector2 position)
        {
            Game = game;
            Position = position + new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            CanBeHighLighted = false;
            HasClickEvent = false;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
