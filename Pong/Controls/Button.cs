using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Pong.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState _curMouseState;
        private MouseState _prevMouseState;
        private SpriteFont _font;
        private bool _hover;
        private Texture2D _tex;

        #endregion

        #region Properties
        public EventHandler Click { get; set; }

        public bool Clicked { get; private set; }

        public float Layer { get; set; }

        public string Text { get; set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Origin 
        { 
            get
            {
                return new Vector2(_tex.Width / 2, _tex.Height / 2);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _tex.Width, _tex.Height);
            }
        }

        #endregion

        #region Methods

        public Button (Texture2D tex, SpriteFont font)
        {
            _tex = tex;
            _font = font;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (_hover)
            {
                color = Color.Green;
            }

            spriteBatch.Draw(_tex, Position, null, color, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 4);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0,0), 0.5f, SpriteEffects.None, Layer + 0.01f);
            }
        }
        public override void Update(GameTime gameTime)
        {
            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();

            var mouseRec = new Rectangle(_curMouseState.X, _curMouseState.Y, 1, 1);
            _hover = false;

            if (mouseRec.Intersects(Rectangle))
            {
                _hover = true;

                if (_curMouseState.LeftButton == ButtonState.Released && _prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                    Clicked = true;
                } 
            }
        }

        #endregion
    }
}
