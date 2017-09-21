﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

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
        private SoundEffect explo,hit;
        private SoundEffectInstance hit1;
        protected Weapon.Color color;

        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType, int damage) : base(game)
        {
            
            hit = Game.Content.Load<SoundEffect>("Hit");
            hit1 = hit.CreateInstance();
            hit1.Volume = 1.0f;
            hit1.Pitch = 0.000001f;
            explo = Game.Content.Load<SoundEffect>("Explo");

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

        protected abstract Type GetClassType();

        public override bool CollidesWith(GameObject otherGameObject)
        {
            bool collides = base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType() && !(otherGameObject is Projectile);
            if (collides)
            {
                var position = new Vector2(Position.X + .25f * Width, Position.Y + .25f * Height);
                var collisionEffectType = GetClassType() == typeof(Missile) 
                    ? CollisionEffectType.Explosion 
                    : CollisionEffectType.Hitmarker;
                var collisionEffect = new CollisionEffect(Game, position, collisionEffectType);
                if (collisionEffect.NoCollisionEffectsNearby())
                    Game.GameObjectManager.CollisionEffects.Add(collisionEffect);
                    explo.Play();
                    hit1.Play();
            }
            return collides;
        }
    }
}
