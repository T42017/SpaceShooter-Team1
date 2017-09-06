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
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public int MaxSpeed = 9;
        public int Width { get; set; } 
        public int Height { get; set; }
        public Rectangle Bounds => new Rectangle(
            (int) Position.X - Width / 2,
            (int) Position.Y - Height / 2,
            Width,
            Height
        );

        protected Texture2D Texture;
        protected Game Game;
        protected readonly SpriteBatch SpriteBatch;

        protected GameObject(Game game) : base(game)
        {
            Game = game;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
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
            if ((this is Player && otherGameObject is Laser) || (this is Laser && otherGameObject is Player))
                return false; // Check this when enemies shoot lasers

            int aLittleToMakeCollisionSeemMoreCorrect = 0;
            var theseBounds = new Rectangle(
                (int) Position.X - Texture.Width / 2 + aLittleToMakeCollisionSeemMoreCorrect,
                (int) Position.Y - Texture.Height / 2 + aLittleToMakeCollisionSeemMoreCorrect,
                Texture.Width - 2 * aLittleToMakeCollisionSeemMoreCorrect,
                Texture.Height - 2 * aLittleToMakeCollisionSeemMoreCorrect
            );

            var otherBounds = new Rectangle(
                (int) otherGameObject.Position.X - otherGameObject.Texture.Width / 2 + aLittleToMakeCollisionSeemMoreCorrect,
                (int) otherGameObject.Position.Y - otherGameObject.Texture.Height / 2 + aLittleToMakeCollisionSeemMoreCorrect,
                otherGameObject.Texture.Width - 2 * aLittleToMakeCollisionSeemMoreCorrect,
                otherGameObject.Texture.Height - 2 * aLittleToMakeCollisionSeemMoreCorrect
            );
            return theseBounds.Intersects(otherBounds) || otherBounds.Intersects(theseBounds);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation - MathHelper.PiOver2, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            
            SpriteBatch.End();
            base.Draw(gameTime);
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

        public void Accelerate(float speed)
        {
            Speed += Forward() * speed;

            if (Speed.LengthSquared() > Math.Pow(MaxSpeed, 2))
            {
                Speed = Vector2.Normalize(Speed) * MaxSpeed;
            }
        }
    }
}
