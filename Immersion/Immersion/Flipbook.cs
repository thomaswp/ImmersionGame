//all code by Shannon Duvall
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
using Microsoft.Xna.Framework.Media;

namespace Immersion
{
    public class Flipbook
    {
        // The actual images
        private Texture2D[] myImages;
        private int currentFrame = 0;
        private double elapsedTime = 0;
        // The list of frames - each frame is an image index and a time to show the image
        private List<Frame> frames;

        public Flipbook(Texture2D[] images)
        {
            myImages = images;
            frames = new List<Frame>();
        }

        public void AddFrame(int index, double seconds)
        {
            Frame frame = new Frame(index, seconds);
            frames.Add(frame);
        }

        public Texture2D GetImage()
        {
            return myImages[frames[currentFrame].index];
        }

        public void Update(double seconds)
        {
            elapsedTime += seconds;
            if (elapsedTime >= frames[currentFrame].seconds)
            {
                elapsedTime = 0;
                currentFrame = (currentFrame + 1) % frames.Count;
            }
        }

        class Frame
        {
            // the index into the images array to tell which image I should show
            public int index;
            // And how long it should show
            public double seconds;

            public Frame(int ind, double secs)
            {
                index = ind;
                seconds = secs;
            }
        }
    }
}
