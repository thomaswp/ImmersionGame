namespace LevelEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxWorld = new System.Windows.Forms.PictureBox();
            this.trackBarDegree = new System.Windows.Forms.TrackBar();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.nudDegree = new System.Windows.Forms.NumericUpDown();
            this.listBoxSegues = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxProperties = new System.Windows.Forms.ListBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDegree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDegree)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxWorld
            // 
            this.pictureBoxWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxWorld.Location = new System.Drawing.Point(0, 76);
            this.pictureBoxWorld.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxWorld.Name = "pictureBoxWorld";
            this.pictureBoxWorld.Size = new System.Drawing.Size(675, 479);
            this.pictureBoxWorld.TabIndex = 0;
            this.pictureBoxWorld.TabStop = false;
            this.pictureBoxWorld.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWorld_MouseDown);
            this.pictureBoxWorld.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWorld_MouseMove);
            this.pictureBoxWorld.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWorld_MouseUp);
            // 
            // trackBarDegree
            // 
            this.trackBarDegree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDegree.Location = new System.Drawing.Point(0, 32);
            this.trackBarDegree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarDegree.Maximum = 359;
            this.trackBarDegree.Name = "trackBarDegree";
            this.trackBarDegree.Size = new System.Drawing.Size(805, 56);
            this.trackBarDegree.TabIndex = 1;
            this.trackBarDegree.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // listBoxAction
            // 
            this.listBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.ItemHeight = 16;
            this.listBoxAction.Items.AddRange(new object[] {
            "Move",
            "Select/Move",
            "New Platform",
            "New Segue"});
            this.listBoxAction.Location = new System.Drawing.Point(683, 97);
            this.listBoxAction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(212, 116);
            this.listBoxAction.TabIndex = 2;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxActions_SelectedIndexChanged);
            // 
            // nudDegree
            // 
            this.nudDegree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDegree.Location = new System.Drawing.Point(813, 32);
            this.nudDegree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudDegree.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.nudDegree.Name = "nudDegree";
            this.nudDegree.Size = new System.Drawing.Size(68, 22);
            this.nudDegree.TabIndex = 3;
            this.nudDegree.ValueChanged += new System.EventHandler(this.nudDegree_ValueChanged);
            // 
            // listBoxSegues
            // 
            this.listBoxSegues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSegues.FormattingEnabled = true;
            this.listBoxSegues.ItemHeight = 16;
            this.listBoxSegues.Items.AddRange(new object[] {
            "Linear",
            "Curved"});
            this.listBoxSegues.Location = new System.Drawing.Point(683, 238);
            this.listBoxSegues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxSegues.Name = "listBoxSegues";
            this.listBoxSegues.Size = new System.Drawing.Size(213, 100);
            this.listBoxSegues.TabIndex = 4;
            this.listBoxSegues.Visible = false;
            this.listBoxSegues.SelectedIndexChanged += new System.EventHandler(this.listBoxSegues_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(683, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Segues";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(683, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Actions";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(683, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Properties";
            // 
            // listBoxProperties
            // 
            this.listBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxProperties.FormattingEnabled = true;
            this.listBoxProperties.ItemHeight = 16;
            this.listBoxProperties.Location = new System.Drawing.Point(685, 359);
            this.listBoxProperties.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxProperties.Name = "listBoxProperties";
            this.listBoxProperties.Size = new System.Drawing.Size(209, 116);
            this.listBoxProperties.TabIndex = 8;
            this.listBoxProperties.Visible = false;
            this.listBoxProperties.SelectedIndexChanged += new System.EventHandler(this.listBoxProperties_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Map Files|*.map";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Map Files|*.map";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.testToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(897, 28);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "menuStrip";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.tsmiSave});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 24);
            this.tsmiFile.Text = "File";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Size = new System.Drawing.Size(114, 24);
            this.tsmiNew.Text = "New";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(114, 24);
            this.tsmiOpen.Text = "Open";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(114, 24);
            this.tsmiSave.Text = "Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 567);
            this.Controls.Add(this.listBoxProperties);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxSegues);
            this.Controls.Add(this.nudDegree);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.pictureBoxWorld);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.trackBarDegree);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDegree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDegree)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWorld;
        private System.Windows.Forms.TrackBar trackBarDegree;
        private System.Windows.Forms.ListBox listBoxAction;
        private System.Windows.Forms.NumericUpDown nudDegree;
        private System.Windows.Forms.ListBox listBoxSegues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxProperties;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

