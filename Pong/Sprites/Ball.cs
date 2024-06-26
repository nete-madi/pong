﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Pong.Sprites
{
    internal class Ball
    {
        #region Variables

        public Vector2 pos;
        public float speed;
        public Rectangle ballBounds;
        public Texture2D tex;

        private int right = 1, top = 1;

        #endregion

        #region Constructor

        public Ball(Vector2 position, float moveSpeed, Texture2D texture)
        {
            pos = position;
            speed = moveSpeed;
            tex = texture;
        }

        #endregion

        #region Public Methods
        public void Update(Ball ball, Rectangle bar1, Rectangle bar2, GraphicsDeviceManager _graphics, GameTime gameTime, SoundEffect sound, ref int leftScore, ref int rightScore)
        {
            if (ball.pos.X > _graphics.PreferredBackBufferWidth - tex.Width / 2)
            {
                // grant a point to bar1 and reset
                leftScore++;
                ResetGame(_graphics);
            }
            else if (ball.pos.X < tex.Width / 2)
            {
                // grant a point to bar2 and reset
                rightScore++;
                ResetGame(_graphics);
            }

            if (ball.pos.Y > _graphics.PreferredBackBufferHeight - tex.Height / 2)
            {
                ball.pos.Y = _graphics.PreferredBackBufferHeight - tex.Height / 2;
            }
            else if (ball.pos.Y < tex.Height / 2)
            {
                ball.pos.Y = tex.Height / 2;
            }

            CheckIntersect(ball, bar1, bar2, _graphics, gameTime, sound);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos, null, Color.White, 0f, new Vector2(tex.Width / 2, tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        #endregion

        #region Private Methods

        private void CheckIntersect(Ball ball, Rectangle bar1, Rectangle bar2, GraphicsDeviceManager _graphics, GameTime gameTime, SoundEffect sound)
        {
            // ballBounds is going to be a square for now. but eventually you should be able to write your own circle class and compute collision that way.
            ballBounds = new Rectangle((int)ball.pos.X, (int)ball.pos.Y, tex.Width, tex.Height);

            int ballSpeedDelta = (int)(ball.speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            ball.pos.X += right * ballSpeedDelta;
            ball.pos.Y += top * ballSpeedDelta;
            ballBounds.X += right * ballSpeedDelta;
            ballBounds.Y += top * ballSpeedDelta;

            // Check if bar bounds intersect with the ball.
            if (ballBounds.Intersects(bar1))
            {
                right = 1;
                sound.CreateInstance().Play();
            }
            if (ballBounds.Intersects(bar2))
            {
                right = -1;
                sound.CreateInstance().Play();
            }
            if (ball.pos.Y < 23) // TODO: figure out why this number works...lol
            {
                top *= -1;
                sound.CreateInstance().Play();
            }
            if (ball.pos.Y > _graphics.PreferredBackBufferHeight - tex.Height)
            {
                top *= -1;
                sound.CreateInstance().Play();
            }
        }

        private void ResetGame(GraphicsDeviceManager _graphics)
        {
            // reset the game when someone scores a point
            pos.X = _graphics.PreferredBackBufferWidth / 2;
            pos.Y = _graphics.PreferredBackBufferHeight / 2;
        }

        #endregion

    }
}
