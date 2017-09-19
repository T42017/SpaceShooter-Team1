﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public abstract class Projectile : GameObject
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        }

        public int Damage;
        public Type ParentType { get; set; }

        protected Weapon.Color color;

        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType, int damage) : base(game)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            MaxSpeed = 200;
            ParentType = parenType;
            Damage = damage;
            Texture = TextureManager.Instance.LaserTextures[(int) color];
        }

        protected void DieIfOutSideMap()
        {
          if (IsOutSideLevel(Game.Level))
                IsDead = true;
        }

        public override void Update()
        {
            DieIfOutSideMap();
            Speed = Forward() * 1;
            AccelerateForward(1);
            Move();

            base.Update();
        }

        public override bool CollidesWith(GameObject otherGameObject)
        {
            return base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType();
        }
    }
}
