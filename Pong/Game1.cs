using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class Game1 : Game
    {
        #region Variables

        private readonly GraphicsDeviceManager _graphics;

        public int _windowHeight, _windowWidth;
        public int leftScore = 0, rightScore = 0;

        Rectangle bar1Bounds, bar2Bounds;
        Ball ball;
        Bar bar1;
        Bar bar2;
        SoundEffect ping;
        SpriteFont font;
        SpriteBatch spriteBatch;

        #endregion

        #region Constructor

        // Tell the project how to start.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _windowHeight = _graphics.PreferredBackBufferHeight;
            _windowWidth = _graphics.PreferredBackBufferWidth;

        }

        #endregion

        #region Game Initialization

        // Initialize game on startup.
        protected override void Initialize()
        {
            ball = new Ball(new Vector2(_windowWidth / 2, _windowHeight / 2), 200f, null);
            bar1 = new Bar(new Vector2(50, _windowHeight / 2), 150f, null);
            bar2 = new Bar(new Vector2(_windowWidth - 50, _windowHeight / 2), 150f, null);

            base.Initialize();
        }


        // Load content. This is called once per game within the initialize method, before the main game loop (Update/Draw)
        protected override void LoadContent()
        {
            ball.ballSpriteBatch = new SpriteBatch(GraphicsDevice);
            Bar.barSpriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball.tex = Content.Load<Texture2D>("assets/ball");
            bar1.tex = Content.Load<Texture2D>("assets/bar1");
            bar2.tex = Content.Load<Texture2D>("assets/bar2");
            font = Content.Load<SpriteFont>("fonts/Score");

            ping = Content.Load<SoundEffect>("sounds/pixel-bounce");

        }

        #endregion

        #region Main Game Loop

        // Update game state.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveBarsKb(gameTime);

            bar1Bounds = new Rectangle((int)bar1.pos.X, (int)bar1.pos.Y, bar1.tex.Width, bar1.tex.Height);
            bar2Bounds = new Rectangle((int)bar2.pos.X, (int)bar2.pos.Y, bar2.tex.Width, bar2.tex.Height);

            ball.Update(ball, bar1Bounds, bar2Bounds, _graphics, gameTime, ping, ref leftScore, ref rightScore);
            bar1.Update(bar1, _graphics);
            bar2.Update(bar2, _graphics);

            base.Update(gameTime);
        }

        // Called on a regular interval to draw game entities to the screen. Called multiple times per second.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ball.Draw();
            bar1.Draw();
            bar2.Draw();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, leftScore.ToString(), new Vector2(100, 50), Color.White);
            spriteBatch.DrawString(font, rightScore.ToString(), new Vector2(_graphics.PreferredBackBufferWidth - 112, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Private Methods

        private void MoveBarsKb(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();

            // Multiply the speed by the amount of time that's passed since the last call to Update().
            // The result is that the bar appears to move at the same speed regardless of the game's frame rate.
            // If you don't multiply the speed, then the bar moves at your game's frame rate. It shoots off the screen!
            if (kstate.IsKeyDown(Keys.W))
            {
                bar1.pos.Y -= Bar.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                bar1.pos.Y += Bar.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Up))
            {
                bar2.pos.Y -= Bar.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                bar2.pos.Y += Bar.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void MoveBarsGamepad(GameTime gameTime)
        {
            // TODO: add gamepad support
        }

        #endregion
    }
}