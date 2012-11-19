using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Immersion;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System.IO;

namespace LevelEditor
{
    public partial class Form1 : Form
    {

        const int SEGUE_RAD = 10;

        enum Actions 
        {
            Move, Select, Platform, Segue
        }

        enum Segues
        {
            Linear, Curved
        }

        private int Degree { get { return this.trackBarDegree.Value; } }

        MapData map = new MapData();

        Point mapOffset, startDragMouse, startDragMap;
        bool draggingMap;


        PlatformData draggingPlatform, selectedPlatform;
        Vector2 draggingItemOffset;

        PlatformSegue draggingSegue, selectedSegue;

        public Form1()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.pictureBoxWorld.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        List<String> words;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            map.Platforms.Add(new PlatformData(new Vector2(0, 0)));
            words = new List<string>();
            String stuff = "This is a public service announcement for the sake of a new moon on the shores of Albermarle county";
            String[] stuffA = stuff.Split(new char[] { ' ' });
            words.Add("Hello");
            words.Add("how");
            words.Add("are");
            words.Add("you");
            words.Add("Hello");
            ////map.WordClouds.Add(new WordCloudData(new Vector2(300, 80), 50, new Vector2(100, 200), 90, words));
            //map.WordClouds.Add(new WordCloudData(new Vector2(100, 200), 50, new Vector2(300, 80), 90, words));
            //Random rand = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    List<String> ws = new List<string>();
            //    for (int j = 0; rand.NextDouble() < 0.75; j++)
            //    {
            //        ws.Add(stuffA[rand.Next(stuffA.Length)]);
            //    }
            //    int time1 = rand.Next(330);
            //    int time2 = rand.Next(360 - time1) + time1;
            //    int w = 700;
            //    int dw = -w / 2;
            //    map.WordClouds.Add(new WordCloudData(new Vector2(dw + rand.Next(w), dw + rand.Next(w)), time1, new Vector2(dw + rand.Next(w), dw + rand.Next(w)), time2, ws));
            //}
            draw();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            if (trackBarDegree.Value != nudDegree.Value)
            {
                nudDegree.Value = trackBarDegree.Value;
            }
            draw();
        }

        private void draw()
        {
            if (pictureBoxWorld.Width == 0) return;
            Image bmp = pictureBoxWorld.Image;
            if (bmp == null || bmp.Width != pictureBoxWorld.Width || bmp.Height != pictureBoxWorld.Height)
            {
                bmp = pictureBoxWorld.Image = new Bitmap(pictureBoxWorld.Width, pictureBoxWorld.Height);
            }
            Graphics g = Graphics.FromImage(bmp);

            int degree = trackBarDegree.Value;
            int width = bmp.Width, height = bmp.Height;

            g.Clear(Color.CornflowerBlue);
            g.DrawString(degree.ToString(), new Font("Arial", 12), Brushes.Black, new PointF(0, 0));
            g.DrawString("(" + mapOffset.X + ", " + mapOffset.Y + ")", new Font("Arial", 12), Brushes.Black, new PointF(50, 0));

            Font smallFont = new Font("Arial", 7);

            foreach (WordCloudData wordCloud in map.WordClouds)
            {
                Pen p = new Pen(Color.DarkBlue, 5);
                g.DrawLine(p, mapPointOnCanvas(wordCloud.StartPosition), 
                    mapPointOnCanvas(wordCloud.EndPosition));
                foreach (WordData word in wordCloud.Words)
                {
                    for (int i = 0; i < word.points.Count; i++)
                    {
                        int lastIndex = i == 0 ? word.points.Count - 1 : i - 1;
                        Point p1 = mapPointOnCanvas(word.points[lastIndex]);
                        Point p2 = mapPointOnCanvas(word.points[i]);
                        g.DrawEllipse(Pens.Black, new Rectangle(p1.X - 2, p1.Y - 2, 4, 4));
                    }
                    for (int i = 0; i < 360; i += 3)
                    {
                        Point p1 = mapPointOnCanvas(word.GetPosition(i));
                        Point p2 = mapPointOnCanvas(word.GetPosition(i + 1));
                        g.DrawLine(Pens.Black, p1, p2);
                    }

                    g.DrawString(word.Text, smallFont, Brushes.Black, 
                        mapPointOnCanvas(word.GetPosition(Degree)));
                }
            }

            foreach (PlatformData platform in map.Platforms)
            {
                Vector2 pos = platform.GetPosition(degree);
                Point canvasPos = mapPointOnCanvas(pos);

                Pen pen = selectedPlatform == platform ? Pens.Red : Pens.Black;
                for (int i = 0; i < 360; i += 6)
                { 
                    Point p1 = mapPointOnCanvas(platform.GetPosition(i));
                    Point p2 = mapPointOnCanvas(platform.GetPosition(i + 2));
                    g.DrawLine(pen, p1, p2);
                }
                Point startPos = mapPointOnCanvas(platform.StartPos);
                g.DrawEllipse(pen, new Rectangle(startPos.X - SEGUE_RAD, startPos.Y - SEGUE_RAD, 
                    SEGUE_RAD * 2, SEGUE_RAD * 2));

                List<PlatformSegue> segs = new List<PlatformSegue>(platform.segues);
                segs.Insert(0, platform.StartSegue);
                foreach (PlatformSegue segue in segs)
                {
                    Point sPos = mapPointOnCanvas(segue.Destination);
                    Pen sPen = pen;
                    if (segue == selectedSegue)
                    {
                        sPen = Pens.Red;
                    }
                    g.DrawEllipse(sPen, new Rectangle(sPos.X - 10, sPos.Y - 10, 20, 20));
                }
                pen = new Pen(pen.Color, 4);
                g.DrawEllipse(pen, new Rectangle(canvasPos.X - 25, canvasPos.Y - 25, 50, 50));
            }

            pictureBoxWorld.Refresh();
        }

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (selectedSegue != null)
            {
                selectedSegue.ChangeProperty(listBoxProperties.SelectedIndex, e.Delta);
                draw();
            }
        }

        private void nudDegree_ValueChanged(object sender, EventArgs e)
        {
           trackBarDegree.Value = (int)nudDegree.Value;
           draw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            draw();
        }

        private void pictureBoxWorld_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxWorld.Select();

            Actions action = (Actions)listBoxAction.SelectedIndex;
            Vector2 pos = mousePosOnMap(e.Location);
            int degree = trackBarDegree.Value;

            if (action == Actions.Move)
            {
                startMapDrag(e);
            }
            else if (action == Actions.Platform)
            {
                draggingPlatform = new PlatformData(pos, degree);
                map.WordClouds.Add(new WordCloudData(draggingPlatform, 30, 120, words));
                draggingItemOffset = new Vector2();
                map.Platforms.Add(draggingPlatform);
                draw();
            }
            else if (action == Actions.Select)
            {
                selectedPlatform = null;
                selectedSegue = null;

                foreach (PlatformData platform in map.Platforms)
                {
                    if (platform.contains(pos, degree))
                        selectedPlatform = platform;
                }
                draggingPlatform = selectedPlatform;
                if (draggingPlatform != null)
                {
                    draggingItemOffset = selectedPlatform.GetPosition(degree) - pos;
                }

                if (draggingPlatform == null)
                {
                    foreach (PlatformData platform in map.Platforms)
                    {
                        if (segueContains(platform.StartSegue, pos))
                        {
                            selectedSegue = platform.StartSegue;
                            break;
                        }
                        foreach (PlatformSegue segue in platform.segues)
                        {
                            if (segueContains(segue, pos))
                            {
                                selectedSegue = segue;
                                break;
                            }
                        }
                    }
                }
                listBoxProperties.Visible = selectedSegue != null;
                if (selectedSegue != null)
                {
                    if ((System.Type)listBoxProperties.Tag != selectedSegue.GetType())
                    {
                        listBoxProperties.Tag = selectedSegue.GetType();
                        listBoxProperties.Items.Clear();
                        listBoxProperties.Items.AddRange(selectedSegue.GetProperties());
                    }
                }

                draggingSegue = selectedSegue;
                if (draggingSegue != null)
                {
                    draggingItemOffset = selectedSegue.Destination - pos;
                }

                if (selectedPlatform == null && selectedSegue == null)
                {
                    startMapDrag(e);
                }

                draw();
            }
            else if (action == Actions.Segue)
            {
                if (selectedPlatform == null) return;
                PlatformSegue segue = null;

                Segues type = (Segues)listBoxSegues.SelectedIndex;

                if (type == Segues.Linear)
                {
                    segue = new PlatformSegueLinear(pos);
                } else if (type == Segues.Curved)
                {
                    segue = new PlatformSegueCurved(pos);
                }

                if (segue == null) return;

                selectedPlatform.segues.Add(segue);
                draggingSegue = segue;
                draggingItemOffset = new Vector2();

                draw();
            }
        }

        private void startMapDrag(MouseEventArgs e)
        {
            draggingMap = true;
            startDragMouse = new Point(e.X, e.Y);
            startDragMap = mapOffset;
        }

        private void pictureBoxWorld_MouseUp(object sender, MouseEventArgs e)
        {
            draggingMap = false;
            draggingPlatform = null;
            draggingSegue = null;
        }

        private void pictureBoxWorld_MouseMove(object sender, MouseEventArgs e)
        {
            Vector2 pos = mousePosOnMap(e.Location);
            if (draggingMap)
            {
                mapOffset.X = startDragMap.X - (e.X - startDragMouse.X);
                mapOffset.Y = startDragMap.Y - (e.Y - startDragMouse.Y);
                draw();
            }
            if (draggingPlatform != null)
            {
                if (draggingPlatform.segues.Count == 0)
                {
                    draggingPlatform.StartPos = pos + draggingItemOffset;
                }
                else
                {
                    updatePlatformOffset(selectedPlatform, Degree, mousePosOnMap(e.Location));
                    //updatePlatformWeights(Degree, mousePosOnMap(e.Location));
                }
                draw();
            }
            if (draggingSegue != null)
            {
                draggingSegue.Destination = pos + draggingItemOffset;
                draw();
            }
        }

        private void updatePlatformOffset(PlatformData platform, int degree, Vector2 pos)
        {
            float desiredDegree = 0;
            float minDis = float.MaxValue;
            for (float deg = 0; deg < 360; deg += 0.1f)
            {
                float dis = (platform.GetPosition(deg) - pos).Length();
                if (dis < minDis)
                {
                    minDis = dis;
                    desiredDegree = deg;
                }
            }

            platform.DegreeOffset += desiredDegree - degree;
            platform.DegreeOffset = (platform.DegreeOffset + 360) % 360;
        }

        private void updatePlatformWeights(int degree, Vector2 pos)
        {
            if (degree == 0) return;
            if (selectedPlatform.segues.Count < 2) return;

            int segueIndex = selectedPlatform.GetCurrentSegueIndex(degree);
            if (segueIndex < 0) return;
            PlatformSegue segue = selectedPlatform.segues[segueIndex];
            Vector2 start = segueIndex == 0 ? selectedPlatform.StartPos : 
                selectedPlatform.segues[segueIndex - 1].Destination;

            float desiredPerc = 0;
            float minDis = float.MaxValue;
            for (float perc = 0; perc < 1; perc += 0.01f)
            {
                float dis = (segue.GetPosition(start, perc) - pos).Length();
                if (dis < minDis)
                {
                    minDis = dis;
                    desiredPerc = perc;
                }
            }

            float degreePerc = degree / 360f;

            if (degreePerc == desiredPerc) return;
            
            float weightBefore = 0, weightAfter = 0;
            for (int i = 0; i < selectedPlatform.segues.Count; i++)
            {
                if (i < segueIndex)
                {
                    weightBefore += selectedPlatform.segues[i].Weight;
                }
                if (i > segueIndex)
                {
                    weightAfter += selectedPlatform.segues[i].Weight;
                }
            }

            float weight = degreePerc * (weightBefore + weightAfter) - weightBefore;
            weight /= desiredPerc - degreePerc;

            if (weight <= 0 || float.IsNaN(weight)) return;

            Console.WriteLine(weight);

            segue.Weight = weight;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Actions action = (Actions)listBoxAction.SelectedIndex;
            listBoxSegues.Visible = action == Actions.Segue;
        }

        private Vector2 mousePosOnMap(Point pos)
        {
            return new Vector2(mapOffset.X + pos.X - pictureBoxWorld.Width / 2, 
                mapOffset.Y + pos.Y - pictureBoxWorld.Height / 2);
        }

        private Point mapPointOnCanvas(Vector2 pos)
        {
            pos -= pointToVec(mapOffset);
            pos += new Vector2(pictureBoxWorld.Width / 2, pictureBoxWorld.Height / 2);
            return vecToPoint(pos);
        }

        private Point vecToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        private Vector2 pointToVec(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        private bool segueContains(PlatformSegue segue, Vector2 pos)
        {
            return (segue.Destination - pos).Length() < SEGUE_RAD;
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                map = MapData.ReadFromFile(openFileDialog.FileName);
                draw();
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                map.WriteToFile(saveFileDialog.FileName);
            }
        }

    }
}
