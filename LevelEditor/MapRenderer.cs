using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Immersion;
using System.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using XNAPoint = Microsoft.Xna.Framework.Point;

namespace LevelEditor
{
    public class MapRenderer
    {

        public const int SEGUE_DRAW_RADIUS = 10;
        public const int CIRCLE_DRAW_RADIUS = 10;

        public const int SEGMENT_DRAW_SIZE = 47;
        public const float SEGMENT_RATIO = 9 / 14f;

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

            Pen pen = editorState.SelectedPlatforms.Contains(platform) ? Pens.DarkRed : Pens.Black;
            if (editorState.SelectedPlatform == platform) pen = Pens.Red;

            Pen circlePen = new Pen(pen.Color, 4);

            if (!platform.Invisible)
            {
                foreach (XNAPoint point in platform.Segments)
                {
                    DrawSegment(canvasPos, g, point);
                }
            }
            else
            {
                Color c = pen.Color;
                c = Color.FromArgb(100, c.R, c.G, c.B);
                pen = new Pen(c);
                circlePen = new Pen(c);
            }

            DrawPath(g, platform, pen);

            DrawSegue(g, platform.StartSegue, pen);

            List<PlatformSegue> segs = new List<PlatformSegue>(platform.segues);
            segs.Insert(0, platform.StartSegue);
            foreach (PlatformSegue segue in segs)
            {
                DrawSegue(g, segue, pen);
            }

            g.DrawEllipse(circlePen, new Rectangle(canvasPos.X - 25, canvasPos.Y - 25, 50, 50));
        }

        private void DrawSegment(Point center, Graphics g, XNAPoint point)
        {
            int rX = SEGMENT_DRAW_SIZE, rY = (int)(SEGMENT_DRAW_SIZE * SEGMENT_RATIO);
            Point p = new Point((point.Y - point.X) * rX, (point.X + point.Y) * rY);
            p.Offset(center);

            Point[] points = new Point[] {
                new Point(p.X - rX, p.Y), new Point(p.X, p.Y + rY),
                new Point(p.X + rX, p.Y), new Point(p.X, p.Y - rY),
                new Point(p.X - rX, p.Y) };
            g.DrawLines(Pens.Black, points);
            g.FillPolygon(new SolidBrush(Color.FromArgb(100, Color.Black)), points);
        }

        private void DrawSegue(Graphics g, PlatformSegue segue, Pen pen)
        {
            Point sPos = MapPointOnCanvas(segue.Destination);
            if (editorState.SelectedSegues.Contains(segue))
            {
                if (editorState.SelectedSegue == segue)
                {
                    pen = Pens.Red;
                }
                else
                {
                    pen = Pens.DarkRed;
                }
            }
            g.DrawEllipse(pen, new Rectangle(sPos.X - SEGUE_DRAW_RADIUS, 
                sPos.Y - SEGUE_DRAW_RADIUS, 
                SEGUE_DRAW_RADIUS * 2, SEGUE_DRAW_RADIUS * 2));
        }

        private void DrawWordCloud(Graphics g, WordCloudData wordCloud)
        {
            Font smallFont = new Font("Arial", 7);
            Pen linePen = new Pen(Color.DarkBlue, 5);
            Pen dashPen = Pens.Black;

            if (wordCloud.PathedObject.GetType() == typeof(CirclePath))
            {
                CirclePath path = (CirclePath)wordCloud.PathedObject;
                Point center = MapPointOnCanvas(path.center);
                g.DrawEllipse(dashPen, center.X - CIRCLE_DRAW_RADIUS, center.Y - CIRCLE_DRAW_RADIUS,
                    CIRCLE_DRAW_RADIUS * 2, CIRCLE_DRAW_RADIUS * 2);
            }
            else
            {
                g.DrawLine(linePen, MapPointOnCanvas(wordCloud.StartPosition),
                    MapPointOnCanvas(wordCloud.EndPosition));
                g.DrawRectangle(linePen, MapPointOnCanvas(wordCloud.StartPosition).X - 1f,
                    MapPointOnCanvas(wordCloud.StartPosition).Y - 1f, 2.5f, 2.5f);
                g.DrawRectangle(linePen, MapPointOnCanvas(wordCloud.EndPosition).X - 1f,
                    MapPointOnCanvas(wordCloud.EndPosition).Y - 1f, 2.5f, 2.5f);
            }

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
