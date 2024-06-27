using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.States;

namespace Pong
{
    public class Game1 : Game
    {
        #region Variables

        private State _currentState;
        private State _nextState;
        private GraphicsDeviceManager _graphics;

        SpriteBatch spriteBatch;
        public static int ScreenWidth, ScreenHeight;

        #endregion

        #region Constructor

        // Tell the project how to start.
        public Game1()
        {
            Window.AllowUserResizing = false;
            IsMouseVisible = true;
            _graphics = new GraphicsDeviceManager(this);
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
        }

        #endregion

        #region Game Initialization

        // Initialize game on startup.
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load content. This is called once per game within the initialize method, before the main game loop (Update/Draw)
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
        }

        #endregion

        #region Main Game Loop

        // Update game state.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            {
                if (_nextState != null)
                {
                    _currentState = _nextState;
                    _currentState.LoadContent();

                    _nextState = null;
                }

                _currentState.Update(gameTime, _graphics);
            }

            base.Update(gameTime);
        }

        // Called on a regular interval to draw game entities to the screen. Called multiple times per second.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        #endregion
    }
}