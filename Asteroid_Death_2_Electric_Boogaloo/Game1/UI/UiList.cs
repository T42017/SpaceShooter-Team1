using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class UiList : BaseUiComponent
    {
        #region Private fields
        private int _spaceBetweenLabels;
        private List<UiLabel> _labels;
        private readonly SpriteFont _font;
        #endregion

        #region Public constructors
        public UiList(AsteroidsGame game, Vector2 position, SpriteFont font, string[] list, int spaceBetweenText) : base(game, position)
        {
            _font = font;
            _spaceBetweenLabels = spaceBetweenText;
            if (list != null)
                UpdateList(list);
        }
        #endregion

        #region Public methods
        public void UpdateList(string[] list)
        {
            _labels = new List<UiLabel>();
            for (int i = 0; i < list.Length; i++)
                _labels.Add(new UiLabel(Game, Position + new Vector2(0, _spaceBetweenLabels * i) - Globals.HalfScreenSize, list[i], _font));
        }
        #endregion

        #region Public overrides
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
        #endregion
    }
}