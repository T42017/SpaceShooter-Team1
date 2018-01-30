namespace Game1.AI
{
    public abstract class BaseAi
    {
        #region Protected fields
        protected readonly AsteroidsGame _game;
        #endregion

        #region Protected constructors
        protected BaseAi(AsteroidsGame game)
        {
            _game = game;
        }
        #endregion

        #region Public abstract methods
        public abstract void Update();
        #endregion
    }
}