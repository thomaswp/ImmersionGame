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
            this.pictureBoxWorld.Location = new System.Drawing.Point(0, 45);
            this.pictureBoxWorld.Name = "pictureBoxWorld";
            this.pictureBoxWorld.Size = new System.Drawing.Size(913, 790);
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
            this.trackBarDegree.Maximum = 360;
            this.trackBarDegree.Name = "trackBarDegree";
            this.trackBarDegree.Size = new System.Drawing.Size(1010, 45);
            this.trackBarDegree.TabIndex = 1;
            this.trackBarDegree.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // listBoxAction
            // 
            this.listBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Items.AddRange(new object[] {
            "Move",
            "Platform"});
            this.listBoxAction.Location = new System.Drawing.Point(919, 45);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(160, 95);
            this.listBoxAction.TabIndex = 2;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // nudDegree
            // 
            this.nudDegree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDegree.Location = new System.Drawing.Point(1016, 12);
            this.nudDegree.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudDegree.Name = "nudDegree";
            this.nudDegree.Size = new System.Drawing.Size(51, 20);
            this.nudDegree.TabIndex = 3;
            this.nudDegree.ValueChanged += new System.EventHandler(this.nudDegree_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 835);
            this.Controls.Add(this.nudDegree);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.trackBarDegree);
            this.Controls.Add(this.pictureBoxWorld);
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
    }
}

