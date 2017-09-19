using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public static class Globals
    {
        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;
        public static Random RNG = new Random();
        public static int Health=10, Maxmeteors= 100, perSecMeteors=10;
        public static Vector2 HalfScreenSize
        {
            get { return new Vector2(ScreenWidth / 2, ScreenHeight / 2); }
        }
    }
}
