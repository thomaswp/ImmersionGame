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
    public class Sprite
    {
        protected internal Texture2D myTexture;
        protected internal Vector2 myPosition;
        protected internal Vector2 myVelocity = new Vector2(0, 0);
        protected internal float myAngle = 0f;
        protected internal float myAngularVelocity = 0f;
        protected internal float myScale = 1f;
        protected internal Color myColor = Color.White;


        public Sprite(Texture2D texture, Vector2 position)
        {
            this.myTexture = texture;
            this.myPosition = position;
        }


        public Sprite(Texture2D texture2D, Vector2 position, Vector2 velocity, float angle, float angleVelocity, float scale)
        {
            // TODO: Complete member initialization
            this.myTexture = texture2D;
            this.myPosition = position;
            this.myVelocity = velocity;
            this.myAngle = angle;
            this.myAngularVelocity = angleVelocity;
            this.myScale = scale;
        }

        public virtual void Update(float elapsedTime, GameState gameState)
        {
            myPosition += myVelocity * elapsedTime;
            myAngle += myAngularVelocity * elapsedTime;
        }

        public virtual void Draw(SpriteBatch batch, Vector2 offset)
        {
            batch.Draw(myTexture, myPosition + offset,null, myColor, 0f, 
                new Vector2(myTexture.Width / 2,myTexture.Height / 2),myScale,SpriteEffects.None, 0f);
        }
    }
}
