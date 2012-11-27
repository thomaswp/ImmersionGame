using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Immersion;
using XNAPoint = Microsoft.Xna.Framework.Point;

namespace LevelEditor
{
    public partial class PlatformDialog : Form
    {
        public PlatformData PlatformData { get; set; }
        public MapData MapData { get; set; }

        private Point selection;
        private int size = 100;
        private List<Point> segments = new List<Point>();

        private Point offset;
        private Point startPan;
        private bool panning;

        public PlatformDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            PlatformData.Segments.Clear();
            foreach (Point point in segments)
            {
                PlatformData.Segments.Add(new XNAPoint(point.X, point.Y));
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void draw()
        {
            if (pictureBoxDisplay.Width == 0) return;
            Image bmp = pictureBoxDisplay.Image;
            if (bmp == null || bmp.Width != pictureBoxDisplay.Width || bmp.Height != pictureBoxDisplay.Height)
            {
                bmp = pictureBoxDisplay.Image = new Bitmap(pictureBoxDisplay.Width, pictureBoxDisplay.Height);
            }

            Graphics g = Graphics.FromImage(bmp);

            int width = bmp.Width, height = bmp.Height;

            g.Clear(Color.CornflowerBlue);
            g.DrawString("(" + offset.X + ", " + offset.Y + ")", new Font("Arial", 12), Brushes.Black, new PointF(0, 0));
            g.DrawString("(" + selection.X + ", " + selection.Y + ")", new Font("Arial", 12), Brushes.Black, new PointF(80, 0));


            Pen black = new Pen(Color.Black, 4);
            foreach (Point p in segments)
            {
                drawPlatform(g, IsoToImage(p), black);
            }

            if (!panning)
            {
                Point sPoint = IsoToImage(selection);
                if (segments.Contains(selection))
                {
                    drawPlatform(g, sPoint, new Pen(Color.Red, 3));
                }
                else
                {
                    drawPlatform(g, sPoint, new Pen(Color.Green, 3));
                }
            }

            this.pictureBoxDisplay.Refresh();
        }

        private void drawPlatform(Graphics g, Point p, Pen pen)
        {
            int r = size / 2;
            Point[] points = new Point[] {
                new Point(p.X - r, p.Y), new Point(p.X, p.Y + r),
                new Point(p.X + r, p.Y), new Point(p.X, p.Y - r),
                new Point(p.X - r, p.Y) };
            g.DrawLines(pen, points);
            g.FillPolygon(new SolidBrush(Color.FromArgb(100, Color.Black)), points);
        }

        private void PlatformDialogcs_Load(object sender, EventArgs e)
        {
            segments = new List<Point>();
            foreach (XNAPoint point in PlatformData.Segments)
            {
                segments.Add(new Point(point.X, point.Y));
            }

            draw();
        }

        private void pictureBoxDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                selection = ImageToIso(e.Location);
                if (segments.Contains(selection))
                {
                    segments.Remove(selection);
                }
                else
                {
                    segments.Add(selection);
                }
            }
            else
            {
                panning = true;
                startPan = e.Location;
                startPan.Offset(-offset.X, -offset.Y);
            }
        }

        private void pictureBoxDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            draw();
            panning = false;
        }

        private void pictureBoxDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning)
            {
                offset = e.Location;
                offset.Offset(-startPan.X, -startPan.Y);
            }
            else
            {
                selection = ImageToIso(e.Location);
            }
            draw();
        }

        private Point ImageToIso(Point pos)
        {
            pos.Offset(-offset.X - this.pictureBoxDisplay.Width / 2, -offset.Y - this.pictureBoxDisplay.Height / 2);
            double px = (double)pos.X / size, py = (double)pos.Y / size;
            double dx = py - px, dy = px + py;
            dx += 0.5 * Math.Sign(dx);
            dy += 0.5 * Math.Sign(dy);
            return new Point((int)dx, (int)dy);
        }

        private Point IsoToImage(Point pos)
        {
            int r = size / 2;
            Point p = new Point((pos.Y - pos.X) * r, (pos.X + pos.Y) * r);
            p.Offset(offset.X + this.pictureBoxDisplay.Width / 2, offset.Y + this.pictureBoxDisplay.Height / 2);
            return p;
        }

        private void PlatformDialogcs_Resize(object sender, EventArgs e)
        {
            draw();
        }
    }
}
