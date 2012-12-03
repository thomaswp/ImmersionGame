using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    [Serializable]
    public class WordCloudData
    {
        private float startDegree, endDegree;
        private Vector2 center;
        public IPathed PathedObject;

        public Vector2 StartPosition { get { return GetForcedPath(startDegree); } }
        public Vector2 EndPosition { get { return GetForcedPath(endDegree); } }
        public float StartDegree { get { return startDegree; } set { startDegree = value; GeneratePaths(); } }
        public float EndDegree { get { return endDegree; } set { endDegree = value; GeneratePaths(); } }
        public Vector2 Center { get { return center; } set { center = value; GeneratePaths(); } }

        public String Name
        {
            get
            {
                String name = "";
                foreach (WordData word in Words)
                {
                    if (name != "") name += " ";
                    name += word.Text;
                }
                return name;
            }
        }

        public readonly List<WordData> Words;


        private static List<String> extractWords(WordCloudData data)
        {
            List<String> words = new List<string>();
            foreach (WordData word in data.Words)
            {
                words.Add(word.Text);
            }
            return words;
        }

        public WordCloudData(WordCloudData toCopy) : this(toCopy.PathedObject, toCopy.startDegree, toCopy.endDegree, extractWords(toCopy)) { this.center = toCopy.center; }

        public WordCloudData(Vector2 startPosition, float startDegree, Vector2 endPosition, float endDegree, List<String> words) : 
            this(new LinearPath(startPosition, startDegree, endPosition, endDegree), startDegree, endDegree, words) {}


        public WordCloudData(IPathed pathed, float startDegree, float endDegree, List<String> words)
        {
            this.startDegree = startDegree;
            this.endDegree = endDegree;
            this.PathedObject = pathed;

            Words = new List<WordData>();
            foreach (String word in words)
            {
                Words.Add(new WordData(this, word));
            }

            GeneratePaths();
        }

        public Vector2 GetForcedPath(float degree)
        {
            return PathedObject.GetPosition(degree);
        }

        public void GeneratePaths()
        {
            for (int i = 0; i < Words.Count; i++)
            {
                Words[i].GeneratePath(i - Words.Count / 2, center);// / (float)Words.Count);
            }
        }

    }
}
