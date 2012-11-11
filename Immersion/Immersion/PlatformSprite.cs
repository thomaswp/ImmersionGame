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
    public class PlatformSprite : Sprite
    {
        protected PlatformData data;
        float degree = 0;

        public Vector2 Velocity;

        public float Size
        {
            get { return myTexture.Width / 2 * myScale; }
        }

        public PlatformSprite(Texture2D texture, PlatformData data) : base(texture, data.startPos)
        {
            this.data = data;
            myScale = 0.5f;
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            float timeMult = Keyboard.GetState().IsKeyDown(Keys.OemPlus) ? 100 : 30;
            float posMult = 3;
            degree = (degree + elapsedTime * timeMult) % 361;
            myPosition = data.getPosition(degree) * posMult;
            Velocity = data.getVelocity(degree) * posMult * timeMult;
        }

        public bool Contains(Vector2 pos)
        {
            Vector2 relPos = pos - myPosition;
            float dw = myTexture.Width / 2 * myScale, dh = myTexture.Height / 2 * myScale;
            float slope = (float)myTexture.Height / myTexture.Width;
            if (slope * (relPos.X - dw) > relPos.Y) return false;
            if (-slope * (relPos.X + dw) > relPos.Y) return false;
            if (slope * (relPos.X + dw) < relPos.Y) return false;
            if (-slope * (relPos.X - dw) < relPos.Y) return false;
            return true;
        }
    }
}
