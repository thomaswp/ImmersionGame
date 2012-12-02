using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Immersion;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LevelEditor
{
    public class EditorState
    {
        public Point MapOffset;
        public MapData Map;
        public Size RenderSize;
        public int Degree;

        public PlatformData SelectedPlatform;
        public PlatformSegue SelectedSegue;
        public List<PlatformData> SelectedPlatforms = new List<PlatformData>();
        public List<PlatformSegue> SelectedSegues = new List<PlatformSegue>();

        public EditorState(MapData map, Size renderSize)
        {
            this.Map = map;
            this.RenderSize = renderSize;
        }

        public Vector2 MousePosOnMap(Point pos)
        {
            return new Vector2(MapOffset.X + pos.X - RenderSize.Width / 2,
                MapOffset.Y + pos.Y - RenderSize.Height / 2);
        }

        public Point MapPointOnCanvas(Vector2 pos)
        {
            pos -= PointToVec(MapOffset);
            pos += new Vector2(RenderSize.Width, RenderSize.Height) / 2;
            return VecToPoint(pos);
        }

        public static Point VecToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Vector2 PointToVec(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
