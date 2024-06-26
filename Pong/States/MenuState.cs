using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Controls;
using System;
using System.Collections.Generic;

namespace Pong.States
{
    public class MenuState : State
    {
        private Texture2D menuBg;
        private List<Component> _components;
        public MenuState (Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBg, Vector2.Zero, Color.White);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch); 
            }
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            var button = _content.Load<Texture2D>("assets/button");
            var buttonFont = _content.Load<SpriteFont>("fonts/buttonfont");
            menuBg = _content.Load<Texture2D>("assets/menu");

            _components = new List<Component>()
            {
                new Button(button, buttonFont)
                {
                    Text = "Start New Game",
                    Position = new Vector2(Game1.ScreenWidth / 2, 400),
                    Click = new EventHandler(Button_1Player_Clicked),
                    Layer = 0.1f
                },

                new Button(button, buttonFont)
                {
                    Text = "Exit",
                    Position = new Vector2(Game1.ScreenWidth / 2, 400),
                    Click = new EventHandler(Button_Quit_Clicked),
                    Layer = 0.1f
                },

            };

        }
         
        public override void Update(GameTime gameTime)
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

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }
    }
}
