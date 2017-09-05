using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public abstract class Ship : GameObject
    {
        private bool ShootLefCannon = false;
        
        public Ship(Game game) : base(game)
        {
            
        }

        protected override void LoadContent()
        {
            LoadTexture("shipPlayer");
            base.LoadContent();
        }

        public void Shoot()
        {
            _game.Components.Add(new LaserRed(_game, new Vector2(X + ((Width / 4) * (ShootLefCannon ? -1 : 1)), Y + (Height / 2)), Vector2.Zero));
            ShootLefCannon = !ShootLefCannon;
        }
    }
}
