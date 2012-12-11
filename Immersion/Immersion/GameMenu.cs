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
        AnimatedHero heroWithItems;
        Texture2D black;
        bool escUp;
        bool finished;
        bool finishing;
        int alpha = 0;
        float milliseconds;
        private SpriteFont font;
        private SoundEffect beep;
        Dictionary<Texture, String> menuItems = new Dictionary<Texture, string>();

        public GameMenu(GraphicsDevice graphicsDevice, ContentManager content, GameState gameState) : base(graphicsDevice, content, gameState) { }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            black = content.Load<Texture2D>("black");
            font = content.Load<SpriteFont>("MenuFont");
            beep = content.Load<SoundEffect>("menu_select");
            beep.Play();

            if (gameState.myAnimatedHero.Items != null)
            {
                foreach (ItemData collected in gameState.myAnimatedHero.Items)
                {
                    menuItems.Add(content.Load<Texture2D>(collected.ImageName), collected.ImageName);
                }
            }
        }

        public override void UpdateGame(ImmersionGame game)
        {
            game.WorldScale = ImmersionGame.Lerp(game.WorldScale, 0.3f, 0.7f);
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public override void Update(GameTime gameTime)
        {
            milliseconds += gameTime.ElapsedGameTime.Milliseconds;

            bool down = Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                GamePad.GetState(0, GamePadDeadZone.None).IsButtonDown(Buttons.Start);

            if (!escUp && !down)
            {
                escUp = true;
            }
            else if (!finishing && escUp && down)
            {
                beep.Play();
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
            String text = "Game Paused...";
            Vector2 textSize = font.MeasureString(text);
            float x = (resolution.X - textSize.X) / 2;
            float y = (float)Math.Sin(milliseconds / 300) * 20 + (resolution.Y - textSize.Y) / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.PowderBlue);

            String itemText = "Items";

            float yy = 100f;
            float xx = font.MeasureString(itemText).X + 20;
            spriteBatch.DrawString(font, itemText, new Vector2(0, 100), Color.PowderBlue);
            foreach (Texture2D key in menuItems.Keys)
            {
                float scale = 0.35f;
                xx += key.Width * scale;
                spriteBatch.Draw(key, new Vector2(xx, yy), null, Color.White, 0f, new Vector2(key.Width / 2,
                    key.Height / 2), scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}
