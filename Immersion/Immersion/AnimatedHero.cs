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
        protected Flipbook myIdleBook;
        protected Flipbook myRunBook;
        protected Flipbook currentBook;
        protected float transitionTime;
        protected bool facingLeft;
        protected bool running;

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

        public AnimatedHero(Texture2D[] idleImages, Texture2D[] runImages, Texture2D shadow, Vector2 position, Vector2 screenSize)
            : base(idleImages[0], position)
        {
            myIdleBook = new Flipbook(idleImages);
            myRunBook = new Flipbook(runImages);
            currentBook = myIdleBook;
            setUpFlipbook();
            shadowImage = shadow;
            SetUpInput();
            facingLeft = true;
            running = false;
        }

        public void setUpFlipbook()
        {
            myIdleBook.AddFrame(0, .1);
            myIdleBook.AddFrame(1, .1);
            myIdleBook.AddFrame(2, .1);
            myIdleBook.AddFrame(3, .1);
            myIdleBook.AddFrame(4, .1);
            myIdleBook.AddFrame(5, .1);
            myIdleBook.AddFrame(6, .1);
            myIdleBook.AddFrame(7, .1);
            myRunBook.AddFrame(0, .1);
            myRunBook.AddFrame(1, .1);
            myRunBook.AddFrame(2, .1);
            myRunBook.AddFrame(3, .1);
            myRunBook.AddFrame(4, .1);
            myRunBook.AddFrame(5, .1);
            myRunBook.AddFrame(6, .1);
            myRunBook.AddFrame(7, .1);

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

            object[] stickDir = { InputManager.GetStickPosition() };
            GameAction isMovingStickDir = new GameAction(this, this.GetType().GetMethod("StickMove"), stickDir);

            GameAction isJumping = new GameAction(this, this.GetType().GetMethod("Jump"), new Object[0]);

            InputManager.AddToKeyboardMap(Keys.W, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.S, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.A, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.D, isMovingRight);
            InputManager.AddToKeyboardMap(Keys.Up, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.Down, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.Left, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.Right, isMovingRight);

            InputManager.AddToControllerMap(Buttons.A, isJumping);

            InputManager.AddToKeyboardPressMap(Keys.Space, isJumping);
        }

        public void Move(Vector2 direction)
        {
            myVelocity += direction * 50;
            moved = true;
            if (direction.X < 0f)
            {
                facingLeft = true;
            }
            if (direction.X > 0f)
            {
                facingLeft = false;
            }

        }

        public void StickMove()
        {
            Vector2 stickDir = new Vector2(InputManager.GetStickPosition().X, (InputManager.GetStickPosition().Y * -1));
            myVelocity += stickDir * 50;
            moved = true;
            if (InputManager.GetStickPosition().X < 0f)
            {
                facingLeft = true;
            }
            if (InputManager.GetStickPosition().X > 0f)
            {
                facingLeft = false;
            }
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
            myIdleBook.Update((double)elapsedTime);
            myRunBook.Update((double)elapsedTime);

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left != new Vector2(0, 0))
            {
                StickMove();
                running = true;
            }
            if((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left == new Vector2(0, 0)) && (running == true))
            {
                running=false;
            }


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
            float slideFactor = (float)Math.Pow(currentPlatform.data.Slide, 0.1);
            float factor = 1f;
            if (!moved)
            {
                factor = 0.9f;
                if (IsGrounded)
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.L))
                    {
                        factor = 0.3f + slideFactor * 0.7f;
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
                    myVelocity /= Math.Max(20 * (1 - slideFactor), 1);
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
                    //pushoffVelocity = currentPlatform.Velocity;
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
            float heroScale = 1.5f * myScale;
            float shadowScale = heroScale * .11f;
            float trans = Math.Max(0, (1 - transitionTime / MAP_TRANSITION_TIME));

            if (!falling)
            {
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, (byte)(100 * trans)), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);

                //base.Draw(myTexture, myPosition + offset,null, myColor, myAngle,
                //    new Vector2(myTexture.Width / 2,myTexture.Height / 2), myScale,SpriteEffects.None, 0f);
            }

            Vector2 jumpingPos = myPosition + offset;
            jumpingPos.Y -= myPositionZ;

            Color transColor = Color.White;
            transColor.A = (byte)(255 * trans);

            if (running)
            {
                currentBook = myRunBook;
            }
            else
            {
                currentBook = myIdleBook;
            }
            if (facingLeft)
            {
                batch.Draw(currentBook.GetImage(), jumpingPos, null, transColor, 0f, new Vector2(myIdleBook.GetImage().Width / 2 + 5,
                 myIdleBook.GetImage().Height / 2 + 35), heroScale, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                batch.Draw(currentBook.GetImage(), jumpingPos, null, transColor, 0f, new Vector2(myIdleBook.GetImage().Width / 2 - 5,
                   myIdleBook.GetImage().Height / 2 + 35), heroScale, SpriteEffects.None, 0f);
            }
            

        }
    }
}

