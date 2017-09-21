﻿using Asteroid_Death_2_Electric_Boogaloo.Enums;
using Microsoft.Xna.Framework;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups
{
    public abstract class Powerup : GameObject
    {
        public PowerupType PowerupType { get; }
        public int Timer { get; set; }

        protected AsteroidsGame Game;
        
        protected Powerup(AsteroidsGame game, Vector2 position, PowerupType powerupType, int duration) : this(game, powerupType, duration)
        {
            Position = position;
        }

        protected Powerup(AsteroidsGame game, PowerupType powerupType, int duration) : base(game)
        {
            Game = game;
            PowerupType = powerupType;
            Texture = TextureManager.Instance.PowerUpTextures[(int)powerupType];
            Timer = duration;
        }

        public abstract void Remove(Player player);
        
        public abstract void DoEffect(Player player);

        public override void Update()
        {
            Timer--;
            base.Update();
        }
    }
}
