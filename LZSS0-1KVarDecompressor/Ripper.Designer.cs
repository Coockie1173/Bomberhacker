namespace LZSS0_1KVarDecompressor
{
    partial class Ripper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ripper));
            this.PathBox = new System.Windows.Forms.TextBox();
            this.DecompBut = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(175, 14);
            this.PathBox.MaxLength = 32767000;
            this.PathBox.Name = "PathBox";
            this.PathBox.ReadOnly = true;
            this.PathBox.Size = new System.Drawing.Size(262, 20);
            this.PathBox.TabIndex = 6;
            // 
            // DecompBut
            // 
            this.DecompBut.Location = new System.Drawing.Point(12, 12);
            this.DecompBut.Name = "DecompBut";
            this.DecompBut.Size = new System.Drawing.Size(157, 23);
            this.DecompBut.TabIndex = 4;
            this.DecompBut.Text = "Decompress";
            this.DecompBut.UseVisualStyleBackColor = true;
            this.DecompBut.Click += new System.EventHandler(this.DecompBut_ClickAsync);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "ReRip Table 01";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Ripper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 75);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.DecompBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ripper";
            this.Text = "Ripper";
            this.Load += new System.EventHandler(this.Ripper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Button DecompBut;
        private System.Windows.Forms.Button button1;
    }
}