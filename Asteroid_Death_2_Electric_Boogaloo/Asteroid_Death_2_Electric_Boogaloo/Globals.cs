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
        public static Rectangle GameArea => new Rectangle(-40, -40, ScreenWidth + 80, ScreenHeight + 80);
    }
}
