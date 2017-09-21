using System;
using System.Diagnostics;
using Asteroid_Death_2_Electric_Boogaloo.AI;
using Asteroid_Death_2_Electric_Boogaloo.Components;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class EnemyBoss : Ship
    {

        public BaseAi Ai;
        private Texture2D _lifeTexture;

        public EnemyBoss(AsteroidsGame game) : base(game, 30)
        {
            Health = 30;
            Ai = new BossAi(game, this);
            Texture = TextureManager.Instance.BossTexture;
            _lifeTexture = TextureManager.Instance.PlayerLifeTexture;
        }
        
        public override void Update()
        {
            base.Update();
            Ai.Update();
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
