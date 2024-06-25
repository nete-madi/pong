using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Pong
{
    internal class Bar
    {
        #region Variables

        public Vector2 pos;
        public static float speed = 150f;
        public Texture2D tex;

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
            // draw the bar with the spritebatch: https://github.com/HadiCya/Pong-MonoGame/blob/master/Ball.cs
        }

        #endregion
    }
}
