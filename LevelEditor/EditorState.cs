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
        private GameData myGame;

        public Point MapOffset;
        public GameData Game 
        { 
            get { return myGame; } 
            set { myGame = value; Reset(); } 
        }
        public MapData Map 
        { 
            get 
            {
                if (Game.LastEditedMap == null) Game.LastEditedMap = Game.StartMap;
                return Game.LastEditedMap; 
            } 
            set 
            {
                if (!myGame.Maps.Contains(value))
                {
                    myGame.Maps.Add(value);
                }
                myGame.LastEditedMap = value;
                Reset();
            } 
        }
        public Size RenderSize;
        public int Degree;

        public PlatformData SelectedPlatform;
        public PlatformSegue SelectedSegue;
        public WordCloudData SelectedWordCloud;
        public List<PlatformData> SelectedPlatforms = new List<PlatformData>();
        public List<PlatformSegue> SelectedSegues = new List<PlatformSegue>();
        public List<WordCloudData> SelectedWordClouds = new List<WordCloudData>();

        public EditorState(GameData game, Size renderSize)
        {
            Game = game;
            this.RenderSize = renderSize;
        }

        public void Reset()
        {
            SelectedPlatform = null;
            SelectedPlatforms.Clear();
            SelectedSegue = null;
            SelectedSegues.Clear();
            SelectedWordCloud = null;
            SelectedWordClouds.Clear();
            MapOffset = new Point();
            Degree = 0;
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
