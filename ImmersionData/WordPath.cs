using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    public class WordPath
    {
        public WordPath(Vector2[] points) //4 points (x, y)
        {
        }

        public Vector2 getPosition(float perc) //[0-1)
        {
            //return the poistion of a spline between the middle 2 points using
            //the endpoints to guess slope
            //but you're doing it parmetrically
            //0 -> points[1] and 1 -> points[2]
            return Vector2.Zero;
        }
    }
}
