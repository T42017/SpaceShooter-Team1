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

        public bool CollidesWith(GameObject otherGameObject)
        {
            if ((this is Player && otherGameObject is LaserRed) || (this is LaserRed && otherGameObject is Player))
                return false;
            var fullWidth = Width + otherGameObject.Width;
            var fullHeight = Height + otherGameObject.Height;
            var distanceX = Math.Abs(Position.X - otherGameObject.Position.X);
            var distanceY = Math.Abs(Position.Y - otherGameObject.Position.Y);

            return distanceX < fullWidth && distanceY < fullHeight;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation - MathHelper.PiOver2, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void Accelerate(float acceleration)
        {
            Speed += new Vector2((float)Math.Cos(Rotation),
                         (float)Math.Sin(Rotation)) * acceleration;

            if (Speed.LengthSquared() > 81)
            {
                Speed = Vector2.Normalize(Speed) * 9;
            }
        }
    }
}
