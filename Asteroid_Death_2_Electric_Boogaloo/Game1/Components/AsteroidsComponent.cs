using System.Collections.Generic;
using Game1.Enums;
using Game1.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Components
{
    public abstract class AsteroidsComponent : DrawableGameComponent
    {
        #region Protected fields
        protected List<BaseUiComponent> UiComponents = new List<BaseUiComponent>();
        protected int HighlightedUiComponent = 0;
        #endregion

        #region Public properties
        public SpriteBatch SpriteBatch { get; private set; }
        public AsteroidsGame AstroidGame { get; private set; }
        public GameState DrawableStates { get; protected set; }
        public GameState UpdatableStates { get; protected set; }
        #endregion

        #region Protected constructors
        protected AsteroidsComponent(Game game) : base(game)
        {
            AstroidGame = (AsteroidsGame)game;
        }
        #endregion

        #region Public methods
        public void UpdateHighlightMarker()
        {
            for (int i = 0; i < UiComponents.Count; i++)
            {
                UiComponents[i].IsHighlighted = i == HighlightedUiComponent;
            }
        }

        public void HighlightPreviusComponent()
        {
            for (var previusComponent = HighlightedUiComponent - 1; previusComponent > 0; previusComponent--)
            {
                if (UiComponents[previusComponent].CanBeHighLighted)
                {
                    HighlightedUiComponent = previusComponent;
                    return;
                }
            }
        }

        public void HighlightNextComponent()
        {
            for (var nextComponent = HighlightedUiComponent + 1; nextComponent < UiComponents.Count; nextComponent++)
            {
                if (UiComponents[nextComponent].CanBeHighLighted)
                {
                    HighlightedUiComponent = nextComponent;
                    return;
                }
            }
        }
        #endregion

        #region Public virtual methods
        public virtual void ChangedState(GameState newState)
        {
            HighlightedUiComponent = 0;
            HighlightNextComponent();
        } 
        #endregion

        #region Protected overrides
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
        #endregion

        #region Public overrides
        public override void Update(GameTime gameTime)
        {
            UpdateHighlightMarker();

            foreach (BaseUiComponent component in UiComponents)
                component.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            UpdateHighlightMarker();

            foreach (var component in UiComponents)
                component.Draw(SpriteBatch);

            base.Draw(gameTime);
        }
        #endregion
    }
}