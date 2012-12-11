using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Immersion
{
    [Serializable]
    public class WordData : IPathed
    {
        public WordCloudData Parent;
        public String Text;

        public List<Vector2> points = new List<Vector2>();


        float degreeOffset, radMult, avgRadius, wordOffset;
        int revolutions, dir, radDegreeMult;
        Vector2 center;

        float percentThrough;

        public WordData(WordCloudData parent, String text)
        {
            this.Text = text;
            this.Parent = parent;
        }

        public void GeneratePath(float percentThrough, Vector2 center)
        {
            this.percentThrough = percentThrough;
            this.center = center;
        }

        private void Update()
        {
            Vector2 relativeParentStart = Parent.StartPosition - center;
            Vector2 relativeParentEnd = Parent.EndPosition - center;

            double posDegreeStart = Math.Atan2(relativeParentStart.Y, relativeParentStart.X) * 180 / Math.PI;
            double posDegreeEnd = Math.Atan2(relativeParentEnd.Y, relativeParentEnd.X) * 180 / Math.PI;
            if (Math.Abs(posDegreeStart - posDegreeEnd) > 180)
            {

            }

            revolutions = 1;
            if (Parent.StartDegree != Parent.EndDegree)
            {
                Math.Abs((int)Math.Round((posDegreeStart - posDegreeEnd) / (Parent.StartDegree - Parent.EndDegree)));
            }
            if (revolutions < 1) revolutions = 1;
            int posDegreesPassed = 360 * revolutions;

            double posDegreeWedge = posDegreeEnd - posDegreeStart;
            if (posDegreeWedge > 180) posDegreeWedge -= 360;
            if (posDegreeWedge < -180) posDegreeWedge += 360;

            dir = 0;
            if (Parent.StartDegree != Parent.EndDegree)
            {
                dir = Math.Sign(posDegreeWedge / (Parent.EndDegree - Parent.StartDegree));
            }

            if (dir == 0) dir++;

            degreeOffset = (float)(posDegreeStart + posDegreeEnd) / 2 -
                (Parent.StartDegree + Parent.EndDegree) / 2 * revolutions * dir;
            degreeOffset = (float)posDegreeStart - Parent.StartDegree;

            float speed = 1;
            if (Parent.StartDegree != Parent.EndDegree)
            {
                speed = (Parent.StartPosition - Parent.EndPosition).Length() /
                    Math.Abs(Parent.StartDegree - Parent.EndDegree);
            }
            wordOffset = percentThrough * -Parent.WordOffset;// / Math.Max(speed, 1);

            float startRadius = relativeParentStart.Length();
            float endRadius = relativeParentEnd.Length();
            avgRadius = (startRadius + endRadius) / 2;

            int hash = Math.Abs(Text.GetHashCode() + percentThrough.GetHashCode());
            float hash1 = ((hash) % 100) / 100f;
            float hash2 = ((hash / 100) % 100) / 100f;

            radMult = 0.1f + hash1 * 1.5f;
            radDegreeMult = (int)Math.Round((0.5f + hash2) / radMult * 360 / 1000 * Math.Pow(avgRadius, 0.3));// / avgRadius);
            radDegreeMult = Math.Max(radDegreeMult, 1);
        }

        public Vector2 GetPosition(float degree)
        {
            return GetPointPos(degree);
        }

        public Vector2 GetPointPos(float timeDeg)
        {
            Update();
            timeDeg += wordOffset;
            timeDeg %= 360;

            float degree = degreeOffset + timeDeg * revolutions * dir;
            double timDegRadians = timeDeg * Math.PI / 180;
            float rad = avgRadius * (float)(Math.Sin(timDegRadians * radDegreeMult) * (radMult) + 1);

            float lPerc = 0;
            if (Parent.StartDegree != Parent.EndDegree)
            {
                lPerc = (timeDeg - Parent.StartDegree) / (Parent.EndDegree - Parent.StartDegree);
            }
            

            double degreeRadians = degree / 180 * Math.PI;
            Vector2 radialPoint = new Vector2((float)Math.Cos(degreeRadians), (float)Math.Sin(degreeRadians)) * rad;
            radialPoint += center;

            double degThrough = 0;
            if (Parent.StartDegree != Parent.EndDegree)
            {
                float tDeg = timeDeg;
                while (tDeg < Parent.StartDegree) tDeg += 360;
                while (tDeg > Parent.EndDegree) tDeg -= 360;
                degThrough = (tDeg - Parent.StartDegree) / (Parent.EndDegree - Parent.StartDegree) * 2 * Math.PI;
            }


            float disFromStart = Math.Abs(timeDeg - Parent.StartDegree);
            float modDisFromStart = Math.Abs(timeDeg - 360 - Parent.StartDegree);
            float disFromEnd = Math.Abs(timeDeg - Parent.EndDegree);
            float modDisFromEnd = Math.Abs(timeDeg + 360 - Parent.EndDegree);
            float dis = Math.Min(disFromStart, Math.Min(disFromEnd, Math.Min(modDisFromEnd, modDisFromStart)));

            //if (dis == modDisFromStart)
            //{
            //    timeDeg = Math.Max(timeDeg - 360, 0);
            //}
            //else if (dis == modDisFromEnd)
            //{
            //    timeDeg = Math.Min(timeDeg + 360, 360);
            //}

            Vector2 linearOffset = new Vector2((float)Math.Cos(degThrough), (float)Math.Sin(degThrough)) * 20;
            Vector2 linearPoint = Parent.GetForcedPath(timeDeg);
            linearPoint += linearOffset;


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
