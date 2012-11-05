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
            : base(image, new Vector2(100, 100))
        {
            SetUpInput();
        }

        public void SetUpInput()
        {
            //Set up game actions for isMovingLeft/Forward/Right/Jumping
            object[] forwardRate = { 1f };
            GameAction isMovingForward = new GameAction(this, this.GetType().GetMethod("MoveForward"), forwardRate);

            object[] rightRate = { 1f };
            GameAction isMovingRight = new GameAction(this, this.GetType().GetMethod("MoveForward"), rightRate);
            
            object[] leftRate = { 1f };
            GameAction isMovingLeft = new GameAction(this, this.GetType().GetMethod("MoveForward"), leftRate);

            InputManager.AddToKeyboardMap(Keys.A, isMovingLeft);
            InputManager.AddToKeyboardMap(Keys.W, isMovingForward);
            InputManager.AddToKeyboardMap(Keys.D, isMovingRight);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
