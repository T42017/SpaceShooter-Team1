using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo.Devices

{
    abstract class AstroidsComponent : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public AsteroidsGame AstroidGame { get; private set; }
        public GameState DrawableStates { get; protected set; }
        public GameState UpdatableStates { get; protected set; }

        protected List<BaseUiComponent> UiComponents = new List<BaseUiComponent>();
        protected int HighlightedUiComponent = 0;

        protected AstroidsComponent(Game game) : base(game)
        {
            AstroidGame = (AsteroidsGame)game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

        public virtual void ChangedState(GameState newState)
        {
            HighlightedUiComponent = 0;
            HighlightNextComponent();
        }

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
    }
}
