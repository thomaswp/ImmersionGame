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
    class Hero : Sprite
    {

        public Hero(Texture2D image)
            : base(image, new Vector2(350, 260))
        {
            SetUpInput();
        }

        public void SetUpInput()
        {
            //Set up game actions for isMovingLeft/Forward/Right/Jumping

            object[] forwardRate = { 5f };
            GameAction isMovingForward = new GameAction(this, this.GetType().GetMethod("MoveForward"), forwardRate);

            object[] rightRate = { 5f };
            GameAction isMovingRight = new GameAction(this, this.GetType().GetMethod("MoveRight"), rightRate);
            
            object[] leftRate = { 5f };
            GameAction isMovingLeft = new GameAction(this, this.GetType().GetMethod("MoveLeft"), leftRate);

            object[] downRate = { 5f };
            GameAction isMovingDown = new GameAction(this, this.GetType().GetMethod("MoveDown"), downRate);

            GameAction isJumping = new GameAction(this, this.GetType().GetMethod("Jump"), new Object[0]);

            InputManager.AddToKeyboardMap(Keys.W, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.S, isMovingDown);
            InputManager.AddToKeyboardMap(Keys.A, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.D, isMovingRight);
            InputManager.AddToKeyboardMap(Keys.Space, isJumping);
        }

        public void MoveForward(float rate)
        {
            myPosition.Y -= rate;
        }
        public void MoveRight(float rate)
        {
            myPosition.X += rate;
        }
        public void MoveLeft(float rate)
        {
            myPosition.X -= rate;
        }

        public void MoveDown(float rate)
        {
            myPosition.Y += rate;
        }
        public void Jump()
        {
            //myVelocity.Y = -3f;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
