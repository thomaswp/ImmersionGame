using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    public abstract class PlatformSegue
    {
        public Vector2 start, end;

        public PlatformSegue(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public abstract Vector2 getPosition(float perc);
    }
}
