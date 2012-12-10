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
        protected float myVelocityZ = 0.25f;
        protected Texture2D shadowImage;
        protected PlatformSprite currentPlatform;
        protected float time, collectedTime;

        public ItemSprite(Texture2D image, Texture2D shadow, Vector2 position)
            : base(image, position)
        {
            this.shadowImage = shadow;
            this.myScale = 0.25f;
        }

        public void Bobble()
        {
            myPositionZ = (1 + (float)Math.Sin(time * 2)) * 15;
        }

        public override void Update(float elapsedTime, GameState gameState)
        {
            base.Update(elapsedTime, gameState);
            time += elapsedTime;
            if (IsCollected)
            {
                collectedTime += elapsedTime;
                myAngularVelocity = 12;
                if (collectedTime < 0.3)
                {
                    myScale *= 1.07f;
                } 
                else
                {
                    myScale *= 0.9f;
                    if (myScale < 0.01f)
                    {
                        myScale = 0;
                    }
                }
            }
            if (!IsCollected)
            {
                Bobble();
            }
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float shadowScale = myScale * .65f; //0.35f;

            // If collected draw the shadow and the item
            //if (!IsCollected)
            //{
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, 100), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);

                base.Draw(batch, offset - new Vector2(0, myPositionZ + myTexture.Height * myScale / 2));

            //}
            //else Draw collected item animation
            //else
           // {
                //base.Draw(batch, offset);
            //}
        }
    }
}
