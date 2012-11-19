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
    class Item : Sprite
    {
        private bool isCollected = false;
        protected float myPositionZ;
        protected float myVelocityZ;
        protected Texture2D shadowImage;
        protected PlatformSprite currentPlatform;

        public Item(Texture2D image, Texture2D shadow, Vector2 position)
            : base(image, position)
        {
            this.shadowImage = shadow;
        }

        public void Bobble()
        {

        }

        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            float itemScale = 0.4f * myScale;
            float shadowScale = itemScale * 0.35f;

            if (!isCollected)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, 100), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);
            }

            batch.Draw(myTexture, new Vector2(0, 0), null, Color.White, 0f, new Vector2(myTexture.Width / 2, myTexture.Height / 2),
                itemScale, SpriteEffects.None, 0f);
            }
    }
}
