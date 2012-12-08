﻿using System;
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
    class AnimatedHero : Sprite 
    {

        const float MAX_PLATFORM_JUMP = 30;
        const float MAP_TRANSITION_TIME = 2;

        protected float myPositionZ;
        protected float myVelocityZ;
        protected Texture2D shadowImage;
        public PlatformSprite currentPlatform;
        protected Vector2 pushoffVelocity;
        protected PlatformSprite respawnPlatform;
        protected bool falling;
        protected Flipbook myBook;
        protected float transitionTime;

        public List<ItemData> Items = new List<ItemData>();

        private bool moved;

        public bool IsGrounded
        {
            get { return myPositionZ == 0; }
        }

        public bool IsTransitioned
        {
            get { return transitionTime > MAP_TRANSITION_TIME; }
        }

        public AnimatedHero(Texture2D[] images, Texture2D shadow, Vector2 position, Vector2 screenSize)
            : base(images[0], position)
        {
            myBook = new Flipbook(images);
            setUpFlipbook();
            shadowImage = shadow;
            SetUpInput();
        }

        public void setUpFlipbook()
        {
            myBook.AddFrame(0, .1);
            myBook.AddFrame(1, .1);
            myBook.AddFrame(2, .1);
            myBook.AddFrame(3, .1);
            myBook.AddFrame(2, .1);
            myBook.AddFrame(1, .1);
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
            InputManager.AddToKeyboardPressMap(Keys.Space, isJumping);
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
                myVelocityZ += 7;
            }
        }

        public void UpdateCurrentPlatform(float elapsedTime, List<PlatformSprite> platforms)
        {
            foreach (PlatformSprite platform in platforms)
            {
                platform.UpdateHeroOnPlatform(currentPlatform == platform && IsGrounded && !falling, elapsedTime);
            }
            if (!IsGrounded || falling) return;
            currentPlatform = null;
            foreach (PlatformSprite platform in platforms)
            {
                if (platform.Solid && platform.Contains(myPosition))
                {
                    currentPlatform = platform;
                    if (platform.Safe)
                    {
                        respawnPlatform = currentPlatform;
                    }
                }
                platform.RespawnPlatform = respawnPlatform == platform;
            }
        }

        public void Reset()
        {
            transitionTime = 0;
            myPosition = new Vector2();
            myPositionZ = 0;
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            myBook.Update((double)elapsedTime);
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
                    currentPlatform = respawnPlatform;
                    if (currentPlatform != null)
                        myPosition = currentPlatform.myPosition;
                    falling = false;
                }
                return;
            }

            myVelocityZ += -2000f * elapsedTime;
            myPositionZ += myVelocityZ * elapsedTime;
            myPositionZ = Math.Max(myPositionZ, 0);

            float maxVel = 400;
            float factor = 1f;
            if (!moved)
            {
                factor = 0.9f;
                if (IsGrounded)
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.L))
                    {
                        factor = 0.3f;
                    }
                }
            }
            myVelocity *= factor;
            Vector2 edge;
            if (IsGrounded &&  currentPlatform != null && (edge = currentPlatform.NearEdge(myPosition)) != Vector2.Zero)
            {
                Vector2 nV = myVelocity; nV.Normalize();
                edge.Normalize();
                if ((nV + edge).Length() > Math.Sqrt(2))
                {
                    myVelocity /= 20;
                }
            }


            if (myVelocity.Length() > maxVel)
            {
                myVelocity *= maxVel / myVelocity.Length();
            }
            if (currentPlatform != null)
            {
                if (IsGrounded)
                {
                    if (currentPlatform.LastFrameMovement.Length() < MAX_PLATFORM_JUMP)
                    {
                        myPosition += currentPlatform.LastFrameMovement;
                    }
                    pushoffVelocity = currentPlatform.Velocity;
                }
                else
                {
                    myPosition += pushoffVelocity * elapsedTime;
                }
            }
            moved = false;

            float transTime = 0;
            if (currentPlatform != null)
            {
                if (currentPlatform.Item != null)
                {
                    if ((myPosition - currentPlatform.Item.myPosition).Length() < 35)
                    {
                        currentPlatform.Item.IsCollected = true;
                        Items.Add(currentPlatform.data.Item);
                    }
                }

                if (IsGrounded && currentPlatform.OnMapTransition(myPosition))
                {
                    transTime = transitionTime + elapsedTime;
                }
            }
            transitionTime = transTime;
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float heroScale = 0.4f * myScale;
            float shadowScale = heroScale * 0.35f;
            float trans = Math.Max(0, (1 - transitionTime / MAP_TRANSITION_TIME));

            if (!falling)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, (byte)(100 * trans)), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);
            }

            Vector2 jumpingPos = myPosition + offset;
            jumpingPos.Y -= myPositionZ;

            Color transColor = Color.White;
            transColor.A = (byte)(255 * trans);

            batch.Draw(myBook.GetImage(), jumpingPos, null, transColor, 0f, new Vector2(myBook.GetImage().Width / 2 - 10,
               myBook.GetImage().Height - 10), heroScale, SpriteEffects.None, 0f);
        }
    }
}

