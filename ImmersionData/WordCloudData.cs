using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Immersion
{
    public class WordCloudData
    {
        private Vector2 startPosition, endPosition;
        private float startDegree, endDegree;

        public Vector2 StartPosition { get { return startPosition; } set { startPosition = value; GeneratePaths(); } }
        public Vector2 EndPosition { get { return endPosition; } set { endPosition = value; GeneratePaths(); } }
        public float StartDegree { get { return startDegree; } set { startDegree = value; GeneratePaths(); } }
        public float EndDegree { get { return endDegree; } set { endDegree = value; GeneratePaths(); } }

        public readonly List<WordData> Words;

        public WordCloudData(Vector2 startPosition, float startDegree, Vector2 endPosition, float endDegree, List<String> words)
        {
            this.startDegree = startDegree;
            this.endDegree = endDegree;
            this.startPosition = startPosition;
            this.endPosition = endPosition;

            Words = new List<WordData>();
            foreach (String word in words)
            {
                Words.Add(new WordData(this, word));
            }

            GeneratePaths();
        }

        private void GeneratePaths()
        {
            foreach (WordData word in Words)
            {
                word.GeneratePath();
            }
        }
    }
}
