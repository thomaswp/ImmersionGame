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
        private bool isCollected;
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
    }
}
