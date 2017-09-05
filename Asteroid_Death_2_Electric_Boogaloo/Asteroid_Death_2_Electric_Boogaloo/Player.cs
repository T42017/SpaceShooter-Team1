using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Player : Ship
    {

        private Texture2D playerTexture;
        //GraphicsDeviceManager graphics;
        //public bool IsDead { get; set; }
        //public Vector2 Position { get; set; }
        //public float Radius { get; set; }
        //public Vector2 Speed { get; set; }
        //public float Rotation { get; set; }

        public Player(Game game) : base(game)
        {
        }
        
        protected override void LoadContent()
        {
            playerTexture = Game.Content.Load<Texture2D>("shipPlayer");
            //LoadTexture("shipPlayer");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation - MathHelper.PiOver2, new Vector2(playerTexture.Width/2, playerTexture.Height/2), 1.0f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            Position += Speed; 
            if(Position.X < Globals.GameArea.Left)
                Position =new Vector2(Globals.GameArea.Right, Position.Y);
            if (Position.X > Globals.GameArea.Right)
                Position = new Vector2(Globals.GameArea.Left, Position.Y);
            if (Position.Y <Globals.GameArea.Top)
                Position = new Vector2(Position.X, Globals.GameArea.Bottom);
            if (Position.Y > Globals.GameArea.Bottom)
                Position = new Vector2(Position.X, Globals.GameArea.Top);



            base.Update(gameTime);
        }

        public void Retardation()
        {
            Speed -= new Vector2((float) Math.Cos(Rotation),
                         (float) Math.Sin(Rotation)) * 0.09f;
            if (Speed.LengthSquared() > 25)
            {
                Speed = Vector2.Normalize(Speed) * 5;
            }
        }

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(Rotation),
               (float)Math.Sin(Rotation))*0.09f;

            if (Speed.LengthSquared() > 25)
            {
                Speed = Vector2.Normalize(Speed) * 5;
            }
        }
    }
}
