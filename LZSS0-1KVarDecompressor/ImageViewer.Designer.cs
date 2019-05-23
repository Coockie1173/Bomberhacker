namespace LZSS0_1KVarDecompressor
{
    partial class ImageViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
            this.FileSelector = new System.Windows.Forms.ComboBox();
            this.ReadImageButton = new System.Windows.Forms.Button();
            this.DebugText = new System.Windows.Forms.TextBox();
            this.CI4But = new System.Windows.Forms.RadioButton();
            this.CI8But = new System.Windows.Forms.RadioButton();
            this.NumericImgWidth = new System.Windows.Forms.NumericUpDown();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpdateVisBut = new System.Windows.Forms.Button();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.PosFixBut = new System.Windows.Forms.Button();
            this.LeftShifter = new System.Windows.Forms.Button();
            this.NextImgBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumericImgWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FileSelector
            // 
            this.FileSelector.FormattingEnabled = true;
            this.FileSelector.Location = new System.Drawing.Point(476, 12);
            this.FileSelector.Name = "FileSelector";
            this.FileSelector.Size = new System.Drawing.Size(167, 21);
            this.FileSelector.TabIndex = 0;
            // 
            // ReadImageButton
            // 
            this.ReadImageButton.Location = new System.Drawing.Point(476, 39);
            this.ReadImageButton.Name = "ReadImageButton";
            this.ReadImageButton.Size = new System.Drawing.Size(167, 23);
            this.ReadImageButton.TabIndex = 1;
            this.ReadImageButton.Text = "ReadImage";
            this.ReadImageButton.UseVisualStyleBackColor = true;
            this.ReadImageButton.Click += new System.EventHandler(this.ReadImageButton_Click);
            // 
            // DebugText
            // 
            this.DebugText.Location = new System.Drawing.Point(476, 163);
            this.DebugText.Multiline = true;
            this.DebugText.Name = "DebugText";
            this.DebugText.ReadOnly = true;
            this.DebugText.Size = new System.Drawing.Size(167, 261);
            this.DebugText.TabIndex = 3;
            // 
            // CI4But
            // 
            this.CI4But.AutoSize = true;
            this.CI4But.Checked = true;
            this.CI4But.Location = new System.Drawing.Point(476, 68);
            this.CI4But.Name = "CI4But";
            this.CI4But.Size = new System.Drawing.Size(41, 17);
            this.CI4But.TabIndex = 4;
            this.CI4But.TabStop = true;
            this.CI4But.Text = "CI4";
            this.CI4But.UseVisualStyleBackColor = true;
            // 
            // CI8But
            // 
            this.CI8But.AutoSize = true;
            this.CI8But.Location = new System.Drawing.Point(523, 68);
            this.CI8But.Name = "CI8But";
            this.CI8But.Size = new System.Drawing.Size(41, 17);
            this.CI8But.TabIndex = 5;
            this.CI8But.Text = "CI8";
            this.CI8But.UseVisualStyleBackColor = true;
            // 
            // NumericImgWidth
            // 
            this.NumericImgWidth.Location = new System.Drawing.Point(477, 92);
            this.NumericImgWidth.Name = "NumericImgWidth";
            this.NumericImgWidth.Size = new System.Drawing.Size(40, 20);
            this.NumericImgWidth.TabIndex = 6;
            this.NumericImgWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // HeightNumericUpDown
            // 
            this.HeightNumericUpDown.Location = new System.Drawing.Point(524, 92);
            this.HeightNumericUpDown.Name = "HeightNumericUpDown";
            this.HeightNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.HeightNumericUpDown.TabIndex = 7;
            this.HeightNumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(476, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Height";
            // 
            // UpdateVisBut
            // 
            this.UpdateVisBut.Enabled = false;
            this.UpdateVisBut.Location = new System.Drawing.Point(570, 97);
            this.UpdateVisBut.Name = "UpdateVisBut";
            this.UpdateVisBut.Size = new System.Drawing.Size(75, 31);
            this.UpdateVisBut.TabIndex = 10;
            this.UpdateVisBut.Text = "UpdateVis";
            this.UpdateVisBut.UseVisualStyleBackColor = true;
            this.UpdateVisBut.Click += new System.EventHandler(this.UpdateVisBut_Click);
            // 
            // PicBox
            // 
            this.PicBox.Location = new System.Drawing.Point(12, 12);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(458, 412);
            this.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBox.TabIndex = 11;
            this.PicBox.TabStop = false;
            this.PicBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseClick);
            // 
            // PosFixBut
            // 
            this.PosFixBut.Enabled = false;
            this.PosFixBut.Location = new System.Drawing.Point(565, 134);
            this.PosFixBut.Name = "PosFixBut";
            this.PosFixBut.Size = new System.Drawing.Size(78, 26);
            this.PosFixBut.TabIndex = 12;
            this.PosFixBut.Text = "Shift Right";
            this.PosFixBut.UseVisualStyleBackColor = true;
            this.PosFixBut.Click += new System.EventHandler(this.PosFixBut_Click);
            // 
            // LeftShifter
            // 
            this.LeftShifter.Enabled = false;
            this.LeftShifter.Location = new System.Drawing.Point(476, 134);
            this.LeftShifter.Name = "LeftShifter";
            this.LeftShifter.Size = new System.Drawing.Size(83, 26);
            this.LeftShifter.TabIndex = 13;
            this.LeftShifter.Text = "Shift Left";
            this.LeftShifter.UseVisualStyleBackColor = true;
            this.LeftShifter.Click += new System.EventHandler(this.LeftShifter_Click);
            // 
            // NextImgBut
            // 
            this.NextImgBut.Enabled = false;
            this.NextImgBut.Location = new System.Drawing.Point(570, 69);
            this.NextImgBut.Name = "NextImgBut";
            this.NextImgBut.Size = new System.Drawing.Size(75, 22);
            this.NextImgBut.TabIndex = 14;
            this.NextImgBut.Text = "Next Img";
            this.NextImgBut.UseVisualStyleBackColor = true;
            this.NextImgBut.Click += new System.EventHandler(this.NextImgBut_Click);
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 450);
            this.Controls.Add(this.NextImgBut);
            this.Controls.Add(this.LeftShifter);
            this.Controls.Add(this.PosFixBut);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.UpdateVisBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HeightNumericUpDown);
            this.Controls.Add(this.NumericImgWidth);
            this.Controls.Add(this.CI8But);
            this.Controls.Add(this.CI4But);
            this.Controls.Add(this.DebugText);
            this.Controls.Add(this.ReadImageButton);
            this.Controls.Add(this.FileSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageViewer";
            this.Text = "ImageViewer";
            this.Load += new System.EventHandler(this.ImageViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericImgWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox FileSelector;
        private System.Windows.Forms.Button ReadImageButton;
        private System.Windows.Forms.TextBox DebugText;
        private System.Windows.Forms.RadioButton CI4But;
        private System.Windows.Forms.RadioButton CI8But;
        private System.Windows.Forms.NumericUpDown NumericImgWidth;
        private System.Windows.Forms.NumericUpDown HeightNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UpdateVisBut;
        private System.Windows.Forms.PictureBox PicBox;
        private System.Windows.Forms.Button PosFixBut;
        private System.Windows.Forms.Button LeftShifter;
        private System.Windows.Forms.Button NextImgBut;
    }
}