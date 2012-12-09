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
        protected float degree = 0;
        protected float timeMult = 30;
        protected float sumTime;
        protected bool heroOnPlatform;
        protected float heroOnPlatformMs = 0;
        protected float heroLastOnPlatformMs = 0;
        protected byte baseOpacity = 200;
        protected Color baseColor;

        public PlatformData data;
        public Vector2 Velocity;
        public Vector2 LastFrameMovement;
        public ItemSprite Item { get; set; }
        public bool Solid { get { return data.FallTime <= 0 || heroOnPlatformMs < data.FallTime; } }
        public bool Safe { get { return data.SafePlatform; } }
        public bool RespawnPlatform { get; set; }
        public bool ForceJump { get; set; }

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
            baseColor = new Color(255, 255, 255, baseOpacity);
        }

        public void UpdateHeroOnPlatform(bool onPlatform, float elapsedTime)
        {
            this.heroOnPlatform = onPlatform;
            float ms = elapsedTime * 1000;
            if (onPlatform)
            {
                heroOnPlatformMs += ms;
                heroOnPlatformMs = Math.Min(heroOnPlatformMs, data.FallTime);
                heroLastOnPlatformMs = 1000;
            }
            else
            {
                if (heroLastOnPlatformMs > 0)
                {
                    heroLastOnPlatformMs -= ms;
                }
                else
                {
                    heroOnPlatformMs = Math.Max(0, heroOnPlatformMs - data.FallTime * ms / 1000);
                }
            }
        }

        public override void Update(float elapsedTime)
        {
            Vector2 lastPos = myPosition;

            base.Update(elapsedTime);
            float timeMult = Keyboard.GetState().IsKeyDown(Keys.OemPlus) ? 100 : 30;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) timeMult /= 2;

            float dd = elapsedTime * timeMult;
            Console.WriteLine(heroOnPlatformMs);
            ForceJump = data.Jump(degree, dd);
            if (!data.Wait(degree, dd) || heroOnPlatform)
            {
                degree = (degree + dd) % 360;
            }
            else
            {
                if ((degree + data.DegreeOffset) % 360 + dd >= 360) degree = data.DegreeOffset;
            }
            myPosition = data.GetPosition(degree) * MapData.DISTANCE_MULTIPLIER;

            LastFrameMovement = myPosition - lastPos;
            Velocity = LastFrameMovement / elapsedTime;

            if (Item != null)
            {
                Item.myPosition = myPosition + getPointOffset(data.ItemOffset);
                if (Item.IsCollected)
                {
                    sumTime = .01f;
                    float currentTime = 0;

                    while (sumTime != 0 && sumTime < 30)
                    {
                        sumTime += 1f;
                        Item.myScale += .5f * elapsedTime;
                        Item.myAngularVelocity += .5f * elapsedTime;
                        currentTime = 31;
                    }
                    if (currentTime == 31)
                    {
                        while (currentTime != 0)
                        {
                            currentTime -= 1f;
                            Item.myScale -= .5f * elapsedTime;
                            Item.myAngularVelocity += .5f * elapsedTime;
                            if (Item.myScale < 0.001f)
                            {
                                Item.myScale = 0f;
                            }
                        }
                    }
                    else { }
                }
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

        public Vector2 NearEdge(Vector2 pos)
        {
            int checks = 4;
            int rad = 8;
            for (int i = 0; i < checks; i++)
            {
                double degree = Math.PI / 4 + i * Math.PI * 2 / checks;
                Vector2 offset = new Vector2((float)Math.Cos(degree), (float)Math.Sin(degree)) * rad;
                Vector2 checkPos = pos + offset;
                if (!Contains(checkPos)) return offset;
            }
            return Vector2.Zero;
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

        public bool OnMapTransition(Vector2 pos)
        {
            return data.NextMap != null &&  SegmentContains(pos);
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (data.Invisible) return;

            myColor = baseColor;

            if (data.FallTime > 0)
            {
                myColor.A = (byte)Math.Max(0, baseOpacity * (data.FallTime - heroOnPlatformMs) / data.FallTime);
            }

            if (RespawnPlatform)
            {
                myColor.R -= 50;
                myColor.B -= 50;
            }

            if (data.Launch)
            {
                myColor.G -= 100;
                myColor.B -= 150;
            }

            myColor.R -= (byte)(100 * data.Slide);
            myColor.G -= (byte)(100 * data.Slide);

            foreach (Point p in data.Segments)
            {
                if (p.X == 0 && p.Y == 0 && data.NextMap != null)
                {
                    double highlight = Math.Sin(degree / 180 * Math.PI * 10) / 2 + 0.5;
                    int plus = (int)(125 * highlight);
                    myColor.R = (byte)Math.Min(myColor.R + plus, 255);
                    myColor.G = (byte)Math.Min(myColor.G + plus, 255);
                    myColor.B = (byte)Math.Min(myColor.B - plus, 255);
                }

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
            if (data.Item != null)
            {
                Item = new ItemSprite(content.Load<Texture2D>(data.Item.ImageName), shadow, getPointOffset(data.ItemOffset));
            }
        }
            
        public void SetDegree(float degree)
        {
            this.degree = degree;
        }
    }
}
