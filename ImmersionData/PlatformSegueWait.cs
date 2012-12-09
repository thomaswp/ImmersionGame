using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class PlatformSegueWait: PlatformSegue
    {
        public PlatformSegueWait(Vector2 destination) : base(destination) { }

        public override Vector2 GetPosition(Vector2 start, float perc)
        {
            return start;
        }

        public override bool HasLength()
        {
            return false;
        }

        public override string[] GetProperties()
        {
            return new String[]
            {
                "Speed"
            };
        }

        public override void ChangeProperty(int index, float value)
        {
            
        }
    }
}
