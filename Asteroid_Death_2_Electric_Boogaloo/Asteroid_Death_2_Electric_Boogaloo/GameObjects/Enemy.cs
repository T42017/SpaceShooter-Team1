using System;
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

        public Type enemyType;
        private AI _ai;
        
        public Enemy(AsteroidsGame game, Type enemyType) : base(game, Laser.Color.Blue)
        {
            this.enemyType = enemyType;
            _ai = new AI(game, this);
            this.enemyType = enemyType;
            ShootingSpeed = 500;
        }

        public override void LoadContent()
        {
            Texture = TextureManager.Instance.EnemyTexures[(int) enemyType];
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
