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
    public abstract class Overlay
    {
        protected SpriteBatch spriteBatch;
        protected Point resolution;

        public Overlay(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.resolution = new Point(graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height);
            LoadContent(content);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract bool IsFinished();

        protected virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void UpdateGame(ImmersionGame game)
        {

        }
    }
}
