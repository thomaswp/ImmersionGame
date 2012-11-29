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
    public class PlatformSprite : Sprite, IPathedSprite
    {
        protected PlatformData data;
        public ItemSprite Item {get; set;}
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

            if (Item != null)
            {
                Item.myPosition = myPosition + data.itemOffset;
            }
        }

        public bool Contains(Vector2 pos)
        {
            foreach (Point p in data.Segments)
            {
                if (SegmentContains(pos - getPointOffset(p)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool SegmentContains(Vector2 pos)
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
            foreach (Point p in data.Segments)
            {
                base.Draw(batch, offset + getPointOffset(p));
            }
            if (Item != null)
            {
                Item.Draw(batch, offset);
            }
        }

        private Vector2 getPointOffset(Point pos)
        {
            float rX = 140, rY = 90;
            return new Vector2((pos.Y - pos.X) * rX, (pos.X + pos.Y) * rY);
        }

        public void LoadItemTextures(ContentManager content, Texture2D shadow)
        {
            if (data.item != null)
            {
                Item = new ItemSprite(content.Load<Texture2D>(data.item.ImageName), shadow, data.itemOffset);
            }
        }
            
        public void SetDegree(float degree)
        {
            this.degree = degree;
        }
    }
}
