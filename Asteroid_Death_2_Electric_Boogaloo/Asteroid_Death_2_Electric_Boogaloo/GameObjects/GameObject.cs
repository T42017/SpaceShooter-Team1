using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Asteroid_Death_2_Electric_Boogaloo.Atmosphere;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class GameObject
    {
        #region Private fields
        private Rectangle _bounds;
        #endregion

        #region Protected properties
        protected AsteroidsGame Game { get; }
        #endregion

        #region Public properties
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; } = MathHelper.DegreesToRadians(-90);
        public int MaxSpeed { get; set; } = 10;
        public Texture2D Texture { get; set; }
        public float Scale { get; set; } = 1;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }
        #endregion

        #region Protected constructors
        protected GameObject(AsteroidsGame game)
        {
            Game = game;
        }
        #endregion

        #region Public virtual methods
        public virtual bool CollidesWith(GameObject otherGameObject)
        {
            return _bounds.Intersects(otherGameObject.Bounds) || otherGameObject.Bounds.Intersects(_bounds);
        }

        public virtual void Update()
        {
            int offset = 20;
            _bounds = new Rectangle(
                (int)Position.X - Texture.Width / 2 + offset,
                (int)Position.Y - Texture.Height / 2 + offset,
                Math.Max(Texture.Width, Texture.Height) - offset,
                Math.Max(Texture.Width, Texture.Height) - offset
            );
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation - Microsoft.Xna.Framework.MathHelper.PiOver2,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f), Scale, SpriteEffects.None, 0f);
        }
        #endregion

        #region Public methods
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
            return new Vector2((float)Math.Cos(Rotation),
                (float)Math.Sin(Rotation));
        }

        public void AccelerateForward(float speed)
        {
            Speed += Forward() * speed;

            if (Speed.LengthSquared() > Math.Pow(MaxSpeed, 2))
            {
                Speed = Vector2.Normalize(Speed) * MaxSpeed;
            }
        }
        #endregion

        #region Public overrides
        public override string ToString()
        {
            return $"{GetType().Name} at position ({Position.X}, {Position.Y})";
        } 
        #endregion
    }
}