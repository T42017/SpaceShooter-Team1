using System;
using System.Diagnostics;
using Asteroid_Death_2_Electric_Boogaloo.AI;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class EnemyBoss : Ship
    {

        public BaseAi Ai;
        
        public EnemyBoss(AsteroidsGame game) : base(game)
        {
            Health = 1;
            Ai = new BossAi(game, this);
            Texture = TextureManager.Instance.BossTexture;
        }
        
        public override void Update()
        {
            base.Update();
            Ai.Update();

            //if (Vector2.Distance(playerPositioon, Position) < 600 &&
            //    !IsWeaponOverheated())
            //{
            //    Rotation = MathHelper.LookAt(Position, playerPositioon);
            //    Shoot(typeof(Enemy));
            //    Rotation = MathHelper.DegreesToRadians(-90);
            //}
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
