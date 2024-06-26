using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites
{
    internal class Bar
    {
        #region Variables

        public Vector2 pos;
        public static float speed = 150f;
        public Texture2D tex;
        public static SpriteBatch barSpriteBatch;

        #endregion

        #region Constructor

        public Bar(Vector2 position, float moveSpeed, Texture2D texture)
        {
            pos = position;
            speed = moveSpeed;
            tex = texture;
        }

        #endregion

        #region Public Methods
        public void Update(Bar bar, GraphicsDeviceManager _graphics)
        {

            if (bar.pos.Y > _graphics.PreferredBackBufferHeight - tex.Height / 2)
            {
                bar.pos.Y = _graphics.PreferredBackBufferHeight - tex.Height / 2;
            }
            else if (bar.pos.Y < tex.Height / 2)
            {
                bar.pos.Y = tex.Height / 2;
            }
        }

        public void Draw()
        {
            barSpriteBatch.Begin();
            barSpriteBatch.Draw(tex, pos, null, Color.White, 0f, new Vector2(tex.Width / 2, tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            barSpriteBatch.Draw(tex, pos, null, Color.White, 0f, new Vector2(tex.Width / 2, tex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            barSpriteBatch.End();
        }

        #endregion
    }
}
