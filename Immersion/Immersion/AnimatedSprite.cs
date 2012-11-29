////all code by Shannon Duvall
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//namespace Immersion
//{
//    class AnimatedSprite : Sprite
//    {
//        // My state variables
//        protected Flipbook myBook;
//        protected Vector2 myScreenSize;
//        protected bool goingRight;

//        public AnimatedSprite(Texture2D[] images, Vector2 screenSize, Vector2 position)
//            : base(images[0], position)
//        {
//            myBook = new Flipbook(images);
//            setUpFlipbook();
//            myScreenSize = screenSize;
//        }

//        //public AnimatedSprite(Texture2D[] images, Vector2 screenSize, Vector2 position, Vector2 velocity,
//        //   float angle, float angleVelocity, float scale, Vector2 scaleVelocity)
//        //    : base(images[0], position, velocity, angle, angleVelocity, scale)
//        //{
//        //    myBook = new Flipbook(images);
//        //    setUpFlipbook();
//        //    myScreenSize = screenSize;
//        //    goingRight = velocity.Y > 0;
//        //}

//        public void setUpFlipbook()
//        {
//            myBook.AddFrame(0, .250);
//            myBook.AddFrame(1, .150);
//            myBook.AddFrame(0, .150);
//            myBook.AddFrame(1, .150);
//            myBook.AddFrame(2, .200);
//            myBook.AddFrame(1, .150);
//        }

//        public virtual void Update(float timeSinceLastUpdate)
//        {
//            //base.Update(timeSinceLastUpdate);
//            myBook.Update(timeSinceLastUpdate);
//            myVelocity = new Vector2
//        }

//        public override void Draw(SpriteBatch batch, Vector2 offset)
//        {
//            batch.Draw(myBook.GetImage(), myPosition + offset, null, myColor, 0f,
//                new Vector2(myTexture.Width / 2, myTexture.Height / 2), myScale, SpriteEffects.None, 0f);
//        }
//    }
//}
