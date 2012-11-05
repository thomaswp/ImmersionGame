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

        enum Actions 
        {
            Move, Platform
        }

        MapData map = new MapData();
        int mapX, mapY;
        int startDragMouseX, startDragMouseY;
        int startDragMapX, startDragMapY;
        bool dragging;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            map.Platforms.Add(new PlatformData(0, 0));
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
            g.DrawString("(" + mapX + ", " + mapY + ")", new Font("Arial", 12), Brushes.Black, new PointF(50, 0));

            foreach (PlatformData platform in map.Platforms)
            {
                Vector2 pos = platform.getPosition(degree);
                pos -= new Microsoft.Xna.Framework.Vector2(mapX, mapY);
                pos += new Microsoft.Xna.Framework.Vector2(width / 2, height / 2);
                g.DrawEllipse(Pens.Black, new Rectangle((int)pos.X - 25, (int)pos.Y - 25, 50, 50));
            }

            pictureBoxWorld.Refresh();
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
            Actions action = (Actions)listBoxAction.SelectedIndex;

            if (action == Actions.Move)
            {
                dragging = true;
                startDragMouseX = e.X;
                startDragMouseY = e.Y;
                startDragMapX = mapX;
                startDragMapY = mapY;
            }
            else if (action == Actions.Platform)
            {
                Point pos = mousePosOnMap(e.Location.X, e.Location.Y);
                map.Platforms.Add(new PlatformData(pos.X, pos.Y, trackBarDegree.Value));
                draw();
            }
        }

        private void pictureBoxWorld_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBoxWorld_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                mapX = startDragMapX - (e.X - startDragMouseX);
                mapY = startDragMapY - (e.Y - startDragMouseY);
                draw();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private Point mousePosOnMap(int x, int y)
        {
            return new Point(mapX + x - pictureBoxWorld.Width / 2, 
                mapY + y - pictureBoxWorld.Height / 2);
        }

    }
}
