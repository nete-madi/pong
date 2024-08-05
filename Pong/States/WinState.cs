using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Controls;
using System;
using System.Collections.Generic;

namespace Pong.States
{
    public class WinState : State
    {
        //private Texture2D menuBg;
        private List<Component> _components;
        private SpriteFont buttonFont;
        public WinState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            content.RootDirectory = "Content";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(menuBg, Vector2.Zero, Color.White);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(buttonFont, "YOU WIN", new Vector2(325, 50), Color.White);
            spriteBatch.DrawString(buttonFont, "Congratulations player 1!", new Vector2(100, 150), Color.White);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            var button = _content.Load<Texture2D>("assets/button");
            buttonFont = _content.Load<SpriteFont>("fonts/Score");

            _components = new List<Component>()
            {
                new Button(button, buttonFont)
                {
                    Text = "PLAY AGAIN",
                    Position = new Vector2(Game1.ScreenWidth / 3, 400),
                    Click = new EventHandler(Button_1Player_Clicked),
                    Layer = 0.1f
                }
            };
        }
        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        private void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
    }
}
