using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immersion
{
    public class PlatformSegueCurved : PlatformSegue
    {
        public float curvature = 0;

        public PlatformSegueCurved(Vector2 destination) : base(destination) { }

        public override Vector2 getPosition(Vector2 start, float perc)
        {

            Vector2 center = (start + Destination) / 2;
            Vector2 relStart = start - center;

            double startDeg = Math.Atan2(relStart.Y, relStart.X);
            double off90 = startDeg + Math.PI / 2;
            center += new Vector2((float)Math.Cos(off90), (float)Math.Sin(off90)) * curvature;

            relStart = start - center;
            Vector2 relEnd = Destination - center;
            startDeg = Math.Atan2(relStart.Y, relStart.X);
            double endDeg = Math.Atan2(relEnd.Y, relEnd.X);

            while (endDeg < startDeg) endDeg += Math.PI * 2;

            double deg = startDeg + (endDeg - startDeg) * perc;
            float rad = relStart.Length();


            return center + new Vector2((float)Math.Cos(deg), (float)Math.Sin(deg)) * rad;

        }

        public override string[] getProperties()
        {
            return new String[] 
            {
                "Speed",
                "Curvature"
            };
        }

        public override void changeProperty(int index, float value)
        {
            if (index == 1)
            {
                curvature += value / 50;
            }
        }
    }
}
