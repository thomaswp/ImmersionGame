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
using System.Diagnostics;
using System.Threading;

namespace LevelEditor
{
    public partial class Form1 : Form
    {

        private int Degree
        {
            get { return editorState.Degree; }
            set { editorState.Degree = value; }
        }

        EditorState editorState;
        UIHandler uiHandler;
        MapRenderer mapRenderer;
        List<Process> processes = new List<Process>();

        public Form1()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.pictureBoxWorld.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            editorState = new EditorState(new MapData(), this.pictureBoxWorld.Size);
            uiHandler = new UIHandler(editorState);
            mapRenderer = new MapRenderer(editorState);

            String lastSave = Properties.Settings.Default.lastSave;
            if (File.Exists(lastSave))
            {
                LoadGame(lastSave);
            }
            else
            {
                NewGame(); 
            }
        }

        private void LoadGame(String game)
        {
            try
            {
                editorState.Map = MapData.ReadFromFile(game);
                this.Text = game.Split('\\').Last();
                Properties.Settings.Default.lastSave = game;
                Properties.Settings.Default.Save();
                draw();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                NewGame();
            }
        }

        private void NewGame()
        {
            editorState.Map = new MapData();
            editorState.Map.Platforms.Add(new PlatformData(new Vector2(0, 0)));
            editorState.Map.Platforms[0].SafePlatform = true;
            draw();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBarDegree.Value != nudDegree.Value)
            {
                nudDegree.Value = trackBarDegree.Value;
                Degree = trackBarDegree.Value;
            }
            draw();
        }

        private void nudDegree_ValueChanged(object sender, EventArgs e)
        {
            trackBarDegree.Value = (int)nudDegree.Value;
            Degree = trackBarDegree.Value;
            draw();
        }

        private void draw()
        {
            if (pictureBoxWorld.Width == 0) return;
            if (editorState == null) return;
            Image bmp = pictureBoxWorld.Image;
            if (bmp == null || bmp.Width != pictureBoxWorld.Width || bmp.Height != pictureBoxWorld.Height)
            {
                bmp = pictureBoxWorld.Image = new Bitmap(pictureBoxWorld.Width, pictureBoxWorld.Height);
                editorState.RenderSize = pictureBoxWorld.Size;
            }
            mapRenderer.Draw(bmp);
            pictureBoxWorld.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            draw();
        }

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            uiHandler.MouseWheel(e);
            draw();
        }

        private void pictureBoxWorld_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxWorld.Select();
            uiHandler.MouseDown(e);
            listBoxProperties.Visible = editorState.SelectedSegue != null;
            if (editorState.SelectedSegue != null)
            {
                if ((System.Type)listBoxProperties.Tag != editorState.SelectedSegue.GetType())
                {
                    listBoxProperties.Tag = editorState.SelectedSegue.GetType();
                    listBoxProperties.Items.Clear();
                    listBoxProperties.Items.AddRange(editorState.SelectedSegue.GetProperties());
                }
            }
            draw();
        }


        private void pictureBoxWorld_MouseUp(object sender, MouseEventArgs e)
        {
            uiHandler.MouseUp(e);
            draw();
        }

        private void pictureBoxWorld_MouseMove(object sender, MouseEventArgs e)
        {
            uiHandler.MouseMove(e);
            draw();
        }

        private void listBoxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiHandler.CurrentActionType = (UIHandler.Actions)listBoxAction.SelectedIndex;
            listBoxSegues.Visible = uiHandler.CurrentActionType == UIHandler.Actions.Segue;
        }

        private void listBoxSegues_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiHandler.CurrentSegueType = (UIHandler.Segues)listBoxSegues.SelectedIndex;
        }

        private void listBoxProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiHandler.CurrentPorpertiesIndex = listBoxProperties.SelectedIndex;
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadGame(openFileDialog.FileName);
                Vector2 offset = Vector2.Zero;
                foreach (PlatformData platform in editorState.Map.Platforms)
                {
                    platform.StartPos += offset;
                    foreach (PlatformSegue segue in platform.segues)
                    {
                        segue.Destination += offset;
                    }
                }
                draw();
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editorState.Map.WriteToFile(saveFileDialog.FileName);
                Properties.Settings.Default.lastSave = saveFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            uiHandler.OnKeyDown(e);
            draw();
        }

        private void pictureBoxWorld_Click(object sender, EventArgs e)
        {
            uiHandler.OnDoubleClick(e);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            dir += @"\Immersion\Immersion\bin\x86\Debug";
            ProcessStartInfo psi = new ProcessStartInfo(dir + @"\Immersion.exe");
            psi.WorkingDirectory = dir;
            editorState.Map.WriteToFile("testing.map");
            psi.Arguments = Directory.GetCurrentDirectory() + @"\testing.map";
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            Process p = Process.Start(psi);

            ParameterizedThreadStart ts = new ParameterizedThreadStart(debugTest);
            Thread t = new Thread(ts);
            processes.Add(p);
            t.Start(p);
        }

        private void debugTest(object obj)
        {
            Process p = (Process)obj;
            while (p.StandardError.Peek() != -1)
            {
                Console.WriteLine(p.StandardError.ReadLine());
            }
            processes.Remove(p);
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process p in processes)
            {
                p.Kill();
            }
        }


    }
}
