using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Immersion
{
    public class GameMenu : Overlay
    {
        Texture2D black;
        bool escUp;
        bool finished;
        bool finishing;
        int alpha = 0;

        public GameMenu(GraphicsDevice graphicsDevice, ContentManager content) : base(graphicsDevice, content) { }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            black = content.Load<Texture2D>("black");
        }

        public override void UpdateGame(Game1 game)
        {
            game.WorldScale = .5f;
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public override void Update(GameTime gameTime)
        {
            if (!escUp && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                escUp = true;
            }
            else if (escUp && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                
                finishing = true;
            }

            if (finishing)
            {
                alpha -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (alpha < 100)
            {
                alpha += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (finishing && alpha <= 0)
            {
                finished = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            Color color = new Color(255, 255, 255, Math.Max((int)alpha, 0));
            spriteBatch.Draw(black, new Rectangle(0, 0, resolution.X, resolution.Y), color);

            spriteBatch.End();
        }
    }
}
