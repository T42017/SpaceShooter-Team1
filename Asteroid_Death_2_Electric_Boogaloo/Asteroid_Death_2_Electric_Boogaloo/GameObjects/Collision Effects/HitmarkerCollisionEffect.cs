using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.GameObjects
{
    public class HitmarkerCollisionEffect : BaseCollisionEffect
    {
        public HitmarkerCollisionEffect(AsteroidsGame game, Vector2 position) : base(game, position)
        {
            Texture = TextureManager.Instance.LoadByName(Game.Content, "hitmarker");
        }

        public override void LoadContent() {}
    }
}
