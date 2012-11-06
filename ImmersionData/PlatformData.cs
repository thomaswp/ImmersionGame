using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class PlatformData
    {
        public Vector2 startPos;
        public List<PlatformSegue> segues = new List<PlatformSegue>();

        public PlatformData(Vector2 startPos) : this(startPos, 0) { }

        public Vector2 getNextSegueStart()
        {
            if (segues.Count > 0)
                return segues[segues.Count - 1].Destination;
            return startPos;
        }

        public PlatformData(Vector2 startPos, int degree)
        {
            this.startPos = startPos;
            //segues.Add(new PlatformSegueLinear(getSegueStart(), getSegueStart() + new Vector2(100, 100)));
            //segues.Add(new PlatformSegueLinear(getSegueStart(), getSegueStart() + new Vector2(100, -100)));
            //startPos = 2 * startPos - getPosition(degree);
        }

        public Vector2 getPosition(int frame)
        {
            if (segues.Count == 0) return startPos;

            float perc = frame / 361f;
            int segueIndex = (int)(segues.Count * perc);
            float sPerc = (perc - (float)segueIndex / segues.Count) * segues.Count;
            Vector2 start = segueIndex > 0 ? segues[segueIndex - 1].Destination : startPos;

            return segues[segueIndex].getPosition(start, sPerc);
        }

        public bool contains(Vector2 position, int frame)
        {
            return (position - getPosition(frame)).Length() < 25;
        }
    }
}
