using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Controls;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

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
            float offsetXOver = (Game1.ScreenWidth / 2) - (buttonFont.MeasureString("GAME OVER").X / 2);
            float offsetXCongrats = (Game1.ScreenWidth / 2) - (buttonFont.MeasureString("Congratulations player 1!").X / 3);
            spriteBatch.DrawString(buttonFont, "GAME OVER", new Vector2(offsetXOver, buttonFont.MeasureString("GAME OVER").Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(buttonFont, "Congratulations player 1!", new Vector2(offsetXCongrats, buttonFont.MeasureString("Congratulations player 1!").Y + 150), Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
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
                    Position = new Vector2(Game1.ScreenWidth / 2, 400),
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
