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
        private ItemSprite item;
        float degree = 0;
        float timeMult = 30;

        public Vector2 Velocity;

        public float Size
        {
            get { return myTexture.Width / 2 * myScale; }
        }

        public PlatformSprite(Texture2D texture, PlatformData data)//, float timeMult)
            : base(texture, data.StartPos)
        {
            this.data = data;
            this.timeMult = 30;// timeMult;
            myScale = 0.5f;
            myColor.A = 100;
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            float timeMult = Keyboard.GetState().IsKeyDown(Keys.OemPlus) ? 100 : 30;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) timeMult /= 2;
            degree = (degree + elapsedTime * timeMult) % 360;
            myPosition = data.GetPosition(degree) * MapData.DISTANCE_MULTIPLIER;
            Velocity = data.getVelocity(degree) * MapData.DISTANCE_MULTIPLIER * timeMult;
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

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            base.Draw(batch, offset);
            if (item != null)
            {
                item.Draw(batch, myPosition + offset);
            }
        }
        public void LoadItemTextures(ContentManager content, Texture2D shadow)
        {
            if (data.item != null)
            {
                item = new ItemSprite(content.Load<Texture2D>(data.item.ImageName),shadow,data.itemOffset);
            }
        }
            
    }
}
