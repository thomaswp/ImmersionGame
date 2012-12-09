namespace LevelEditor
{
    partial class PlatformDialog
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOnPlatform = new System.Windows.Forms.Button();
            this.nudOffsetY = new System.Windows.Forms.NumericUpDown();
            this.nudOffsetX = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxNextLevel = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxInvisible = new System.Windows.Forms.CheckBox();
            this.checkBoxStart = new System.Windows.Forms.CheckBox();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxSafe = new System.Windows.Forms.CheckBox();
            this.nudFallTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudTo = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudFrom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWords = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddWordCloud = new System.Windows.Forms.Button();
            this.comboBoxWordClouds = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.checkBoxItem = new System.Windows.Forms.CheckBox();
            this.labelItemName = new System.Windows.Forms.Label();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxItemTexture = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFallTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(1018, 544);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(905, 544);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(105, 28);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.89655F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.10345F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxDisplay, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1132, 536);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOnPlatform);
            this.panel1.Controls.Add(this.nudOffsetY);
            this.panel1.Controls.Add(this.nudOffsetX);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.nudTo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nudFrom);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxWords);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonAddWordCloud);
            this.panel1.Controls.Add(this.comboBoxWordClouds);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(647, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 530);
            this.panel1.TabIndex = 3;
            // 
            // buttonOnPlatform
            // 
            this.buttonOnPlatform.Location = new System.Drawing.Point(226, 290);
            this.buttonOnPlatform.Name = "buttonOnPlatform";
            this.buttonOnPlatform.Size = new System.Drawing.Size(94, 23);
            this.buttonOnPlatform.TabIndex = 14;
            this.buttonOnPlatform.Text = "On Platform";
            this.buttonOnPlatform.UseVisualStyleBackColor = true;
            this.buttonOnPlatform.Click += new System.EventHandler(this.buttonOnPlatform_Click);
            // 
            // nudOffsetY
            // 
            this.nudOffsetY.Location = new System.Drawing.Point(133, 290);
            this.nudOffsetY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOffsetY.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.nudOffsetY.Name = "nudOffsetY";
            this.nudOffsetY.Size = new System.Drawing.Size(64, 22);
            this.nudOffsetY.TabIndex = 13;
            // 
            // nudOffsetX
            // 
            this.nudOffsetX.Location = new System.Drawing.Point(59, 290);
            this.nudOffsetX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOffsetX.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.nudOffsetX.Name = "nudOffsetX";
            this.nudOffsetX.Size = new System.Drawing.Size(64, 22);
            this.nudOffsetX.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Offset:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBoxItemTexture);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBoxItemName);
            this.panel2.Controls.Add(this.labelItemName);
            this.panel2.Controls.Add(this.checkBoxItem);
            this.panel2.Controls.Add(this.comboBoxNextLevel);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.checkBoxInvisible);
            this.panel2.Controls.Add(this.checkBoxStart);
            this.panel2.Controls.Add(this.nudSpeed);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.checkBoxSafe);
            this.panel2.Controls.Add(this.nudFallTime);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(3, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(476, 202);
            this.panel2.TabIndex = 10;
            // 
            // comboBoxNextLevel
            // 
            this.comboBoxNextLevel.FormattingEnabled = true;
            this.comboBoxNextLevel.Location = new System.Drawing.Point(90, 82);
            this.comboBoxNextLevel.Name = "comboBoxNextLevel";
            this.comboBoxNextLevel.Size = new System.Drawing.Size(199, 24);
            this.comboBoxNextLevel.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Next Level:";
            // 
            // checkBoxInvisible
            // 
            this.checkBoxInvisible.AutoSize = true;
            this.checkBoxInvisible.Location = new System.Drawing.Point(253, 33);
            this.checkBoxInvisible.Name = "checkBoxInvisible";
            this.checkBoxInvisible.Size = new System.Drawing.Size(80, 21);
            this.checkBoxInvisible.TabIndex = 7;
            this.checkBoxInvisible.Text = "Invisible";
            this.checkBoxInvisible.UseVisualStyleBackColor = true;
            // 
            // checkBoxStart
            // 
            this.checkBoxStart.AutoSize = true;
            this.checkBoxStart.Location = new System.Drawing.Point(130, 33);
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.Size = new System.Drawing.Size(116, 21);
            this.checkBoxStart.TabIndex = 6;
            this.checkBoxStart.Text = "Start Platform";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            // 
            // nudSpeed
            // 
            this.nudSpeed.Location = new System.Drawing.Point(125, 55);
            this.nudSpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(46, 22);
            this.nudSpeed.TabIndex = 5;
            this.nudSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Speed Multiplier:";
            // 
            // checkBoxSafe
            // 
            this.checkBoxSafe.AutoSize = true;
            this.checkBoxSafe.Location = new System.Drawing.Point(9, 33);
            this.checkBoxSafe.Name = "checkBoxSafe";
            this.checkBoxSafe.Size = new System.Drawing.Size(115, 21);
            this.checkBoxSafe.TabIndex = 3;
            this.checkBoxSafe.Text = "Safe Platform";
            this.checkBoxSafe.UseVisualStyleBackColor = true;
            // 
            // nudFallTime
            // 
            this.nudFallTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudFallTime.Location = new System.Drawing.Point(113, 5);
            this.nudFallTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFallTime.Name = "nudFallTime";
            this.nudFallTime.Size = new System.Drawing.Size(80, 22);
            this.nudFallTime.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Fall Time (ms):";
            // 
            // nudTo
            // 
            this.nudTo.Location = new System.Drawing.Point(319, 264);
            this.nudTo.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.nudTo.Name = "nudTo";
            this.nudTo.Size = new System.Drawing.Size(55, 22);
            this.nudTo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "to";
            // 
            // nudFrom
            // 
            this.nudFrom.Location = new System.Drawing.Point(203, 264);
            this.nudFrom.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.nudFrom.Name = "nudFrom";
            this.nudFrom.Size = new System.Drawing.Size(63, 22);
            this.nudFrom.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Follows platform from degree:";
            // 
            // textBoxWords
            // 
            this.textBoxWords.Location = new System.Drawing.Point(6, 62);
            this.textBoxWords.Multiline = true;
            this.textBoxWords.Name = "textBoxWords";
            this.textBoxWords.Size = new System.Drawing.Size(473, 191);
            this.textBoxWords.TabIndex = 5;
            this.textBoxWords.TextChanged += new System.EventHandler(this.textBoxWords_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Words:";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(380, 4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(99, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete Cloud";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddWordCloud
            // 
            this.buttonAddWordCloud.Location = new System.Drawing.Point(299, 4);
            this.buttonAddWordCloud.Name = "buttonAddWordCloud";
            this.buttonAddWordCloud.Size = new System.Drawing.Size(75, 23);
            this.buttonAddWordCloud.TabIndex = 2;
            this.buttonAddWordCloud.Text = "Add New";
            this.buttonAddWordCloud.UseVisualStyleBackColor = true;
            this.buttonAddWordCloud.Click += new System.EventHandler(this.buttonAddWordCloud_Click);
            // 
            // comboBoxWordClouds
            // 
            this.comboBoxWordClouds.FormattingEnabled = true;
            this.comboBoxWordClouds.Location = new System.Drawing.Point(102, 3);
            this.comboBoxWordClouds.Name = "comboBoxWordClouds";
            this.comboBoxWordClouds.Size = new System.Drawing.Size(191, 24);
            this.comboBoxWordClouds.TabIndex = 1;
            this.comboBoxWordClouds.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Word Clouds:";
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDisplay.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(636, 528);
            this.pictureBoxDisplay.TabIndex = 0;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseDown);
            this.pictureBoxDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseMove);
            this.pictureBoxDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseUp);
            // 
            // checkBoxItem
            // 
            this.checkBoxItem.AutoSize = true;
            this.checkBoxItem.Location = new System.Drawing.Point(9, 113);
            this.checkBoxItem.Name = "checkBoxItem";
            this.checkBoxItem.Size = new System.Drawing.Size(85, 21);
            this.checkBoxItem.TabIndex = 10;
            this.checkBoxItem.Text = "Has Item";
            this.checkBoxItem.UseVisualStyleBackColor = true;
            this.checkBoxItem.CheckedChanged += new System.EventHandler(this.checkBoxItem_CheckedChanged);
            // 
            // labelItemName
            // 
            this.labelItemName.AutoSize = true;
            this.labelItemName.Location = new System.Drawing.Point(100, 113);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(79, 17);
            this.labelItemName.TabIndex = 11;
            this.labelItemName.Text = "Item Name:";
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Enabled = false;
            this.textBoxItemName.Location = new System.Drawing.Point(189, 110);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.Size = new System.Drawing.Size(144, 22);
            this.textBoxItemName.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "Item Texture:";
            // 
            // comboBoxItemTexture
            // 
            this.comboBoxItemTexture.Enabled = false;
            this.comboBoxItemTexture.FormattingEnabled = true;
            this.comboBoxItemTexture.Items.AddRange(new object[] {
            "thoughtCloud",
            "orb",
            "wine-bottle",
            "phoenixcard"});
            this.comboBoxItemTexture.Location = new System.Drawing.Point(195, 141);
            this.comboBoxItemTexture.Name = "comboBoxItemTexture";
            this.comboBoxItemTexture.Size = new System.Drawing.Size(138, 24);
            this.comboBoxItemTexture.TabIndex = 14;
            // 
            // PlatformDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 583);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlatformDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PlatformDialogcs";
            this.Load += new System.EventHandler(this.PlatformDialogcs_Load);
            this.Resize += new System.EventHandler(this.PlatformDialogcs_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffsetX)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFallTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddWordCloud;
        private System.Windows.Forms.ComboBox comboBoxWordClouds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown nudFallTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxSafe;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxStart;
        private System.Windows.Forms.NumericUpDown nudOffsetY;
        private System.Windows.Forms.NumericUpDown nudOffsetX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxInvisible;
        private System.Windows.Forms.Button buttonOnPlatform;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxNextLevel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.CheckBox checkBoxItem;
        private System.Windows.Forms.ComboBox comboBoxItemTexture;
    }
}