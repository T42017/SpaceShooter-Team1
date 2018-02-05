using System;
using Microsoft.Xna.Framework;

namespace Game1
{
    public static class Globals
    {
        #region Public static properties
        public static int ScreenWidth { get; set; } = 1920;
        public static int ScreenHeight { get; set; } = 1080;
        public static Random RNG { get; set; } = new Random();
        public static int Health { get; set; } = 10;
        public static int Maxmeteors { get; set; } = 100;
        public static int MeteorsPerSecond { get; set; } = 10;
        public static Vector2 HalfScreenSize => new Vector2(ScreenWidth / 2f, ScreenHeight / 2f);
        public static float universalMusicVolume { get; set; } = 1f;
        public static float universalEffectVolume { get; set; } = 1f;

        #endregion
    }
}