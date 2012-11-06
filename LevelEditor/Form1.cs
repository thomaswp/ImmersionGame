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


        private void Form1_Load_1(object sender, EventArgs e)
        {
            map.Platforms.Add(new PlatformData(new Vector2(0, 0)));
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

            foreach (PlatformData platform in map.Platforms)
            {
                Vector2 pos = platform.getPosition(degree);
                Point canvasPos = mapPointOnCanvas(pos);

                Pen pen = selectedPlatform == platform ? Pens.Red : Pens.Black;
                for (int i = 0; i < 360; i += 6)
                { 
                    Point p1 = mapPointOnCanvas(platform.getPosition(i));
                    Point p2 = mapPointOnCanvas(platform.getPosition(i + 2));
                    g.DrawLine(pen, p1, p2);
                }
                Point startPos = mapPointOnCanvas(platform.startPos);
                g.DrawEllipse(pen, new Rectangle(startPos.X - SEGUE_RAD, startPos.Y - SEGUE_RAD, 
                    SEGUE_RAD * 2, SEGUE_RAD * 2));
                foreach (PlatformSegue segue in platform.segues)
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
                selectedSegue.changeProperty(listBoxProperties.SelectedIndex, e.Delta);
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
                    draggingItemOffset = selectedPlatform.getPosition(degree) - pos;
                }

                if (draggingPlatform == null)
                {
                    foreach (PlatformData platform in map.Platforms)
                    {
                        foreach (PlatformSegue segue in platform.segues)
                        {
                            if (segueContains(segue, pos))
                            {
                                selectedSegue = segue;
                            }
                        }
                    }
                }
                listBoxProperties.Visible = selectedSegue != null;
                if (selectedSegue != null)
                {
                    if (listBoxProperties.Tag != selectedSegue.GetType())
                    {
                        listBoxProperties.Tag = selectedSegue.GetType();
                        listBoxProperties.Items.Clear();
                        listBoxProperties.Items.AddRange(selectedSegue.getProperties());
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
                draggingPlatform.startPos = pos + draggingItemOffset;
                draw();
            }
            if (draggingSegue != null)
            {
                draggingSegue.Destination = pos + draggingItemOffset;
                draw();
            }
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

    }
}
