using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public abstract class PlatformSegue
    {
        public float Weight = 1;

        public Vector2 Destination;
        public float SegWeight { get { return HasLength() ? Weight : 0; } set { Weight = value; } }

        public PlatformSegue(Vector2 destination)
        {
            this.Destination = destination;
        }

        public abstract Vector2 GetPosition(Vector2 start, float perc);
        public abstract bool HasLength();
        public abstract String[] GetProperties();
        public abstract void ChangeProperty(int index, float value);
    }
}
