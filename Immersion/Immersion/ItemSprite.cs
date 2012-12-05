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
using System.Collections;
namespace Immersion
{
    public class ItemSprite : Sprite
    {
        public bool IsCollected { get; set; }
        protected float myPositionZ = 0f;
        protected Texture2D shadowImage;
        protected PlatformSprite currentPlatform;
        protected float time;
        protected float sumTime;

        public ItemSprite(Texture2D image, Texture2D shadow, Vector2 position)
            : base(image, position)
        {
            this.shadowImage = shadow;
            this.myScale = 0.25f;
        }

        public void Bobble()
        {
            if (!IsCollected)
            {
                //myPositionZ = (1 + (float)Math.Sin(time * 2)) * 30;
            }
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float shadowScale = myScale * .65f; //0.35f;

            // If collected draw the shadow and the item
            if (!IsCollected)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, 100), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);

                Vector2 jumpingPos = myPosition + offset;
                jumpingPos.Y -= myPositionZ;
                batch.Draw(myTexture, jumpingPos, null,
                Color.White, myAngularVelocity, Vector2.Zero, myScale, SpriteEffects.None, 0f);
            }
            //else Draw collected item animation
            else
            {
                base.Draw(batch, offset);
            }
        }
           
            

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            time += elapsedTime;
            if (IsCollected)
            {
                sumTime = .01f;
                float currentTime = 0;

                while (sumTime != 0 && sumTime < 30)
                {
                    sumTime += 1f;
                    myScale += .5f * elapsedTime;
                    myAngularVelocity += .5f * elapsedTime;
                    currentTime = 31;
                }
                if (currentTime == 31)
                {
                    while (currentTime != 0)
                    {
                        currentTime -= 1f;
                        myScale -= .5f * elapsedTime;
                        myAngularVelocity += .5f * elapsedTime;
                        if (myScale < 0.001f)
                        {
                            myScale = 0f;
                        }
                    }
                }
                else { }
            }
            if (!IsCollected)
            {
               Bobble();
            }
        }
    }
}
