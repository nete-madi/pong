using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    public abstract class State
    {
        protected Game1 _game;
        protected ContentManager _content;
        protected readonly GraphicsDevice _graphicsDevice;

        public State (Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _content = content;
            _graphicsDevice = graphicsDevice;
        }
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
