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
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; } 


        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } 
        public int Height { get; set; }

        protected Texture2D _texture;
        protected readonly SpriteBatch _spriteBatch;
        protected readonly Game _game;

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

        public bool CollidesWith(GameObject otherGameObject)
        {
            if ((this is Player && otherGameObject is LaserRed) || (this is LaserRed && otherGameObject is Player))
                return false;
            var fullWidth = Width + otherGameObject.Width;
            var fullHeight = Height + otherGameObject.Height;
            var distanceX = Math.Abs(X - otherGameObject.X);
            var distanceY = Math.Abs(Y - otherGameObject.Y);

            return distanceX < fullWidth && distanceY < fullHeight;
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
