﻿using System;
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
    public class WordSprite : IPathedSprite
    {
        protected WordData data;
        protected SpriteFont font;
        protected Vector2 position;
        protected bool storyText;
        float degree = 0;

        public WordSprite(WordData data, SpriteFont font, bool storyText)
        {
            this.data = data;
            this.font = font;
            this.storyText = storyText;
        }

        public void Update(float elapsedTime)
        {
            float timeMult = Keyboard.GetState().IsKeyDown(Keys.OemPlus) ? 100 : 30;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) timeMult /= 2;
            float posMult = 3;
            degree = (degree + elapsedTime * timeMult) % 360;
            position = data.GetPosition(degree) * posMult;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 offset)
        {
            Vector2 size = font.MeasureString(data.Text);
            spritebatch.DrawString(font, data.Text, position + offset - size / 2, storyText ? Color.White : Color.LightBlue);
        }

        public void SetDegree(float degree)
        {
        }
    }
}
