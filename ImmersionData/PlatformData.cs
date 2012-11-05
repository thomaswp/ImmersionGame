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
        public double startX, startY;

        public Vector2 getPosition(int frame)
        {
            return new Vector2(0, 0);
        }
    }
}
