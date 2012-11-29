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
        private List<WordCloudData> originalWordClouds = new List<WordCloudData>();
        private List<WordCloudData> wordClouds = new List<WordCloudData>();
        private WordCloudData editingCloud;

        private Point offset;
        private Point startPan;
        private bool panning;

        public PlatformDialog()
        {
            InitializeComponent();
        }

        private void PlatformDialogcs_Load(object sender, EventArgs e)
        {
            segments = new List<Point>();
            foreach (XNAPoint point in PlatformData.Segments)
            {
                segments.Add(new Point(point.X, point.Y));
            }

            editingCloud = null;
            originalWordClouds.Clear();
            wordClouds.Clear();
            foreach (WordCloudData wordCloud in MapData.WordClouds)
            {
                if (wordCloud.PathedObject == PlatformData)
                {
                    originalWordClouds.Add(wordCloud);
                    wordClouds.Add(new WordCloudData(wordCloud));
                }
            }
            UpdateWordClouds();

            if (wordClouds.Count > 0)
            {
                comboBoxWordClouds.SelectedIndex = 0;
            }
            else
            {
                comboBoxWordClouds.SelectedIndex = -1;
            }

            draw();
        }

        private void UpdateWordClouds()
        {
            bool enabled = wordClouds.Count > 0;

            if (!enabled)
            {
                comboBoxWordClouds.Text = "";
                nudFrom.Value = 0;
                nudTo.Value = 0;
                textBoxWords.Clear();
            }

            int originalIndex = comboBoxWordClouds.SelectedIndex;
            comboBoxWordClouds.Items.Clear();
            foreach (WordCloudData wordCloud in wordClouds)
            {
                comboBoxWordClouds.Items.Add(wordCloud.Name);
            }
            comboBoxWordClouds.SelectedIndex = Math.Min(originalIndex, comboBoxWordClouds.Items.Count - 1);

            nudFrom.Enabled = enabled;
            nudTo.Enabled = enabled;
            textBoxWords.Enabled = enabled;
            comboBoxWordClouds.Enabled = enabled;
            buttonDelete.Enabled = enabled;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            PlatformData.Segments.Clear();
            foreach (Point point in segments)
            {
                PlatformData.Segments.Add(new XNAPoint(point.X, point.Y));
            }
            saveWordCloud();
            foreach (WordCloudData wordCloud in originalWordClouds)
            {
                MapData.WordClouds.Remove(wordCloud);
            }
            MapData.WordClouds.AddRange(wordClouds);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveWordCloud();
            textBoxWords.Text = "";
            if (comboBoxWordClouds.SelectedIndex >= 0)
            {
                editingCloud = wordClouds[comboBoxWordClouds.SelectedIndex];
                nudFrom.Value = new Decimal(editingCloud.StartDegree);
                nudTo.Value = new Decimal(editingCloud.EndDegree);
                foreach (WordData word in editingCloud.Words)
                {
                    if (textBoxWords.Text != "")
                    {
                        textBoxWords.Text += "\r\n";
                    }
                    textBoxWords.Text += word.Text;
                }
            }
        }

        private void saveWordCloud()
        {
            if (editingCloud != null)
            {
                editingCloud.StartDegree = (float)nudFrom.Value;
                editingCloud.EndDegree = (float)nudTo.Value;
                editingCloud.Words.Clear();
                foreach (String line in textBoxWords.Lines)
                {
                    editingCloud.Words.Add(new WordData(editingCloud, line));
                }
                editingCloud.GeneratePaths();


                String name = editingCloud.Name;
                if ((String)comboBoxWordClouds.Items[wordClouds.IndexOf(editingCloud)] != name)
                {
                    comboBoxWordClouds.Items[wordClouds.IndexOf(editingCloud)] = name;
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            wordClouds.Remove(editingCloud);
            editingCloud = null;
            UpdateWordClouds();
        }

        private void buttonAddWordCloud_Click(object sender, EventArgs e)
        {
            wordClouds.Add(new WordCloudData(PlatformData, 0, 10, new List<string>()));
            UpdateWordClouds();
            comboBoxWordClouds.SelectedIndex = comboBoxWordClouds.Items.Count - 1;
        }

        private void textBoxWords_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
