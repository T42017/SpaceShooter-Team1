﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public abstract class GameObject
    {
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; } = Physic.DegreesToRadians(-90);
        public int MaxSpeed = 9;
        public int Width { get; set; } 
        public int Height { get; set; }

        private Rectangle _bounds;

        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        protected Texture2D Texture { get; set; }
        protected AsteroidsGame Game { get; }
        
        protected GameObject(AsteroidsGame game)
        {
            Game = game;
        }

        public void LoadTexture(string name)
        {
            Texture = Game.Content.Load<Texture2D>(name);

            Width = Texture.Width;
            Height = Texture.Height;
        }

        public virtual bool CollidesWith(GameObject otherGameObject)
        {
            return _bounds.Intersects(otherGameObject.Bounds) || otherGameObject.Bounds.Intersects(_bounds);
        }

        public float DistanceToSquared(GameObject otherGameObject)
        {
            if (CollidesWith(otherGameObject))
                return 0f;

            float dx = Math.Abs(Position.X - otherGameObject.Position.X);
            float dy = Math.Abs(Position.Y - otherGameObject.Position.Y);
            return dx * dx + dy * dy;
        }

        private void DrawBounds(SpriteBatch spriteBatch)
        {
            if (_bounds == Rectangle.Empty)
                return;
            var rectangle = new Texture2D(Game.GraphicsDevice, Bounds.Width, Bounds.Height);
            var data = new Color[Bounds.Width * Bounds.Height];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Red;
            rectangle.SetData(data);
            spriteBatch.Draw(rectangle, Position - new Vector2(Bounds.Width / 2f, Bounds.Height / 2f), Color.Red);
        }

        public abstract void LoadContent();

        public virtual void Update()
        {
            //_bounds.X = (int)Position.X - Width  / 2;
            //_bounds.Y = (int)Position.Y - Height / 2;
            //_bounds.Width = _bounds.Height = Math.Max(Width, Height);

            int offset = 20;
            _bounds = new Rectangle(
                (int) Position.X - Width / 2 + offset,
                (int) Position.Y - Height / 2 + offset,
                Math.Max(Width, Height) - offset,
                Math.Max(Width, Height) - offset
            //Width,
            //Height
            );
            //Debug.WriteLine($"{GetType().Name} at ({_bounds.X}, {_bounds.Y})");
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //DrawBounds(spriteBatch);
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation - MathHelper.PiOver2,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f), 1.0f, SpriteEffects.None, 0f);
        }

        public void StayInsideLevel(Level level)
        {
            if (Position.X + Texture.Width / 2 > level.SizeX)
            {
                Position = new Vector2(level.SizeX - Texture.Width / 2, Position.Y);
                if (Speed.X > 0) Speed = new Vector2(0, Speed.Y);
            }
            else if (Position.X - Texture.Width / 2 < 0)
            {
                Position = new Vector2(Texture.Width / 2, Position.Y);
                if (Speed.X < 0) Speed = new Vector2(0, Speed.Y);
            }

            if (Position.Y + Texture.Height / 2 > level.SizeY)
            {
                Position = new Vector2(Position.X, level.SizeY - Texture.Height / 2);
                if (Speed.Y > 0) Speed = new Vector2(Speed.X, 0);
            }
            else if (Position.Y - Texture.Height / 2 < 0)
            {
                Position = new Vector2(Position.X, Texture.Height / 2);
                if (Speed.Y < 0) Speed = new Vector2(Speed.X, 0);
            }

            //if (Position.Y - Texture.Height < Globals.GameArea.Top)
            //    Position = new Vector2(Position.X, Position.Y - Texture.Height);
            //if (Position.Y + Texture.Height > Globals.GameArea.Bottom)
            //    Position = new Vector2(Position.X, Position.Y + Texture.Height);
        }

        public void Move()
        {
            Position += Speed;
        }

        public Vector2 Forward()
        {
            return new Vector2((float) Math.Cos(Rotation),
                (float) Math.Sin(Rotation));
        }

        public void AccelerateForward(float speed)
        {
            Speed += Forward() * speed;

            if (Speed.LengthSquared() > Math.Pow(MaxSpeed, 2))
            {
                Speed = Vector2.Normalize(Speed) * MaxSpeed;
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name} at position ({Position.X}, {Position.Y})";
        }
    }
}
