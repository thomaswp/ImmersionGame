using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Immersion
{
    [Serializable]
    public class WordData
    {
        public WordCloudData Parent;
        public String Text;

        public List<Vector2> points = new List<Vector2>();


        float degreeOffset, radMult, avgRadius, wordOffset;
        int revolutions, dir, radDegreeMult;

        public WordData(WordCloudData parent, String text)
        {
            this.Text = text;
            this.Parent = parent;
        }

        public void GeneratePath(float percentThrough)
        {
            double posDegreeStart = Math.Atan2(Parent.StartPosition.Y, Parent.StartPosition.X) * 180 / Math.PI;
            double posDegreeEnd = Math.Atan2(Parent.EndPosition.Y, Parent.EndPosition.X) * 180 / Math.PI;
            if (Math.Abs(posDegreeStart - posDegreeEnd) > 180)
            {

            }

            revolutions = (int)Math.Round((posDegreeStart - posDegreeEnd) / (Parent.StartDegree - Parent.EndDegree));
            if (revolutions < 1) revolutions = 1;
            int posDegreesPassed = 360 * revolutions;

            double posDegreeWedge = posDegreeEnd - posDegreeStart;
            if (posDegreeWedge > 180) posDegreeWedge -= 360;
            dir = Math.Sign(posDegreeWedge / (Parent.EndDegree - Parent.StartDegree));

            degreeOffset = (float)(posDegreeStart + posDegreeEnd) / 2 - 
                (Parent.StartDegree + Parent.EndDegree) / 2 * revolutions * dir;

            wordOffset = -10 * percentThrough;

            float startRadius = Parent.StartPosition.Length();
            float endRadius = Parent.EndPosition.Length();
            avgRadius = (startRadius + endRadius) / 2;

            int hash = Math.Abs(Text.GetHashCode() + percentThrough.GetHashCode());
            float hash1 = ((hash) % 100) / 100f;
            float hash2 = ((hash / 100) % 100) / 100f;
            
            radMult = 0.1f + hash1 * 1.5f;
            radDegreeMult = (int)Math.Round((0.5f + hash2) / radMult * 360 / avgRadius);
            radDegreeMult = Math.Max(radDegreeMult, 1);

            //int pointCount = 75 * revolutions;
            //for (int i = 0; i < pointCount; i++)
            //{
            //    float timeDeg = i / (float)pointCount * 360 + wordOffset;

            //    float degree = degreeOffset + timeDeg * revolutions * dir;
            //    double timDegRadians = timeDeg * Math.PI / 180;
            //    float rad = avgRadius * (float)(Math.Sin(timDegRadians * radDegreeMult + degreeOffset) * (radMult) + 1);

            //    float lPerc = (timeDeg - Parent.StartDegree) / (Parent.EndDegree - Parent.StartDegree);
            //    Vector2 linearPoint = (1 - lPerc) * Parent.StartPosition + lPerc * Parent.EndPosition;

            //    double degreeRadians = degree / 180 * Math.PI;
            //    Vector2 radialPoint = new Vector2((float)Math.Cos(degreeRadians), (float)Math.Sin(degreeRadians)) * rad;


            //    float disFromStart = Math.Abs(timeDeg - Parent.StartDegree);//,
            //        //Math.Abs(timeDeg - 360 - Parent.StartDegree));
            //    float disFromEnd = Math.Abs(timeDeg - Parent.EndDegree);//,
            //        //Math.Abs(timeDeg + 360 - Parent.EndDegree));
            //    float dis = Math.Min(disFromStart, disFromEnd);
            //    if (timeDeg >= Parent.StartDegree && timeDeg <= Parent.EndDegree)
            //    {
            //        dis = 0;
            //    }
            //    dis /= 180;
            //    dis = Math.Max(dis, 0);

            //    float rate = 1 - dis;
            //    rate = (float)Math.Pow(rate, 10);

            //    points.Add(linearPoint * (rate) + radialPoint * (1 - rate));
            //    //points.Add(radialPoint);
            //}
        }

        public Vector2 GetPosition(float degree)
        {
            return GetPointPos(degree);
        }

        public Vector2 GetPointPos(float timeDeg)
        {

            timeDeg += wordOffset;

            float degree = degreeOffset + timeDeg * revolutions * dir;
            double timDegRadians = timeDeg * Math.PI / 180;
            float rad = avgRadius * (float)(Math.Sin(timDegRadians * radDegreeMult + degreeOffset) * (radMult) + 1);

            float lPerc = (timeDeg - Parent.StartDegree) / (Parent.EndDegree - Parent.StartDegree);
            Vector2 linearPoint = (1 - lPerc) * Parent.StartPosition + lPerc * Parent.EndPosition;

            double degreeRadians = degree / 180 * Math.PI;
            Vector2 radialPoint = new Vector2((float)Math.Cos(degreeRadians), (float)Math.Sin(degreeRadians)) * rad;


            float disFromStart = Math.Abs(timeDeg - Parent.StartDegree);//,
            disFromStart = Math.Min(disFromStart, Math.Abs(timeDeg - 360 - Parent.StartDegree));
            float disFromEnd = Math.Abs(timeDeg - Parent.EndDegree);//,
            disFromEnd = Math.Min(disFromEnd, Math.Abs(timeDeg + 360 - Parent.EndDegree));
            float dis = Math.Min(disFromStart, disFromEnd);
            if (timeDeg >= Parent.StartDegree && timeDeg <= Parent.EndDegree)
            {
                dis = 0;
            }
            dis /= 180;
            dis = Math.Max(dis, 0);

            float rate = 1 - dis;
            rate = (float)Math.Pow(rate, 10);

           return linearPoint * (rate) + radialPoint * (1 - rate);
        }
    }
}
