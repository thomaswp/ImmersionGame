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
            editorState = new EditorState(new GameData(), this.pictureBoxWorld.Size);
            uiHandler = new UIHandler(editorState);
            mapRenderer = new MapRenderer(editorState);

            String lastSave = Properties.Settings.Default.lastSave;
            if (File.Exists(lastSave))
            {
                LoadData(lastSave);
            }
            else
            {
                NewGame(); 
            }
        }

        private void LoadData(String game)
        {
            try
            {
                if (game.ToLower().EndsWith(".map"))
                {
                    MapData map = MapData.ReadFromFile(game);
                    if (map.Name == null) map.Name = "New Map";
                    editorState.Game = new GameData(map);
                }
                else
                {
                    editorState.Game = GameData.ReadFromFile(game);
                    if (editorState.Game.StartMap == null)
                    {
                        editorState.Game.StartMap = editorState.Game.Maps[0];
                    }
                }
                Properties.Settings.Default.lastSave = game;
                Properties.Settings.Default.Save();
                GameLoaded();

                for (float f = 170.8f; f < 171.2f; f += 0.1f)
                {
                    Vector2 pos = editorState.Map.WordClouds[0].Words[1].GetPosition(f % 360);
                    Console.WriteLine((f % 360) + ": " + pos);
                }
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                NewGame();
            }
        }

        private void LoadMapMenu()
        {
            tsmiSelectMap.DropDownItems.Clear();
            foreach (MapData map in editorState.Game.Maps)
            {
                tsmiSelectMap.DropDownItems.Add(map.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiSelectMap.DropDownItems[tsmiSelectMap.DropDownItems.Count - 1];
                tsmi.Checked = map == editorState.Map;
                tsmi.Tag = map;
                tsmi.Click += new EventHandler(tsmiMap_Click);
            }
        }

        void tsmiMap_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            editorState.Map = (MapData)tsmi.Tag;
            this.Text = editorState.Map.Name;
            MapLoaded();
        }

        private void NewGame()
        {
            editorState.Game = new GameData();
            GameLoaded();
        }

        private void GameLoaded()
        {
            MapLoaded();
        }

        private void MapLoaded()
        {
            LoadMapMenu();
            this.Text = editorState.Map.Name;
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
            if (editorState.SelectedPlatform != null)
            {
                labelSelected.Text = "Selected Platform: " + editorState.Map.Platforms.IndexOf(editorState.SelectedPlatform);
            }
            else if (editorState.SelectedSegue != null)
            {
                labelSelected.Text = "Selected Segue:\n" + editorState.SelectedSegue.GetType().Name;
            }
            else
            {
                labelSelected.Text = "";
            }

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
            //listBoxSegues.Visible = uiHandler.CurrentActionType == UIHandler.Actions.Segue;
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
            if (openGameDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadData(openGameDialog.FileName);
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (this.saveGameDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editorState.Game.WriteToFile(saveGameDialog.FileName);
                Properties.Settings.Default.lastSave = saveGameDialog.FileName;
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
            editorState.Game.WriteToFile("testing.game");
            psi.Arguments = Directory.GetCurrentDirectory() + @"\testing.game";
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

        private void tsmiNewLevel_Click(object sender, EventArgs e)
        {
            editorState.Map = new MapData();
            MapLoaded();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.openMapDialog.ShowDialog() == DialogResult.OK)
                {
                    MapData data = MapData.ReadFromFile(this.openMapDialog.FileName);
                    editorState.Map = data;
                    MapLoaded();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        private void tsmiExportMap_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveMapDialog.ShowDialog() == DialogResult.OK)
                {
                    editorState.Map.WriteToFile(this.saveMapDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        private MapDialog mapDialog = new MapDialog();
        private void tsmiEditLevel_Click(object sender, EventArgs e)
        {
            mapDialog.MapData = editorState.Map;
            mapDialog.GameData = editorState.Game;
            if (mapDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MapLoaded();
            }
        }
    }
}
