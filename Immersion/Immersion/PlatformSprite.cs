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
        protected float degree = 0;
        protected float timeMult = 30;
        protected float heroOnPlatform = 0;
        protected float heroLastOnPlatform = 0;
        protected byte baseOpacity = 200;

        public Vector2 Velocity;
        public Vector2 LastFrameMovement;
        public ItemSprite Item { get; set; }
        public bool Solid { get { return data.FallTime <= 0 || heroOnPlatform < data.FallTime; } }
        public bool Safe { get { return data.SafePlatform; } }
        public bool RespawnPlatform { set { byte color = (byte)(value ? 200 : 255); myColor.R = color; myColor.B = color; } }

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
            myColor.A = baseOpacity;
        }

        public void UpdateHeroOnPlatform(bool onPlatform, float elapsedTime)
        {
            float ms = elapsedTime * 1000;
            if (onPlatform)
            {
                heroOnPlatform += ms;
                heroOnPlatform = Math.Min(heroOnPlatform, data.FallTime);
                heroLastOnPlatform = 1000;
            }
            else
            {
                if (heroLastOnPlatform > 0)
                {
                    heroLastOnPlatform -= ms;
                }
                else
                {
                    heroOnPlatform = Math.Max(0, heroOnPlatform - data.FallTime * ms / 1000);
                }
            }
        }

        public override void Update(float elapsedTime)
        {
            Vector2 lastPos = myPosition;

            base.Update(elapsedTime);
            float timeMult = Keyboard.GetState().IsKeyDown(Keys.OemPlus) ? 100 : 30;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) timeMult /= 2;
            degree = (degree + elapsedTime * timeMult) % 360;
            myPosition = data.GetPosition(degree) * MapData.DISTANCE_MULTIPLIER;

            LastFrameMovement = myPosition - lastPos;
            Velocity = LastFrameMovement / elapsedTime;

            if (Item != null)
            {
                Item.myPosition = myPosition + data.itemOffset;
            }
        }

        public bool Contains(Vector2 pos)
        {
            if (data.Invisible) return false;
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
            if (data.Invisible) return;
            if (data.FallTime > 0)
            {
                myColor.A = (byte)Math.Max(0, baseOpacity * (data.FallTime - heroOnPlatform) / data.FallTime);
            }
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
