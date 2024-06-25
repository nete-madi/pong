using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Pong
{
    public class Game1 : Game
    {
        private SpriteBatch _ballSpriteBatch;
        private SpriteBatch _barSpriteBatch;

        private readonly GraphicsDeviceManager _graphics;

        Texture2D bar1Texture, bar2Texture;
        Rectangle bar1Bounds, bar2Bounds;
        public int _windowHeight;
        public int _windowWidth;

        Ball ball;
        Bar bar1;
        Bar bar2;

        // Tell the project how to start.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _windowHeight = _graphics.PreferredBackBufferHeight;
            _windowWidth = _graphics.PreferredBackBufferWidth;

        }

        // Initialize game on startup.
        // Called after constructor, but before main game loop.
        // You can query any required services and load any non-graphic related content.
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
            _ballSpriteBatch = new SpriteBatch(GraphicsDevice);
            _barSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Load your ball texture.
            ball.tex = Content.Load<Texture2D>("ball");
            bar1.tex = Content.Load<Texture2D>("bar1");
            bar2.tex = Content.Load<Texture2D>("bar2");

        }

        // Update game state -- this is called on a regular interval. Called multiple times per second.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveBarsKb(gameTime);

            ball.Update(ball, bar1Bounds, bar2Bounds, _graphics, gameTime);
            bar1.Update(bar1, _graphics);
            bar2.Update(bar2, _graphics);

            bar1Bounds = new Rectangle((int)bar1.pos.X, (int)bar1.pos.Y, bar1.tex.Width, bar1.tex.Height);
            bar2Bounds = new Rectangle((int)bar2.pos.X, (int)bar2.pos.Y, bar2.tex.Width, bar2.tex.Height);

            base.Update(gameTime);
        }

        // Called on a regular interval to draw game entities to the screen. Called multiple times per second.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _ballSpriteBatch.Begin();
            // This Draw method sets the center of the ball when the default "center" of a sprite is the top left corner.
            _ballSpriteBatch.Draw(ball.tex, ball.pos, null, Color.White, 0f, new Vector2(ball.tex.Width / 2, ball.tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _ballSpriteBatch.End();

            _barSpriteBatch.Begin();
            _barSpriteBatch.Draw(bar1.tex, bar1.pos, null, Color.White, 0f, new Vector2(bar1.tex.Width / 2, bar1.tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _barSpriteBatch.Draw(bar2.tex, bar2.pos, null, Color.White, 0f, new Vector2(bar2.tex.Width / 2, bar2.tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _barSpriteBatch.End();


            base.Draw(gameTime);
        }

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
    }
}