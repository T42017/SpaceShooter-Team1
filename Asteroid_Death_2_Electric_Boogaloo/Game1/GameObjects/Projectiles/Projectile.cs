using System;
using Game1.Collision_effects;
using Game1.Enums;
using Game1.GameObjects.Powerups;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game1.GameObjects.Projectiles
{
    public abstract class Projectile : GameObject
    {
        #region Public enums
        public enum Color
        {
            Red,
            Blue,
            Green
        }
        #endregion
       
        #region Private fields
        private SoundEffect explo, hit;
        #endregion

        #region Protected fields
        protected Weapon.Color color;
        #endregion

        #region Public properties
        public int Damage { get; }
        public Type ParentType { get; set; }
        #endregion

        #region Protected constructors
        protected Projectile(AsteroidsGame game, Vector2 position, float rotation, Weapon.Color color, Type parenType, int damage) : base(game)
        {
            
            hit = Game.Content.Load<SoundEffect>("Hitting");
            explo = Game.Content.Load<SoundEffect>("Explo");

            this.Position = position;
            this.Rotation = rotation;
            this.color = color;
            MaxSpeed = 200;
            ParentType = parenType;
            Damage = damage;
            Texture = TextureManager.Instance.LaserTextures[(int)color];
        }
        #endregion

        #region Protected methods
        protected void DieIfOutSideMap()
        {
            if (IsOutSideLevel(Game.Level))
                IsDead = true;
        } 
        #endregion

        #region Protected abstract methods
        protected abstract Type GetClassType(); 
        #endregion

        #region Public overrides
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
            bool collides = base.CollidesWith(otherGameObject) && ParentType != otherGameObject.GetType() &&
                !(otherGameObject is Projectile) &&
                !(otherGameObject is Powerup) &&
                !(otherGameObject is Player player && player.HasMariostar);
            if (collides)
            {
                var position = new Vector2(Position.X + .25f * Width, Position.Y + .25f * Height);
                var collisionEffectType = GetClassType() == typeof(Missile)
                    ? CollisionEffectType.Explosion
                    : CollisionEffectType.Hitmarker;
                var collisionEffect = new CollisionEffect(Game, position, collisionEffectType);
                if (collisionEffect.NoCollisionEffectsNearby())
                    Game.GameObjectManager.CollisionEffects.Add(collisionEffect);
                if(collisionEffectType==CollisionEffectType.Explosion)
                    explo.Play(0.3f*Globals._universalEffectVolume,0.0f,0.0f);
                 else
                    hit.Play(0.35f*Globals._universalEffectVolume,0.0f,0.0f);
            }
            return collides;
        } 
        #endregion
    }
}