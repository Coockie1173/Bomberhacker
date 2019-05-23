namespace LZSS0_1KVarDecompressor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.FilePathBox = new System.Windows.Forms.TextBox();
            this.DecompressBut = new System.Windows.Forms.Button();
            this.CompButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CI4Button = new System.Windows.Forms.RadioButton();
            this.CI8Button = new System.Windows.Forms.RadioButton();
            this.ImgDataBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PalDataBox = new System.Windows.Forms.TextBox();
            this.ImgPathBox = new System.Windows.Forms.TextBox();
            this.OpenImgButton = new System.Windows.Forms.Button();
            this.InsertButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.FileIDNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.CompressedCheck = new System.Windows.Forms.CheckBox();
            this.RGBAButton = new System.Windows.Forms.RadioButton();
            this.AboutButton = new System.Windows.Forms.Button();
            this.FileGrabberButton = new System.Windows.Forms.Button();
            this.ViewerButton = new System.Windows.Forms.Button();
            this.ViewImageButton = new System.Windows.Forms.Button();
            this.MusicViewer = new System.Windows.Forms.Button();
            this.ReadHeaderBut = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.TableIDValues = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileIDNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableIDValues)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(13, 13);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(75, 23);
            this.OpenFileButton.TabIndex = 1;
            this.OpenFileButton.Text = "Open File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // FilePathBox
            // 
            this.FilePathBox.Location = new System.Drawing.Point(94, 15);
            this.FilePathBox.Name = "FilePathBox";
            this.FilePathBox.ReadOnly = true;
            this.FilePathBox.Size = new System.Drawing.Size(374, 20);
            this.FilePathBox.TabIndex = 2;
            // 
            // DecompressBut
            // 
            this.DecompressBut.Location = new System.Drawing.Point(13, 42);
            this.DecompressBut.Name = "DecompressBut";
            this.DecompressBut.Size = new System.Drawing.Size(221, 23);
            this.DecompressBut.TabIndex = 3;
            this.DecompressBut.Text = "Decompress";
            this.DecompressBut.UseVisualStyleBackColor = true;
            this.DecompressBut.Click += new System.EventHandler(this.DecompressBut_Click);
            // 
            // CompButton
            // 
            this.CompButton.Location = new System.Drawing.Point(240, 42);
            this.CompButton.Name = "CompButton";
            this.CompButton.Size = new System.Drawing.Size(228, 23);
            this.CompButton.TabIndex = 4;
            this.CompButton.Text = "Compress";
            this.CompButton.UseVisualStyleBackColor = true;
            this.CompButton.Click += new System.EventHandler(this.CompButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Convert Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "__________________________________________________________________________";
            // 
            // CI4Button
            // 
            this.CI4Button.AutoSize = true;
            this.CI4Button.Checked = true;
            this.CI4Button.Location = new System.Drawing.Point(16, 128);
            this.CI4Button.Name = "CI4Button";
            this.CI4Button.Size = new System.Drawing.Size(41, 17);
            this.CI4Button.TabIndex = 7;
            this.CI4Button.TabStop = true;
            this.CI4Button.Text = "CI4";
            this.CI4Button.UseVisualStyleBackColor = true;
            // 
            // CI8Button
            // 
            this.CI8Button.AutoSize = true;
            this.CI8Button.Location = new System.Drawing.Point(16, 151);
            this.CI8Button.Name = "CI8Button";
            this.CI8Button.Size = new System.Drawing.Size(41, 17);
            this.CI8Button.TabIndex = 8;
            this.CI8Button.Text = "CI8";
            this.CI8Button.UseVisualStyleBackColor = true;
            // 
            // ImgDataBox
            // 
            this.ImgDataBox.Location = new System.Drawing.Point(12, 194);
            this.ImgDataBox.MaxLength = 32767000;
            this.ImgDataBox.Multiline = true;
            this.ImgDataBox.Name = "ImgDataBox";
            this.ImgDataBox.ReadOnly = true;
            this.ImgDataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ImgDataBox.Size = new System.Drawing.Size(149, 131);
            this.ImgDataBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Image data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Palette Data";
            // 
            // PalDataBox
            // 
            this.PalDataBox.Location = new System.Drawing.Point(181, 194);
            this.PalDataBox.MaxLength = 32767000;
            this.PalDataBox.Multiline = true;
            this.PalDataBox.Name = "PalDataBox";
            this.PalDataBox.ReadOnly = true;
            this.PalDataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PalDataBox.Size = new System.Drawing.Size(149, 131);
            this.PalDataBox.TabIndex = 11;
            // 
            // ImgPathBox
            // 
            this.ImgPathBox.Location = new System.Drawing.Point(194, 102);
            this.ImgPathBox.Name = "ImgPathBox";
            this.ImgPathBox.ReadOnly = true;
            this.ImgPathBox.Size = new System.Drawing.Size(270, 20);
            this.ImgPathBox.TabIndex = 13;
            // 
            // OpenImgButton
            // 
            this.OpenImgButton.Location = new System.Drawing.Point(111, 99);
            this.OpenImgButton.Name = "OpenImgButton";
            this.OpenImgButton.Size = new System.Drawing.Size(75, 23);
            this.OpenImgButton.TabIndex = 14;
            this.OpenImgButton.Text = "Open File";
            this.OpenImgButton.UseVisualStyleBackColor = true;
            this.OpenImgButton.Click += new System.EventHandler(this.OpenImgButton_Click);
            // 
            // InsertButton
            // 
            this.InsertButton.Enabled = false;
            this.InsertButton.Location = new System.Drawing.Point(341, 300);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(127, 25);
            this.InsertButton.TabIndex = 15;
            this.InsertButton.Text = "Insert";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Hexadecimal = true;
            this.numericUpDown1.Location = new System.Drawing.Point(341, 210);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(127, 20);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(338, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Image Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Palette offset";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Hexadecimal = true;
            this.numericUpDown2.Location = new System.Drawing.Point(341, 248);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(127, 20);
            this.numericUpDown2.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(451, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "__________________________________________________________________________";
            // 
            // FileIDNumeric
            // 
            this.FileIDNumeric.Location = new System.Drawing.Point(94, 344);
            this.FileIDNumeric.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.FileIDNumeric.Name = "FileIDNumeric";
            this.FileIDNumeric.Size = new System.Drawing.Size(127, 20);
            this.FileIDNumeric.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 346);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "File ID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Inject";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // CompressedCheck
            // 
            this.CompressedCheck.AutoSize = true;
            this.CompressedCheck.Location = new System.Drawing.Point(136, 377);
            this.CompressedCheck.Name = "CompressedCheck";
            this.CompressedCheck.Size = new System.Drawing.Size(90, 17);
            this.CompressedCheck.TabIndex = 26;
            this.CompressedCheck.Text = "Compressed?";
            this.CompressedCheck.UseVisualStyleBackColor = true;
            // 
            // RGBAButton
            // 
            this.RGBAButton.AutoSize = true;
            this.RGBAButton.Enabled = false;
            this.RGBAButton.Location = new System.Drawing.Point(63, 128);
            this.RGBAButton.Name = "RGBAButton";
            this.RGBAButton.Size = new System.Drawing.Size(67, 17);
            this.RGBAButton.TabIndex = 27;
            this.RGBAButton.Text = "RGBA32";
            this.RGBAButton.UseVisualStyleBackColor = true;
            this.RGBAButton.Visible = false;
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(394, 376);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(75, 23);
            this.AboutButton.TabIndex = 28;
            this.AboutButton.Text = "About";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // FileGrabberButton
            // 
            this.FileGrabberButton.Location = new System.Drawing.Point(394, 347);
            this.FileGrabberButton.Name = "FileGrabberButton";
            this.FileGrabberButton.Size = new System.Drawing.Size(75, 23);
            this.FileGrabberButton.TabIndex = 29;
            this.FileGrabberButton.Text = "Grab Files";
            this.FileGrabberButton.UseVisualStyleBackColor = true;
            this.FileGrabberButton.Click += new System.EventHandler(this.FileGrabberButton_Click);
            // 
            // ViewerButton
            // 
            this.ViewerButton.Location = new System.Drawing.Point(394, 405);
            this.ViewerButton.Name = "ViewerButton";
            this.ViewerButton.Size = new System.Drawing.Size(75, 23);
            this.ViewerButton.TabIndex = 30;
            this.ViewerButton.Text = "Viewer";
            this.ViewerButton.UseVisualStyleBackColor = true;
            this.ViewerButton.Visible = false;
            this.ViewerButton.Click += new System.EventHandler(this.ViewerButton_Click);
            // 
            // ViewImageButton
            // 
            this.ViewImageButton.Location = new System.Drawing.Point(327, 347);
            this.ViewImageButton.Name = "ViewImageButton";
            this.ViewImageButton.Size = new System.Drawing.Size(61, 52);
            this.ViewImageButton.TabIndex = 31;
            this.ViewImageButton.Text = "Image Viewer";
            this.ViewImageButton.UseVisualStyleBackColor = true;
            this.ViewImageButton.Click += new System.EventHandler(this.ViewImageButton_Click);
            // 
            // MusicViewer
            // 
            this.MusicViewer.Location = new System.Drawing.Point(327, 405);
            this.MusicViewer.Name = "MusicViewer";
            this.MusicViewer.Size = new System.Drawing.Size(61, 23);
            this.MusicViewer.TabIndex = 32;
            this.MusicViewer.Text = "Music";
            this.MusicViewer.UseVisualStyleBackColor = true;
            this.MusicViewer.Visible = false;
            // 
            // ReadHeaderBut
            // 
            this.ReadHeaderBut.Location = new System.Drawing.Point(260, 347);
            this.ReadHeaderBut.Name = "ReadHeaderBut";
            this.ReadHeaderBut.Size = new System.Drawing.Size(61, 52);
            this.ReadHeaderBut.TabIndex = 33;
            this.ReadHeaderBut.Text = "Read header";
            this.ReadHeaderBut.UseVisualStyleBackColor = true;
            this.ReadHeaderBut.Click += new System.EventHandler(this.ReadHeaderBut_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(13, 405);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 34;
            this.button3.Text = "SmartInject";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TableIDValues
            // 
            this.TableIDValues.Location = new System.Drawing.Point(94, 376);
            this.TableIDValues.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.TableIDValues.Name = "TableIDValues";
            this.TableIDValues.Size = new System.Drawing.Size(36, 20);
            this.TableIDValues.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 434);
            this.Controls.Add(this.TableIDValues);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ReadHeaderBut);
            this.Controls.Add(this.MusicViewer);
            this.Controls.Add(this.ViewImageButton);
            this.Controls.Add(this.ViewerButton);
            this.Controls.Add(this.FileGrabberButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.RGBAButton);
            this.Controls.Add(this.CompressedCheck);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FileIDNumeric);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.OpenImgButton);
            this.Controls.Add(this.ImgPathBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PalDataBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImgDataBox);
            this.Controls.Add(this.CI8Button);
            this.Controls.Add(this.CI4Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CompButton);
            this.Controls.Add(this.DecompressBut);
            this.Controls.Add(this.FilePathBox);
            this.Controls.Add(this.OpenFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BomberHack (De)Compressor & Image convertor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileIDNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableIDValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.TextBox FilePathBox;
        private System.Windows.Forms.Button DecompressBut;
        private System.Windows.Forms.Button CompButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton CI4Button;
        private System.Windows.Forms.RadioButton CI8Button;
        private System.Windows.Forms.TextBox ImgDataBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PalDataBox;
        private System.Windows.Forms.TextBox ImgPathBox;
        private System.Windows.Forms.Button OpenImgButton;
        private System.Windows.Forms.Button InsertButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown FileIDNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox CompressedCheck;
        private System.Windows.Forms.RadioButton RGBAButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button FileGrabberButton;
        private System.Windows.Forms.Button ViewerButton;
        private System.Windows.Forms.Button ViewImageButton;
        private System.Windows.Forms.Button MusicViewer;
        private System.Windows.Forms.Button ReadHeaderBut;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown TableIDValues;
    }
}

