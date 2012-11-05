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

        public PlatformData(float startX, float startY) : this(startX, startY, 0) { }

        public Vector2 getSegueStart()
        {
            if (segues.Count > 0)
                return segues[segues.Count - 1].end;
            return startPos;
        }

        public PlatformData(float startX, float startY, int degree)
        {
            startPos = new Vector2(startX, startY) - getPosition(degree);
            segues.Add(new PlatformSegueLinear(getSegueStart(), getSegueStart() + new Vector2(100, 100)));
            segues.Add(new PlatformSegueLinear(getSegueStart(), getSegueStart() + new Vector2(100, -100)));
        }

        public Vector2 getPosition(int frame)
        {
            if (segues.Count == 0) return startPos;

            float perc = frame / 361f;
            int segueIndex = (int)(segues.Count * perc);
            float sPerc = (perc - (float)segueIndex / segues.Count) * segues.Count;

            return segues[segueIndex].getPosition(sPerc);
        }
    }
}
