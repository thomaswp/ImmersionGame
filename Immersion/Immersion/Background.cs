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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Immersion
{
    public class Background
    {
        int rows, cols;
        Texture2D texture;

        public Background(Texture2D texture, int width, int height)
        {
            cols = width / texture.Width + 2;
            rows = height / texture.Height + 2;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 offset, float scale)
        {
            offset *= 0.7f;
            while (offset.X < -texture.Width) offset.X += texture.Width;
            while (offset.X > 0) offset.X -= texture.Width;
            while (offset.Y < -texture.Height) offset.Y += texture.Height;
            while (offset.Y > 0) offset.Y -= texture.Height;

            for (int i = 0; i < cols / scale; i++)
            {
                for (int j = 0; j < rows / scale; j++)
                {
                    spritebatch.Draw(texture, offset + new Vector2(texture.Width * i, 
                        texture.Height * j), Color.White);
                }
            }

        }
    }
}
