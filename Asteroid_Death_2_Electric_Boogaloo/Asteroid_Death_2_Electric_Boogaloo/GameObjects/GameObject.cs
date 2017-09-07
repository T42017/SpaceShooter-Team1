﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        protected Texture2D Texture;
        protected AsteroidsGame Game;

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

        public bool CollidesWith(GameObject otherGameObject)
        {
            if ((this is Player && otherGameObject is Laser) || (this is Laser && otherGameObject is Player))
                return false;
            //var fullWidth = Width + otherGameObject.Width;
            //var fullHeight = Height + otherGameObject.Height;
            //var distanceX = Math.Abs(Position.X - otherGameObject.Position.X);
            //var distanceY = Math.Abs(Position.Y - otherGameObject.Position.Y);

            //return distanceX < fullWidth && distanceY < fullHeight;

            var theseBounds = new Rectangle((int) Position.X - Texture.Width, 
                (int) Position.Y - Texture.Height, 
                Texture.Width, 
                Texture.Height
            );

            var otherBounds = new Rectangle((int) otherGameObject.Position.X - otherGameObject.Texture.Width,
                (int) otherGameObject.Position.Y - otherGameObject.Texture.Height,
                otherGameObject.Texture.Width,
                otherGameObject.Texture.Height);

            return theseBounds.Intersects(otherBounds); //|| otherBounds.Intersects(theseBounds);
        }

        public abstract void LoadContent();
        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation - MathHelper.PiOver2, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }

        public void StayInsideLevel()
        {
            if (Position.X + Texture.Width / 2 > Game.Level.SizeX)
            {
                Position = new Vector2(Game.Level.SizeX - Texture.Width / 2, Position.Y);
                if (Speed.X > 0) Speed = new Vector2(0, Speed.Y);
            }
            else if (Position.X - Texture.Width / 2 < 0)
            {
                Position = new Vector2(Texture.Width / 2, Position.Y);
                if (Speed.X < 0) Speed = new Vector2(0, Speed.Y);
            }

            if (Position.Y + Texture.Height / 2 > Game.Level.SizeY)
            {
                Position = new Vector2(Position.X, Game.Level.SizeY - Texture.Height / 2);
                if (Speed.Y > 0) Speed = new Vector2(Speed.X, 0);
            }
            else if (Position.Y - Texture.Height / 2 < 0)
            {
                Position = new Vector2(Position.X, Texture.Height / 2);
                if (Speed.Y < 0) Speed = new Vector2(Speed.X, 0);
            }
        }

        public bool IsOutSideLevel(Level level)
        {
            if (Position.X + Texture.Width / 2 > level.SizeX ||
                Position.X - Texture.Width / 2 < 0 ||
                Position.Y + Texture.Height / 2 > level.SizeY ||
                Position.Y - Texture.Height / 2 < 0)
                return true;

            return false;
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
    }
}
