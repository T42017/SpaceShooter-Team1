using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public abstract class GameObject : DrawableGameComponent
    {
        public Vector2 Position;
        public int Width { get; set; }
        public int Height { get; set; }

        protected Texture2D Texture;
        protected Game _game;
        protected readonly SpriteBatch SpriteBatch;
        protected static readonly Random Random = new Random();

        protected GameObject(Game game) : base(game)
        {
            _game = game;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void LoadTexture(string name)
        {
            Texture = _game.Content.Load<Texture2D>(name);

            Width = Texture.Width;
            Height = Texture.Height;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, new Rectangle((int) (Position.X - (Width / 2)), (int) (Position.Y - (Height / 2)), Width, Height), Color.White);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
