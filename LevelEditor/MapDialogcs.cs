using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Immersion;

namespace LevelEditor
{
    public partial class MapDialog : Form
    {
        public GameData GameData { get; set; }
        public MapData MapData { get; set; }

        public MapDialog()
        {
            InitializeComponent();
        }

        private void MapDialogcs_Load(object sender, EventArgs e)
        {
            this.textBoxName.Text = MapData.Name;
            this.checkBoxStart.Checked = (GameData.StartMap == MapData);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            MapData.Name = this.textBoxName.Text;
            if (this.checkBoxStart.Checked)
            {
                GameData.StartMap = MapData;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
