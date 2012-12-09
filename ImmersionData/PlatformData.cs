using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class PlatformData : IPathed
    {
        public Vector2 StartPos 
        { 
            get { return StartSegue.Destination; } 
            set { StartSegue.Destination = value; } 
        }
        public PlatformSegueStart StartSegue;
        public List<PlatformSegue> segues = new List<PlatformSegue>();
        public int Repeats = 1;
        public float DegreeOffset = 0;
        public ItemData Item;
        public Point ItemOffset = new Point();
        public List<Point> Segments = new List<Point>();
        public int FallTime;
        public float Slide;
        public bool Launch;
        public bool SafePlatform;
        public bool Invisible;
        public MapData NextMap;

        public PlatformData(Vector2 startPos) : this(startPos, 0) { }

        public Vector2 getNextSegueStart()
        {
            if (segues.Count > 0)
            {
                return segues[segues.Count - 1].Destination;
            }
            return StartPos;
        }

        public PlatformData(Vector2 startPos, int degree)
        {
            StartSegue = new PlatformSegueStart(startPos);
            Segments.Add(new Point(0, 0));
        }

        private float[] getWeightMarks()
        {
            float totalWeight = 0;
            float[] weightMarks = new float[segues.Count];
            for (int i = 0; i < segues.Count; i++)
            {
                totalWeight += segues[i].SegWeight;
                weightMarks[i] = totalWeight;
            }
            for (int i = 0; i < weightMarks.Length; i++)
            {
                weightMarks[i] /= totalWeight;
            }
            return weightMarks;
        }

        private float getDegreePerc(float degree)
        {
            degree += DegreeOffset;
            while (degree < 0) degree += 360;
            degree = (degree * Repeats) % 360;

            return degree / 360f;
        }

        public bool Wait(float degree, float dd)
        {
            int index; 
            if ((index = GetCurrentSegueIndex(degree)) != GetCurrentSegueIndex(degree + dd))
            {
                if (segues[(index + 1) % segues.Count].GetType() == typeof(PlatformSegueWait))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetCurrentSegueIndex(float degree)
        {
           
            float[] weightMarks = getWeightMarks();
            float perc = getDegreePerc(degree);

            for (int i = 0; i < weightMarks.Length; i++)
            {
                float mark = weightMarks[i];
                if (perc <= mark)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector2 GetPosition(float degree)
        {
            if (segues.Count == 0) return StartPos;

            float perc = getDegreePerc(degree);
            float[] weightMarks = getWeightMarks();

            float lastMark = 0;
            Vector2 start = StartPos;
            for (int i = 0; i < weightMarks.Length; i++)
            {
                float mark = weightMarks[i];
                if (perc <= mark)
                {
                    float sPerc = (perc - lastMark) / (mark - lastMark);
                    return segues[i].GetPosition(start, sPerc);
                }
                lastMark = mark;
                start = segues[i].Destination;
            }
            return Vector2.Zero;
        }

        public Vector2 getVelocity(float degree)
        {
            float dt = 0.001f;
            return (GetPosition(degree) - GetPosition(degree - dt)) / dt;
        }

        public bool contains(Vector2 position, int frame)
        {
            return (position - GetPosition(frame)).Length() < 25;
        }
    }
}
