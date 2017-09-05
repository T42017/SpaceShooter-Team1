﻿using System;
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
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        protected Texture2D _texture;
        protected readonly SpriteBatch _spriteBatch;
        protected readonly Game _game;
        protected static readonly Random Random = new Random();

        protected GameObject(Game game) : base(game)
        {
            _game = game;
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void LoadTexture(string name)
        {
            _texture = _game.Content.Load<Texture2D>(name);

            Width = _texture.Width;
            Height = _texture.Height;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_texture, new Rectangle(X - (Width / 2), Y - (Height / 2), Width, Height), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
