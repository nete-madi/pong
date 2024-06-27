using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Sprites;

namespace Pong.States
{
    public class GameState : State
    {
        private readonly Ball ball;
        private readonly Bar bar1;
        private readonly Bar bar2;

        private Rectangle bar1Bounds, bar2Bounds;
        private SoundEffect ping;
        private SpriteFont font;

        int leftScore = 0, rightScore = 0;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            ball = new Ball(new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2), 200f, null);
            bar1 = new Bar(new Vector2(50, Game1.ScreenWidth / 2), 150f, null);
            bar2 = new Bar(new Vector2(Game1.ScreenWidth - 50, Game1.ScreenHeight / 2), 150f, null);

            content.RootDirectory = "Content";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ball.Draw(spriteBatch);
            bar1.Draw(spriteBatch);
            bar2.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, leftScore.ToString(), new Vector2(100, 50), Color.White);
            spriteBatch.DrawString(font, rightScore.ToString(), new Vector2(Game1.ScreenWidth - 112, 50), Color.White);
            spriteBatch.End();
        }
        public override void LoadContent()
        {
            ball.tex = _content.Load<Texture2D>("assets/ball");
            bar1.tex = _content.Load<Texture2D>("assets/bar1");
            bar2.tex = _content.Load<Texture2D>("assets/bar2");
            font = _content.Load<SpriteFont>("fonts/Score");
            ping = _content.Load<SoundEffect>("sounds/pixel-bounce");
        }
        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            MoveBarsKb(gameTime);

            bar1Bounds = new Rectangle((int)bar1.pos.X, (int)bar1.pos.Y, bar1.tex.Width, bar1.tex.Height);
            bar2Bounds = new Rectangle((int)bar2.pos.X, (int)bar2.pos.Y, bar2.tex.Width, bar2.tex.Height);

            ball.Update(ball, bar1Bounds, bar2Bounds, graphics, gameTime, ping, ref leftScore, ref rightScore);
            bar1.Update(bar1, graphics);
            bar2.Update(bar2, graphics);
        }

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

        //private void MoveBarsGamepad(GameTime gameTime)
        //{
        //    // TODO: add gamepad support
        //}

        #endregion
    }
}
