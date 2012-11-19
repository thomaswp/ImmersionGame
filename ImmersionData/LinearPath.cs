using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class LinearPath : IPathed
    {
        private Vector2 startPos, endPos;
        private float startDegree, endDegree;

        public LinearPath(Vector2 startPos, float startDegree, Vector2 endPos, float endDegree)
        {
            this.startPos = startPos;
            this.endPos = endPos;
            this.startDegree = startDegree;
            this.endDegree = endDegree;
        }

        public Vector2 GetPosition(float degree)
        {
            return startPos + (endPos - startPos) / (endDegree - startDegree) * (degree - startDegree);
        }
    }
}
