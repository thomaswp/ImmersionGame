using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immersion
{
    public class PlatformSegueLinear : PlatformSegue
    {
        public PlatformSegueLinear(Vector2 destination) : base(destination) { }

        public override Vector2 getPosition(Vector2 start, float perc)
        {
            return start * (1 - perc) + Destination * perc;
        }

        public override string[] getProperties()
        {
            return new String[]
            {
                "Speed"
            };
        }

        public override void changeProperty(int index, float value)
        {
            
        }
    }
}
