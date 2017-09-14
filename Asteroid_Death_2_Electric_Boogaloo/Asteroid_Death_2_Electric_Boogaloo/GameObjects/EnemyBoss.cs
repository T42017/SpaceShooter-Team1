using System;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class EnemyBoss : Ship
    {

        public enum Type
        {
            enemyBoss
        }

        public Type enemyType;
        private AI _ai;
        
        public EnemyBoss(AsteroidsGame game, Type enemyType) : base(game, Laser.Color.Blue)
        {
            this.enemyType = enemyType;
            ShootingSpeed = 1000;
            Texture = TextureManager.Instance.EnemyTexures[(int) enemyType];
        }

        public override void LoadContent()
        {
        }

        public override void Update()
        {
            _ai.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && otherGameObject is Laser laser && laser.ParentType == typeof(Player);
            if (collides)
            {
                IsDead = true;
            }
            return collides;
        }

    }
}
