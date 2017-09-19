using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleAsteroid_Death_2_Electric_Boogaloo.ParticlesEngine2D;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;


namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        
        private SoundEffect _pewEffect;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;
        ParticleEngine particleEngine;
        private List<Texture2D> textures = new List<Texture2D>();
        public static int score = 0;

        public Player(AsteroidsGame game) : base(game, new Weapon(game, Weapon.Type.Missile, Weapon.Color.Blue))
        {
            Health = 12;
            
            ShootingSpeed = 200;
           
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke00"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke01"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke02"));
            textures.Add(Game.Content.Load<Texture2D>("blackSmoke03"));
            Texture = TextureManager.Instance.PlayerShipTexture;
            _lifeTexture = Game.Content.Load<Texture2D>("playerLife2_red");
            _pewEffect = Game.Content.Load<SoundEffect>("Blaster");
            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));
        }

        public override void Update()
        {
            
            if (Input.Instance.HoldUp()) 

            particleEngine.EmitterLocation = Position;
            particleEngine.Update();

                AccelerateForward(0.45f);
           
            if (Input.Instance.HoldDown())
                AccelerateForward(-0.07f);

            if (Input.Instance.HoldLeft()) 
                Rotation -= 0.026f;

            if (Input.Instance.HoldRight())
                Rotation += 0.026f;

            Speed += new Vector2(-Speed.X * 0.015f, -Speed.Y * 0.015f);
            Move();
            
            base.Update();
            
            if (Input.Instance.HoldSelect() && !IsWeaponOverheated())
            {
                Shoot(typeof(Player));
                _pewEffect.Play();
                _timeSenceLastShot = DateTime.Now;
            }
            
            StayInsideLevel();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            particleEngine.Draw(spriteBatch);

            base.Draw(spriteBatch);

            //Draw player health
            spriteBatch.DrawString(MenuComponent.menuFont, Health + " x ", Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(_lifeTexture, Position, null, Color.White, Rotation + MathHelper.DegreesToRadians(90),
                new Vector2(Globals.ScreenWidth / 2 - 70, Globals.ScreenHeight / 2), 1.0f, SpriteEffects.None, 0);

            //Draw score
            spriteBatch.DrawString(MenuComponent.menuFont, "Score: " + score, Position,
                Color.OrangeRed, Rotation + MathHelper.DegreesToRadians(90), new Vector2(-Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 13), 1f, SpriteEffects.None, 0);

        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || otherGameObject is Enemy || otherGameObject is Projectile projectile && projectile.ParentType == typeof(Enemy));
            if (collides)
            {
                if (otherGameObject is Projectile pro)
                {
                    Health -= pro.Damage;
                    pro.IsDead = true;
                   
                }
                if (Health <= 0 || !(otherGameObject is Projectile))
                {
                    IsDead = true;
                    MediaPlayer.Stop();
                    Game.ChangeGameState(GameState.gameover);
                    IngameComponent.playing = false;
                }
            }
            return collides;
        }
    }
}
