using System;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Player : Ship
    {
        
        private SoundEffect _pewEffect;
        private DateTime _timeSenceLastShot = DateTime.Today;
        private Texture2D _lifeTexture;
        public static int score = 0;

        public Player(AsteroidsGame game) : base(game, new Weapon(game, Weapon.Type.Missile, Weapon.Color.Blue))
        {
            Health = 10;
            ShootingSpeed = 200;
            Texture = TextureManager.Instance.PlayerShipTexture;
            _lifeTexture = Game.Content.Load<Texture2D>("playerLife2_red");
            _pewEffect = Game.Content.Load<SoundEffect>("Blaster");
        }

        public override void Update()
        {
            
            if (Input.Instance.HoldUp()) 
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
