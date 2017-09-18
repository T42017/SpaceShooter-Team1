using System;
using Asteroid_Death_2_Electric_Boogaloo.Devices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class Enemy : Ship
    {

        public enum Type
        {
            enemyRed1,
            enemyRed2,
            enemyRed3,
            enemyRed4,
            enemyRed5,
            enemyBlue1,
            enemyBlue2,
            enemyBlue3,
            enemyBlue4,
            enemyBlue5,
            enemyGreen1,
            enemyGreen2,
            enemyGreen3,
            enemyGreen4,
            enemyGreen5,
            enemyBlack1,
            enemyBlack2,
            enemyBlack3,
            enemyBlack4,
            enemyBlack5
        }

        public Type enemyType;
        private AI _ai;
        
        public Enemy(AsteroidsGame game, Type enemyType) : base(game)
        {
            this.enemyType = enemyType;
            _ai = new AI(game, this);
            this.enemyType = enemyType;
            ShootingSpeed = 500;
            Weapon = new Weapon(game, Weapon.Type.Laser, Weapon.Color.Green);
            Texture = TextureManager.Instance.EnemyTexures[(int) enemyType];
        }

        public override void LoadContent()
        {
        }

        public override void Update()
        {
            _ai.Update();
            base.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || (otherGameObject is Projectile projectile && projectile.ParentType == typeof(Player)));
            if (collides)
            {
                if (otherGameObject is Projectile pro)
                {
                    Player.score = Player.score + 100;
                    Health -= pro.Damage;
                    pro.IsDead = true;
                }

                if (Health <= 0)
                {
                    IsDead = true;
                }
                
            }
            return collides;
        }
    }
}
