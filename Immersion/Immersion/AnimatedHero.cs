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
    public class AnimatedHero : Sprite 
    {

        //max distance a platform can move in a frame and bring the player with it
        //otherwise we assume it's supposed to dump them
        const float MAX_PLATFORM_JUMP = 50;
        //Time it takes to fade out when moving to a new platform
        const float MAP_TRANSITION_TIME = 2;
        //Max speed the player accelerate to
        //by using the controls
        const float MAX_SPEED = 400;
        //Cooldown on the blink ability
        const float BLINK_COOLDOWN = 1.5f;

        //Z for shadow
        protected float myPositionZ;
        protected float myVelocityZ;

        protected Texture2D shadowImage;

        //Current platform the player is standing on
        public PlatformSprite currentPlatform;
        //Last "Safe" Platform
        protected PlatformSprite respawnPlatform;
        
        //Velocity leaves the platform with
        protected Vector2 pushoffVelocity;

        //have we fallen?
        protected bool falling;
        
        //Animation
        protected Flipbook myIdleBook;
        protected Flipbook myRunBook;
        protected Flipbook currentBook;

        //timer for level transitions
        protected float transitionTime;

        //are we sliding, facing left, running?
        protected bool sliding;
        protected bool facingLeft;
        protected bool running;

        //Last direction we moved
        protected Vector2 lastDirection;

        //fall and slide sound effects
        protected SoundEffect fall;
        protected SoundEffect slide;

        //cool down for blink
        protected float coolDownTime;

        //have we gotten the right items for abilities
        protected bool canBlink, canSlide;

        //items collected
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

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            fall = content.Load<SoundEffect>("fall");
            slide = content.Load<SoundEffect>("slide");//.CreateInstance();
            //slide.IsLooped = true;
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

            GameAction isBlinkable = new GameAction(this, this.GetType().GetMethod("BlinkAbility"), new Object[0]);
            InputManager.AddToKeyboardMap(Keys.W, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.S, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.A, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.D, isMovingRight);
            InputManager.AddToKeyboardMap(Keys.Up, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.Down, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.Left, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.Right, isMovingRight);
            InputManager.AddToKeyboardPressMap(Keys.OemQuestion, isBlinkable);
            InputManager.AddToKeyboardPressMap(Keys.Z, isBlinkable);

            InputManager.AddToControllerPressMap(Buttons.A, isJumping);
            InputManager.AddToControllerPressMap(Buttons.B, isBlinkable);

            InputManager.AddToKeyboardPressMap(Keys.Space, isJumping);
        }

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            //modify the last direction
            lastDirection += direction;

            //Max speed should be the max of either how fast they are NOW 
            //or the max they can acellerate to
            float maxSpeed = MAX_SPEED;
            if (sliding) maxSpeed *= 1.3f;
            if (!IsGrounded) maxSpeed -= pushoffVelocity.Length();
            maxSpeed = Math.Max(myVelocity.Length(), maxSpeed);

            myVelocity += direction * 50;
            moved = true;

            if (myVelocity.Length() > maxSpeed)
            {
                myVelocity.Normalize();
                myVelocity *= maxSpeed;
            }

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
            Move(new Vector2(InputManager.GetStickPosition().X, (InputManager.GetStickPosition().Y * -1)));
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                myVelocityZ = 600;
                if (currentPlatform != null && currentPlatform.data.Launch)
                {
                    //Keep the platform's velocity on launch platforms
                    pushoffVelocity = currentPlatform.Velocity;
                }
                else
                {
                    pushoffVelocity = Vector2.Zero;
                }
            }
            else
            {
                //let them jump a little higher by holding down
                myVelocityZ += 7;
            }
        }

        public void BlinkAbility()
        {
            if (canBlink && coolDownTime <= 0)
            {
                if (lastDirection != Vector2.Zero)
                {
                    lastDirection.Normalize();
                    coolDownTime = BLINK_COOLDOWN;
                    myPosition += 200f * lastDirection;
                }
            }
        }

        private void UpdateCurrentPlatform(float elapsedTime, GameState gameState)
        {

            //gravity
            myVelocityZ += -2000f * elapsedTime;
            myPositionZ += myVelocityZ * elapsedTime;
            myPositionZ = Math.Max(myPositionZ, 0);

            //Update the current platform
            if (IsGrounded && !falling)
            {
                PlatformSprite lastPlatform = currentPlatform;
                currentPlatform = null;
                foreach (PlatformSprite platform in gameState.myPlatforms)
                {
                    if (platform.Solid && platform.Contains(myPosition))
                    {
                        currentPlatform = platform;
                        if (platform.Safe)
                        {
                            respawnPlatform = currentPlatform;
                        }
                        if (currentPlatform != lastPlatform && pushoffVelocity != Vector2.Zero)
                        {
                            myVelocity += pushoffVelocity;
                            pushoffVelocity = Vector2.Zero;
                        }
                    }
                    platform.RespawnPlatform = respawnPlatform == platform;
                }
            }
            foreach (PlatformSprite platform in gameState.myPlatforms)
            {
                platform.UpdateHeroOnPlatform(currentPlatform == platform && IsGrounded && !falling, elapsedTime);
            }
        }

        public void Reset()
        {
            transitionTime = 0;
            myPosition = new Vector2();
            myPositionZ = 0;
        }

        public override void Update(float elapsedTime, GameState gameState)
        {

            base.Update(elapsedTime, gameState);


            coolDownTime -= elapsedTime;
            UpdateCurrentPlatform(elapsedTime, gameState);

            myIdleBook.Update((double)elapsedTime);
            myRunBook.Update((double)elapsedTime);


            running = lastDirection != Vector2.Zero;
            lastDirection = Vector2.Zero;


            //fall if we have no platform
            if (currentPlatform == null)
            {
                if (!falling)
                {
                    fall.Play(0.7f, 0, 0);
                }
                falling = true;
            }

            //become wee if falling
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

            bool wasSliding = sliding;

            sliding = canSlide && (InputManager.IsSliding());

            if (sliding && !wasSliding)
            {
               slide.Play();
            }

            //slide as much as the platform would have you slide
            float slideFactor = (float)Math.Pow(currentPlatform.data.Slide, 0.1);
            float factor = 1f;
            if (!moved || myVelocity.Length() > MAX_SPEED)
            {
                //Slow down if we're not moving
                factor = 0.98f;
                //Show down more if we're on the ground than in the air
                if (IsGrounded)
                {
                    if (!sliding)
                    {
                        //minimum of 0.3 friction
                        factor = 0.3f + slideFactor * 0.7f;
                    }
                }
            } 
            myVelocity *= factor;
            Vector2 edge;
            if (IsGrounded &&  currentPlatform != null && (edge = currentPlatform.NearEdge(myPosition)) != Vector2.Zero)
            {
                //If we're near the edge, slow them down
                Vector2 nV = myVelocity; 
                if (nV != Vector2.Zero) nV.Normalize();
                if (edge != Vector2.Zero) edge.Normalize();
                if ((nV + edge).Length() > Math.Sqrt(2))
                {
                    myVelocity /= Math.Max(20 * (1 - slideFactor), 1);
                }
            }

            //Update the pushOffVelocity
            //and move the hero with his platform
            if (currentPlatform != null)
            {
                if (IsGrounded)
                {
                    if (currentPlatform.LastFrameMovement.Length() < MAX_PLATFORM_JUMP)
                    {
                        myPosition += currentPlatform.LastFrameMovement;
                    }
                }
                else
                {
                    myPosition += pushoffVelocity * elapsedTime;
                }
            }
            moved = false;

            //Transition to the next level
            float transTime = 0;
            if (currentPlatform != null)
            {
                if (currentPlatform.Item != null)
                {
                    if ((myPosition - currentPlatform.Item.myPosition).Length() < 35)
                    {
                        currentPlatform.Item.IsCollected = true;
                        ItemData item = currentPlatform.data.Item;
                        if (!Items.Contains(item))
                        {
                            Items.Add(item);
                            if (item.EnablesBlink)
                            {
                                canBlink = true;
                            }
                            if (item.EnablesSlide)
                            {
                                canSlide = true;
                            }
                        }
                    }
                }

                if (IsGrounded && currentPlatform.OnMapTransition(myPosition))
                {
                    transTime = transitionTime + elapsedTime;
                }
            }
            transitionTime = transTime;


            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left != new Vector2(0, 0))
            {
                StickMove();
            }
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            float heroScale = 1.5f * myScale;
            float shadowScale = heroScale * .11f;
            float trans = Math.Max(0, (1 - transitionTime / MAP_TRANSITION_TIME));



            if (!falling)
            {
                //Draw the shadow
                batch.Draw(shadowImage, myPosition + offset, null, new Color(255, 255, 255, (byte)(100 * trans)), 0f,
                    new Vector2(shadowImage.Width / 2, shadowImage.Height / 2),
                    shadowScale / (1 + myPositionZ / 100), SpriteEffects.None, 0f);
            }

            Vector2 jumpingPos = myPosition + offset;
            jumpingPos.Y -= myPositionZ;

            Color transColor = Color.White;
            transColor.A = (byte)(255 * trans);

            //Show cooldown on blink
            if (coolDownTime > 0)
            {
                float perc = 1 - coolDownTime / BLINK_COOLDOWN;
                transColor.G = (byte)(perc * transColor.G);
            }

            //draw the right animation
            if (running && !sliding)
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

