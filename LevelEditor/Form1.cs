﻿using System;
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

        private int Degree
        {
            get { return editorState.Degree; }
            set { editorState.Degree = value; }
        }

        EditorState editorState;
        UIHandler uiHandler;
        MapRenderer mapRenderer;

        public Form1()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.pictureBoxWorld.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        List<String> words;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            editorState = new EditorState(new MapData(), this.pictureBoxWorld.Size);
            uiHandler = new UIHandler(editorState);
            mapRenderer = new MapRenderer(editorState);

            MapData map = editorState.Map;

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
                editorState.Map = MapData.ReadFromFile(openFileDialog.FileName);
                draw();
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editorState.Map.WriteToFile(saveFileDialog.FileName);
            }
        }

    }
}
