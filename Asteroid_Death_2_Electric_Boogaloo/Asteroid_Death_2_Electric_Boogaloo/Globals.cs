using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class Globals
    {
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        public static Random RNG = new Random();
        public static Rectangle GameArea => new Rectangle(-70, 70, ScreenWidth + 140, Screenheight + 140);
    }
}
