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
    public class MapTransition : Overlay
    {
        MapData map;
        Texture2D black;
        bool finished;
        bool finishing;
        float alpha;

        public MapTransition(GraphicsDevice graphicsDevice, ContentManager content, MapData map)
            : base(graphicsDevice, content)
        {
            this.map = map;
        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            black = content.Load<Texture2D>("black");
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public override void UpdateGame(Game1 game)
        {
            base.UpdateGame(game);
            if (!finishing && alpha >= 300)
            {
                finishing = true;
                game.LoadMap(map);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (finishing)
            {
                alpha -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else
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
            Color color = new Color(255, 255, 255, Math.Min(Math.Max((int)alpha, 0), 255));
            spriteBatch.Draw(black, new Rectangle(0, 0, resolution.X, resolution.Y), color);
            spriteBatch.End();
        }
    }
}
