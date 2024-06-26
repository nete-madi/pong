using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Pong.Sprites;
using Pong.States;

namespace Pong
{
    public class Game1 : Game
    {
        #region Variables

        private State _currentState;
        private State _nextState;

        SpriteBatch spriteBatch;

        #endregion

        #region Constructor

        // Tell the project how to start.
        public Game1()
        {
            _currentState = new GameState(this, Content);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

                _currentState.Update(gameTime);
                // _currentState.PostUpdate(gameTime);
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