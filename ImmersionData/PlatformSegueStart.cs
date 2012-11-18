using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class PlatformSegueStart : PlatformSegue
    {
        public PlatformSegueStart(Vector2 destination) : base(destination) { }

        public override void ChangeProperty(int index, float value)
        {
            
        }

        public override Microsoft.Xna.Framework.Vector2 GetPosition(Microsoft.Xna.Framework.Vector2 start, float perc)
        {
            return Destination;
        }

        public override string[] GetProperties()
        {
            return new string[0];
        }
    }
}
