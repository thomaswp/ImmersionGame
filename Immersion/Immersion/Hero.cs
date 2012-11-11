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
using System.Diagnostics;

namespace Immersion
{
    class Hero : Sprite
    {

        protected float myPositionZ;
        protected float myVelocityZ;
        protected Texture2D shadowImage;
        protected PlatformSprite currentPlatform;
        protected Vector2 pushoffVelocity;
        protected PlatformSprite lastPlatform;
        protected bool falling;

        private bool moved;

        public bool IsGrounded
        {
            get { return myPositionZ == 0; }
        }

        public Hero(Texture2D image, Texture2D shadow, Vector2 position)
            : base(image, position)
        {
            shadowImage = shadow;
            SetUpInput();
        }

        public void SetUpInput()
        {
            //Set up game actions for isMovingLeft/Forward/Right/Jumping

            object[] forwardDir = { new Vector2(0, -1) };
            GameAction isMovingForward = new GameAction(this, this.GetType().GetMethod("Move"), forwardDir);

            object[] rightDir = { new Vector2(1, 0) };
            GameAction isMovingRight = new GameAction(this, this.GetType().GetMethod("Move"), rightDir);

            object[] leftDir = { new Vector2(-1, 0) };
            GameAction isMovingLeft = new GameAction(this, this.GetType().GetMethod("Move"), leftDir);

            object[] downDir = { new Vector2(0, 1) };
            GameAction isMovingDown = new GameAction(this, this.GetType().GetMethod("Move"), downDir);

            GameAction isJumping = new GameAction(this, this.GetType().GetMethod("Jump"), new Object[0]);

            InputManager.AddToKeyboardMap(Keys.W, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.S, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.A, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.D, isMovingRight);
            InputManager.AddToKeyboardMap(Keys.Space, isJumping);
        }

        public void Move(Vector2 direction)
        {
            myVelocity += direction * 50;
            moved = true;
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                myVelocityZ = 600;
            }
            else
            {
                myVelocityZ += 5;
            }
        }

        public void UpdateCurrentPlatform(List<PlatformSprite> platforms)
        {
            if (!IsGrounded || falling) return;
            currentPlatform = null;
            foreach (PlatformSprite platform in platforms)
            {
                if (platform.Contains(myPosition))
                {
                    currentPlatform = platform;
                    lastPlatform = currentPlatform;
                    break;
                }
            }
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            if (currentPlatform == null)
            {
                falling = true;
            }

            if (falling)
            {
                myScale *= 0.9f;
                myVelocity = Vector2.Zero;

                if (myScale < 0.001f)
                {
                    myScale = 1;
                    myPositionZ = 0;
                    myVelocityZ = 0;
                    currentPlatform = lastPlatform;
                    if (currentPlatform != null)
                        myPosition = currentPlatform.myPosition;
                    falling = false;
                }
                return;
            }

            float maxVel = 400;
            myVelocityZ += -2000f * elapsedTime;
            myPositionZ += myVelocityZ * elapsedTime;
            myPositionZ = Math.Max(myPositionZ, 0);
            if (!moved)
            {
                if (IsGrounded)
                    myVelocity *= 0.3f;
                else
                    myVelocity *= 0.9f;
            }
            if (myVelocity.Length() > maxVel)
            {
                myVelocity *= maxVel / myVelocity.Length();
            }
            if (currentPlatform != null)
            {
                if (IsGrounded)
                {
                    pushoffVelocity = currentPlatform.Velocity;
                }
                myPosition += pushoffVelocity * elapsedTime;
                Debug.WriteLine(currentPlatform.Velocity);
            }
            moved = false;
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float heroScale = 0.4f * myScale;
            float shadowScale = heroScale * 0.35f;

            if (!falling)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, 100), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);
            }
            
            Vector2 jumpingPos = myPosition + offset;
            jumpingPos.Y -= myPositionZ; 
            
            batch.Draw(myTexture, jumpingPos, null, Color.White, 0f, new Vector2(myTexture.Width /2 - 10, 
                myTexture.Height - 10), heroScale, SpriteEffects.None, 0f);
        }
    }
}
