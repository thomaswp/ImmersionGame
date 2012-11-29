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
        protected float myVelocityZ = 0f;
        protected Texture2D shadowImage;
        protected PlatformSprite currentPlatform;

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
                myVelocityZ = 1;
            }
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float shadowScale = myScale * .75f; //0.35f;

            if (!IsCollected)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, 100), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);

                base.Draw(batch, offset);
            }
        }
                //batch.Draw(shadowImage, myPosition + offset, null, Color.White, 0f, new Vector2(myTexture.Width / 2, myTexture.Height / 2),
                //itemScale, SpriteEffects.None, 0f);
           
            

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            if (!IsCollected)
            {
               // Bobble();
            }
        }
    }
}
