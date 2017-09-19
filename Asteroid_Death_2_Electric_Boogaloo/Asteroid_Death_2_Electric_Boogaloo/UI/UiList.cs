using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.UI
{
    public class UiList : BaseUiComponent
    {

        private List<UiLabel> _labels;
        private int _spaceBetweenLabels;
        private readonly SpriteFont _font;

        public UiList(AsteroidsGame game, Vector2 position, SpriteFont font, string[] list, int spaceBetweenText) : base(game, position)
        {
            _font = font;
            _spaceBetweenLabels = spaceBetweenText;
            if (list != null)
                UpdateList(list);
        }

        public void UpdateList(string[] list)
        {
            _labels = new List<UiLabel>();

            for (int i = 0; i < list.Length; i++)
            {
                _labels.Add(new UiLabel(Game, Position + new Vector2(0, _spaceBetweenLabels * i) - Globals.HalfScreenSize, list[i], _font));
            }
        }

        public override void Update()
        {
            foreach (var label in _labels)
            {
                label.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var label in _labels)
            {
                label.Draw(spriteBatch);
            }
        }
    }
}
