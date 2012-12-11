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
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LevelEditor
{
    public partial class PlatformDialog : Form
    {
        public EditorState EditorState { get; set; }

        private PlatformData PlatformData { get { return EditorState.SelectedPlatform; } }
        private MapData MapData { get { return EditorState.Map; } }

        private Point selection;
        private int size = 100;
        private List<Point> segments = new List<Point>();
        private List<WordCloudData> originalWordClouds = new List<WordCloudData>();
        private List<WordCloudData> wordClouds = new List<WordCloudData>();
        private WordCloudData editingCloud;
        private Point itemOffset;

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
            nudFallTime.Value = new Decimal(PlatformData.FallTime);
            checkBoxSafe.Checked = PlatformData.SafePlatform;
            checkBoxStart.Checked = PlatformData == MapData.startPlatform;
            checkBoxInvisible.Checked = PlatformData.Invisible;
            nudSpeed.Value = PlatformData.Repeats;
            checkBoxItem.Checked = PlatformData.Item != null;
            nudSlide.Value = new Decimal(PlatformData.Slide);
            checkBoxLaunch.Checked = PlatformData.Launch;
            if (PlatformData.Item != null)
            {
                textBoxItemName.Text = PlatformData.Item.Name;
                comboBoxItemTexture.Text = PlatformData.Item.ImageName;
                itemOffset = new Point(PlatformData.ItemOffset.X, PlatformData.ItemOffset.Y);
                checkBoxSlide.Checked = PlatformData.Item.EnablesSlide;
                checkBoxBlink.Checked = PlatformData.Item.EnablesBlink;
            }
            else
            {
                textBoxItemName.Text = "";
                comboBoxItemTexture.Text = "";
                itemOffset = new Point();
            }

            comboBoxNextLevel.Items.Clear();
            comboBoxNextLevel.Items.Add("None");
            foreach (MapData map in EditorState.Game.Maps)
            {
                comboBoxNextLevel.Items.Add(map.Name);
            }
            if (PlatformData.NextMap != null)
            {
                comboBoxNextLevel.SelectedIndex = EditorState.Game.Maps.IndexOf(PlatformData.NextMap) + 1;
            }
            else
            {
                comboBoxNextLevel.SelectedIndex = 0;
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            PlatformData.Segments.Clear();
            foreach (Point point in segments)
            {
                PlatformData.Segments.Add(new XNAPoint(point.X, point.Y));
            }
            PlatformData.FallTime = (int)nudFallTime.Value;
            PlatformData.Slide = (float)nudSlide.Value;
            PlatformData.SafePlatform = checkBoxSafe.Checked;
            PlatformData.Invisible = checkBoxInvisible.Checked;
            PlatformData.Launch = checkBoxLaunch.Checked;
            if (comboBoxNextLevel.SelectedIndex > 0)
            {
                PlatformData.NextMap = EditorState.Game.Maps[comboBoxNextLevel.SelectedIndex - 1];
            }
            if (checkBoxItem.Checked)
            {
                PlatformData.Item = new ItemData(textBoxItemName.Text, comboBoxItemTexture.Text);
                PlatformData.ItemOffset = new XNAPoint(itemOffset.X, itemOffset.Y);
                PlatformData.Item.EnablesSlide = checkBoxSlide.Checked;
                PlatformData.Item.EnablesBlink = checkBoxBlink.Checked;
            }
            else
            {
                PlatformData.Item = null;
                PlatformData.ItemOffset = new XNAPoint();
            }

            if (checkBoxStart.Checked)
            {
                MapData.startPlatform = PlatformData;
            }
            else if (MapData.startPlatform == PlatformData)
            {
                MapData.startPlatform = null;
            }
            PlatformData.Repeats = (int)nudSpeed.Value;

            saveWordCloud();
            foreach (WordCloudData wordCloud in originalWordClouds)
            {
                MapData.WordClouds.Remove(wordCloud);
            }
            MapData.WordClouds.AddRange(wordClouds);
            this.Close();
        }

        private void UpdateWordClouds()
        {
            bool enabled = wordClouds.Count > 0;

            if (!enabled)
            {
                comboBoxWordClouds.Text = "";
                nudFrom.Value = 0;
                nudTo.Value = 0;
                nudOffsetX.Value = 0;
                nudOffsetY.Value = 0;
                textBoxWords.Clear();
                nudWordOffset.Value = 0;
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
            nudOffsetX.Enabled = enabled;
            nudOffsetY.Enabled = enabled;
            textBoxWords.Enabled = enabled;
            comboBoxWordClouds.Enabled = enabled;
            buttonDelete.Enabled = enabled;
            buttonOnPlatform.Enabled = enabled;
            nudWordOffset.Enabled = enabled;
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

            if (checkBoxItem.Checked)
            {
                Point sPoint = IsoToImage(itemOffset);
                g.DrawEllipse(new Pen(Color.Black), new Rectangle(sPoint.X - 30, sPoint.Y - 30, 60, 60));
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
                if (Control.ModifierKeys == Keys.Control && checkBoxItem.Checked)
                {
                    itemOffset = ImageToIso(e.Location);
                }
                else
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
            if (comboBoxWordClouds.SelectedIndex >= 0 && comboBoxWordClouds.SelectedIndex < wordClouds.Count)
            {
                editingCloud = wordClouds[comboBoxWordClouds.SelectedIndex];
                nudFrom.Value = new Decimal(editingCloud.StartDegree);
                nudTo.Value = new Decimal(editingCloud.EndDegree);
                nudOffsetX.Value = new Decimal(editingCloud.Center.X);
                nudOffsetY.Value = new Decimal(editingCloud.Center.Y);
                nudWordOffset.Value = new Decimal(editingCloud.WordOffset);
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
                editingCloud.Center = new Microsoft.Xna.Framework.Vector2(
                    (float)nudOffsetX.Value, (float)nudOffsetY.Value);
                editingCloud.Words.Clear();
                foreach (String line in textBoxWords.Lines)
                {
                    editingCloud.Words.Add(new WordData(editingCloud, line));
                }
                editingCloud.GeneratePaths();
                editingCloud.WordOffset = (float)nudWordOffset.Value;


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

        private void buttonOnPlatform_Click(object sender, EventArgs e)
        {
            nudOffsetX.Value = new Decimal(PlatformData.StartPos.X);
            nudOffsetY.Value = new Decimal(PlatformData.StartPos.Y);
        }

        private void checkBoxItem_CheckedChanged(object sender, EventArgs e)
        {
            bool check = this.checkBoxItem.Checked;
            this.textBoxItemName.Enabled = check;
            this.comboBoxItemTexture.Enabled = check;
            this.checkBoxSlide.Enabled = check;
            this.checkBoxBlink.Enabled = check;
            draw();
        }

    }
}
