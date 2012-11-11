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
        public Vector2 Destination;

        public PlatformSegue(Vector2 destination)
        {
            this.Destination = destination;
        }

        public abstract Vector2 getPosition(Vector2 start, float perc);

        public abstract String[] getProperties();
        public abstract void changeProperty(int index, float value);
    }
}
