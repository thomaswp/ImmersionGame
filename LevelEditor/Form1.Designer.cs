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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDegree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDegree)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxWorld
            // 
            this.pictureBoxWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxWorld.Location = new System.Drawing.Point(0, 55);
            this.pictureBoxWorld.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxWorld.Name = "pictureBoxWorld";
            this.pictureBoxWorld.Size = new System.Drawing.Size(660, 441);
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
            this.trackBarDegree.Location = new System.Drawing.Point(0, 0);
            this.trackBarDegree.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarDegree.Maximum = 360;
            this.trackBarDegree.Name = "trackBarDegree";
            this.trackBarDegree.Size = new System.Drawing.Size(790, 56);
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
            this.listBoxAction.Location = new System.Drawing.Point(667, 64);
            this.listBoxAction.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(212, 116);
            this.listBoxAction.TabIndex = 2;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // nudDegree
            // 
            this.nudDegree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDegree.Location = new System.Drawing.Point(798, 15);
            this.nudDegree.Margin = new System.Windows.Forms.Padding(4);
            this.nudDegree.Maximum = new decimal(new int[] {
            360,
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
            this.listBoxSegues.Location = new System.Drawing.Point(667, 204);
            this.listBoxSegues.Name = "listBoxSegues";
            this.listBoxSegues.Size = new System.Drawing.Size(213, 100);
            this.listBoxSegues.TabIndex = 4;
            this.listBoxSegues.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Segues";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Actions";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(667, 307);
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
            this.listBoxProperties.Location = new System.Drawing.Point(670, 327);
            this.listBoxProperties.Name = "listBoxProperties";
            this.listBoxProperties.Size = new System.Drawing.Size(209, 116);
            this.listBoxProperties.TabIndex = 8;
            this.listBoxProperties.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 509);
            this.Controls.Add(this.listBoxProperties);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxSegues);
            this.Controls.Add(this.nudDegree);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.trackBarDegree);
            this.Controls.Add(this.pictureBoxWorld);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDegree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDegree)).EndInit();
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
    }
}

