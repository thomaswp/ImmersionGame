using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Immersion;
using System.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LevelEditor
{
    public class MapRenderer
    {

        public const int SEGUE_DRAW_RAD = 10;

        private EditorState editorState;
        private int Degree { get { return editorState.Degree; } }

        public MapRenderer(EditorState editorState)
        {
            this.editorState = editorState;
        }

        public void Draw(Image bmp)
        {
            Graphics g = Graphics.FromImage(bmp);

            int width = bmp.Width, height = bmp.Height;

            Point mapOffset = editorState.MapOffset;
            MapData map = editorState.Map;

            g.Clear(Color.CornflowerBlue);
            g.DrawString(Degree.ToString(), new Font("Arial", 12), Brushes.Black, new PointF(0, 0));
            g.DrawString("(" + mapOffset.X + ", " + mapOffset.Y + ")", new Font("Arial", 12), Brushes.Black, new PointF(50, 0));
            
            foreach (WordCloudData wordCloud in map.WordClouds)
            {
                DrawWordCloud(g, wordCloud);
            }

            foreach (PlatformData platform in map.Platforms)
            {
                DrawPlatform(g, platform);
            }
        }

        private void DrawPath(Graphics g, IPathed pathed, Pen pen)
        {
            for (int i = 0; i < 360; i += 6)
            {
                Point p1 = MapPointOnCanvas(pathed.GetPosition(i));
                Point p2 = MapPointOnCanvas(pathed.GetPosition(i + 2));
                g.DrawLine(pen, p1, p2);
            }
        }

        private void DrawPlatform(Graphics g, PlatformData platform)
        {
            Vector2 pos = platform.GetPosition(Degree);
            Point canvasPos = MapPointOnCanvas(pos);

            Pen pen = editorState.SelectedPlatform == platform ? Pens.Red : Pens.Black;

            DrawPath(g, platform, pen);

            DrawSegue(g, platform.StartSegue, pen);

            List<PlatformSegue> segs = new List<PlatformSegue>(platform.segues);
            segs.Insert(0, platform.StartSegue);
            foreach (PlatformSegue segue in segs)
            {
                DrawSegue(g, segue, pen);
            }
            pen = new Pen(pen.Color, 4);
            g.DrawEllipse(pen, new Rectangle(canvasPos.X - 25, canvasPos.Y - 25, 50, 50));
        }

        private void DrawSegue(Graphics g, PlatformSegue segue, Pen pen)
        {
            Point sPos = MapPointOnCanvas(segue.Destination);
            if (segue == editorState.SelectedSegue)
            {
                pen = Pens.Red;
            }
            g.DrawEllipse(pen, new Rectangle(sPos.X - 10, sPos.Y - 10, 20, 20));
        }

        private void DrawWordCloud(Graphics g, WordCloudData wordCloud)
        {
            Font smallFont = new Font("Arial", 7);
            Pen linePen = new Pen(Color.DarkBlue, 5);
            g.DrawLine(linePen, MapPointOnCanvas(wordCloud.StartPosition),
                MapPointOnCanvas(wordCloud.EndPosition));

            Pen dashPen = Pens.Black;
            foreach (WordData word in wordCloud.Words)
            {
                for (int i = 0; i < word.points.Count; i++)
                {
                    int lastIndex = i == 0 ? word.points.Count - 1 : i - 1;
                    Point p1 = MapPointOnCanvas(word.points[lastIndex]);
                    Point p2 = MapPointOnCanvas(word.points[i]);
                    g.DrawEllipse(Pens.Black, new Rectangle(p1.X - 2, p1.Y - 2, 4, 4));
                }
                DrawPath(g, word, dashPen);

                g.DrawString(word.Text, smallFont, Brushes.Black,
                    MapPointOnCanvas(word.GetPosition(Degree)));
            }
        }

        private Vector2 MousePosOnMap(Point pos)
        {
            return editorState.MousePosOnMap(pos);
        }

        private Point MapPointOnCanvas(Vector2 pos)
        {
            return editorState.MapPointOnCanvas(pos);
        }
    }
}
