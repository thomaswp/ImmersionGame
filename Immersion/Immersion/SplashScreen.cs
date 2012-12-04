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
    public class SplashScreen : Overlay
    {
        private Texture2D splashScreen;
        private bool finishing;
        private float alpha = 255;
        private float milliseconds;
        private SpriteFont font;

        public SplashScreen(GraphicsDevice graphicsDevice, ContentManager content) : base(graphicsDevice, content) { }        

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            splashScreen = content.Load<Texture2D>("splash-screen");
            font = content.Load<SpriteFont>("MenuFont");
        }

        public override bool IsFinished()
        {
            return alpha <= 0;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                finishing = true;
            }
            if (finishing)
            {
                alpha -= gameTime.ElapsedGameTime.Milliseconds / 2;
            }

            milliseconds += gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            spriteBatch.Draw(splashScreen, new Rectangle(0, 0, resolution.X, resolution.Y), 
                new Color(255, 255, 255, (int)Math.Max(alpha, 0)));

            if (!finishing)
            {
                String text = "Press any key to play...";
                Vector2 textSize = font.MeasureString(text);
                float x = (resolution.X - textSize.X) / 2;
                float y = (float)Math.Sin(milliseconds / 300) * 20 + (resolution.Y - textSize.Y) / 2;
                spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);
            }

            spriteBatch.End();
        }
    }
}
