using System;
using Asteroid_Death_2_Electric_Boogaloo.AI;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
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

        public BaseAi Ai;

        public Type enemyType;
        private Texture2D _lifeTexture;

        public Enemy(AsteroidsGame game, Type enemyType) : base(game, new Weapon(game, Weapon.Type.Laser, Weapon.Color.Green), 3)
        {
            this.enemyType = enemyType;
            Ai = new BasicEnemyAI(game, this);
            ShootingSpeed = 5000; //500;
            Texture = TextureManager.Instance.EnemyTexures[(int) enemyType];
            _lifeTexture = TextureManager.Instance.PlayerLifeTexture;
        }

        public override void Update()
        {
            base.Update();
            Ai?.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(_lifeTexture, Position, null, Color.White, Game.GameObjectManager.Player.Rotation + MathHelper.DegreesToRadians(90),
                new Vector2(15, 80), 1.0f, SpriteEffects.None, 0);

            spriteBatch.DrawString(MenuComponent.menuFont, "" + Health, Position, Color.OrangeRed,
                Game.GameObjectManager.Player.Rotation + MathHelper.DegreesToRadians(90), new Vector2(-15, 90), 1f, SpriteEffects.None, 0);
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && (otherGameObject is Meteor || (otherGameObject is Projectile projectile && projectile.ParentType == typeof(Player)));
            if (collides)
            {
                if (otherGameObject is Projectile pro)
                {
                    Player.Score = Player.Score + 100;
                    Health -= pro.Damage;
                    pro.IsDead = true;
                }
                
                if (Health <= 0)
                {
                    IsDead = true;
                    Game.GameObjectManager.Player.EnemyKills++;
                    if (otherGameObject is Projectile)
                    {
                        otherGameObject.IsDead = true;
                    }
                }   
            }
            return collides;
        }
    }
}
