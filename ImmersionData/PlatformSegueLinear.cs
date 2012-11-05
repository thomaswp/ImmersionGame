using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immersion
{
    class PlatformSegueLinear : PlatformSegue
    {
        public PlatformSegueLinear(Vector2 start, Vector2 end) : base(start, end) { }

        public override Microsoft.Xna.Framework.Vector2 getPosition(float perc)
        {
            return start * (1 - perc) + end * perc;
        }
    }
}
