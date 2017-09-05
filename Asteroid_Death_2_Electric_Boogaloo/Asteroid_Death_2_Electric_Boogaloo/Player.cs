using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Player : Ship  
    {
        GraphicsDeviceManager graphics;
        //public bool IsDead { get; set; }
        //public Vector2 Position { get; set; }
        //public float Radius { get; set; }
        //public Vector2 Speed { get; set; }
        //public float Rotation { get; set; }

        public Player(Game game) : base(game)
        {
            Position = new Vector2();
        }

        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
        }
    }
}
