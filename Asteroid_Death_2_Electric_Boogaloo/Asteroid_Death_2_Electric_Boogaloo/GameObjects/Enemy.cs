using System;
using Asteroid_Death_2_Electric_Boogaloo.AI;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Microsoft.Xna.Framework.Graphics;

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

        public BaseAi Ai;

        public Type enemyType;
        
        public Enemy(AsteroidsGame game, Type enemyType) : base(game, new Weapon(game, Weapon.Type.Laser, Weapon.Color.Green))
        {
            this.enemyType = enemyType;
            Ai = new BasicEnemyAI(game, this);
            ShootingSpeed = 500;
            Texture = TextureManager.Instance.EnemyTexures[(int) enemyType];
        }

        public override void Update()
        {
            base.Update();
            if (Ai != null)
                Ai.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || (otherGameObject is Projectile projectile && projectile.ParentType == typeof(Player)));
            if (collides)
            {
                if (otherGameObject is Projectile pro)
                {
                    Health -= pro.Damage;
                    otherGameObject.IsDead = true;
                    if (pro.ParentType == typeof(Player))
                    {
                        Game.GameObjectManager.Player.EnemyKills++;
                    }
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
