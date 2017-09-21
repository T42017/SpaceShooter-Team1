using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class TextureManager
    {
        #region Private fields
        private static TextureManager _instance;
        #endregion

        #region Public properties
        public Texture2D PlayerShipTexture { get; private set; }
        public Texture2D BackGroundTexture { get; private set; }
        public Texture2D ButtonUnpressedTexture { get; private set; }
        public Texture2D ButtonPressedTexture { get; private set; }
        public Texture2D[] PowerUpTextures { get; private set; }
        public Texture2D[] PixelExplosionTextures { get; private set; }
        public Texture2D[] EnemyTexures { get; private set; }
        public Texture2D[] LaserTextures { get; private set; }
        public Texture2D[] MissileTextures { get; private set; }
        public Texture2D[] HitmarkerTextures { get; private set; }
        public SoundEffect PlayerShootSoundEffect { get; private set; }
        public static TextureManager Instance => _instance ?? (_instance = new TextureManager());
        #endregion

        #region Private constructors
        private TextureManager() {}
        #endregion

        #region Public methods
        public void LoadContent(ContentManager content)
        {
            PlayerShipTexture = content.Load<Texture2D>("shipPlayer");
            BackGroundTexture = content.Load<Texture2D>("background");

            PixelExplosionTextures = GetTextures(content, "pixelExplosion", 9);

            List<Texture2D> enemyTexture2Ds = new List<Texture2D>();
            enemyTexture2Ds.AddRange(GetTextures(content, "enemyRed", 5));
            enemyTexture2Ds.AddRange(GetTextures(content, "enemyBlue", 5));
            enemyTexture2Ds.AddRange(GetTextures(content, "enemyGreen", 5));
            enemyTexture2Ds.AddRange(GetTextures(content, "enemyBlack", 5));
            EnemyTexures = enemyTexture2Ds.ToArray();

            List<Texture2D> laserTexture2Ds = new List<Texture2D>();
            laserTexture2Ds.Add(content.Load<Texture2D>("laserRed"));
            laserTexture2Ds.Add(content.Load<Texture2D>("laserBlue"));
            laserTexture2Ds.Add(content.Load<Texture2D>("laserGreen"));
            LaserTextures = laserTexture2Ds.ToArray();

            List<Texture2D> missileTexture2Ds = new List<Texture2D>();
            missileTexture2Ds.Add(content.Load<Texture2D>("missileRed"));
            missileTexture2Ds.Add(content.Load<Texture2D>("missileBlue"));
            missileTexture2Ds.Add(content.Load<Texture2D>("missileGreen"));
            MissileTextures = missileTexture2Ds.ToArray();

            List<Texture2D> powerupTexture2Ds = new List<Texture2D>();
            powerupTexture2Ds.Add(content.Load<Texture2D>("PowerupMissile"));
            powerupTexture2Ds.Add(content.Load<Texture2D>("powerupHealth"));
            powerupTexture2Ds.Add(content.Load<Texture2D>("powerupBoost"));
            powerupTexture2Ds.Add(content.Load<Texture2D>("powerupMariostar"));
            powerupTexture2Ds.Add(content.Load<Texture2D>("powerupRandom"));
            PowerUpTextures = powerupTexture2Ds.ToArray();

            var hitmarkerTexture2Ds = new List<Texture2D>();
            hitmarkerTexture2Ds.AddRange(GetTextures(content, "hitmarker", 9));
            HitmarkerTextures = hitmarkerTexture2Ds.ToArray();

            PlayerShootSoundEffect = content.Load<SoundEffect>("Blaster");
        }

        public Texture2D LoadByName(ContentManager content, string name)
        {
            return content.Load<Texture2D>(name);
        }

        private Texture2D[] GetTextures(ContentManager content, string name, int amount)
        {
            Texture2D[] textures = new Texture2D[amount];

            for (int i = 0; i < amount; i++)
            {
                textures[i] = content.Load<Texture2D>(name + i);
            }
            return textures;
        } 
        #endregion
    }
}