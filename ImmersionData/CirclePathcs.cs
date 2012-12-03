using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class CirclePath : IPathed
    {
        public Vector2 center;
        public float radius;
        public int speedMult;

        public CirclePath(Vector2 center, float radius, int speedMult)
        {
            this.center = center;
            this.radius = radius;
        }

        public Vector2 GetPosition(float degree)
        {
            double rads = degree * speedMult * Math.PI / 180;
            return center + new Vector2((float)Math.Sin(rads), (float)Math.Cos(rads)) * radius;
        }
    }
}
