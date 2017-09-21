using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class TextureManager
    {

        private static TextureManager instance;

        public Texture2D PlayerShipTexture;
        public Texture2D BackGroundTexture;
        public Texture2D PlayerLifeTexture;

        public Texture2D[] PowerUpTextures; 
        public Texture2D[] PixelExplosionTextures;
        public Texture2D[] EnemyTexures;
        public Texture2D[] LaserTextures;
        public Texture2D[] MissileTextures;
        public Texture2D[] HitmarkerTextures { get; set; }

        public SoundEffect ShootSoundEffect;

        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }

        private TextureManager()
        {

        }

        public void LoadContent(ContentManager content)
        {
            PlayerShipTexture = content.Load<Texture2D>("shipPlayer");
            BackGroundTexture = content.Load<Texture2D>("background");
            PlayerLifeTexture = content.Load<Texture2D>("playerLife2_red");

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
            
            
            ShootSoundEffect = content.Load<SoundEffect>("shot");
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
    }
}
