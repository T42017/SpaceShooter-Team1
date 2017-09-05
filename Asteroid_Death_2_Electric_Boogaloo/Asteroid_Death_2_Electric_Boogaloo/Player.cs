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
        
        GraphicsDeviceManager graphics;

        public Player(Game game) : base(game)
        {
        }
        
        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
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

        
    }
}
