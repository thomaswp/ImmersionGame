using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Immersion
{
    public class WordData
    {
        public WordCloudData Parent;
        public String Text;

        public List<Vector2> points = new List<Vector2>();
        private float degreeOffset;

        public WordData(WordCloudData parent, String text)
        {
            this.Text = text;
            this.Parent = parent;
        }

        public void GeneratePath()
        {
            double degreeStart = Math.Atan2(Parent.StartPosition.Y, Parent.StartPosition.X) * 180 / Math.PI;
            double degreeEnd = Math.Atan2(Parent.EndPosition.Y, Parent.EndPosition.X) * 180 / Math.PI;

            degreeStart -= Parent.StartDegree;
            degreeEnd -= Parent.EndDegree;

           // while (degreeStart < 0) degreeStart += 360;
           // while (degreeEnd < degreeStart) degreeEnd += 360;

            points.Clear();
            for (int i = 0; i < Parent.StartDegree; i += 5)
            {
                addNormalPoint(i);
            }
            float wedge = Parent.EndDegree - Parent.StartDegree;
            float div =  wedge / (int)(wedge / 5 + 1);
            for (float d = Parent.StartDegree; d <= Parent.EndDegree; d += div)
            {
                addPointOnPath(d);
            }
            for (int i = (int)(Parent.EndDegree / 5) + 1; i <= 360; i++)
            {
                addNormalPoint(i);
            }
        }

        private void addNormalPoint(float degree)
        {
            double degRad = degree * Math.PI / 180;
            float rad = 10;
            points.Add(new Vector2((float)Math.Cos(degRad) * rad, (float)Math.Sin(degRad) * rad));
        }

        private void addPointOnPath(float degree)
        {

        }

        public Vector2 getPosition(float degree)
        {
            return Vector2.Zero;
        }
    }
}
