using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZSS0_1KVarDecompressor
{
    public partial class ImageViewer : Form
    {
        public ImageViewer()
        {
            InitializeComponent();
        }

        public byte[] OpenedFile;
        string[] Files;
        string FilePath;
        int BaseOffset;
        int pallength;

        private void ImageViewer_Load(object sender, EventArgs e)
        {
            if (File.Exists(Globals.RomFolder + "\\decompressed\\Table 0_000.bin"))
            {
                string[] files = Directory.GetFiles(Globals.RomFolder + "\\decompressed");
                List<string> items = new List<string>();

                FilePath = Globals.RomFolder + "\\decompressed";

                foreach (string s in files)
                {
                    if (s.Contains("13_"))
                    {
                        items.Add(s);
                    }
                }

                Files = items.ToArray();

                if (Files.Length == 0)
                {
                    MessageBox.Show("Files not found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                foreach (string s in Files)
                {
                    FileSelector.Items.Add(s.Substring(s.LastIndexOf("_") + 1));
                }

            }
            else
            {
                MessageBox.Show("Files not ripped!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            /*
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] files = Directory.GetFiles(dialog.FileName);
                List<string> items = new List<string>();

                FilePath = dialog.FileName;

                foreach (string s in files)
                {
                    if (s.Contains("13_"))
                    {
                        items.Add(s);
                    }
                }

                Files = items.ToArray();

                if (Files.Length == 0)
                {
                    MessageBox.Show("Files not found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                foreach (string s in Files)
                {
                    FileSelector.Items.Add(s.Substring(s.LastIndexOf("_") + 1));
                }
                
            }*/
        }

        private void ReadImageButton_Click(object sender, EventArgs e)
        {
            byte[] img = File.ReadAllBytes(Files[FileSelector.SelectedIndex]);

            OpenedFile = img;

            int i = 0;
            while (Read4Bytes(img, (UInt32)i) != 0 || Read4Bytes(img, (UInt32)(i + 4)) != 0 || Read4Bytes(img, (UInt32)(i + 8)) != 0)
            {
                i += 12;
            }
            BaseOffset = i;
            NextImgBut.Enabled = true;
            UpdateVisBut.Enabled = true;
            LeftShifter.Enabled = true;
            PosFixBut.Enabled = true;   
            UpdateDebugBox("Image loaded succesfully");
        }

        public UInt32 Read4Bytes(byte[] buf, UInt32 index)
        {
            return ((uint)(buf[index + 0] << 24 | buf[index + 1] << 16 | buf[index + 2] << 8 | buf[index + 3]));
        }

        private void UpdateVisBut_Click(object sender, EventArgs e)
        {
            if (BaseOffset != 0)
            {

                int width = (int)NumericImgWidth.Value;
                int height = (int)HeightNumericUpDown.Value;

                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap);

                byte[] graf = new byte[width * height + 1];

                for (int i = 0; i <= width * height; i++)
                {
                    graf[i] = OpenedFile[BaseOffset + i];
                }

                byte[] pallette = new byte[5000];

                int j = 0;

                while (Read4Bytes(OpenedFile, (UInt32)j) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 4)) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 8)) != 0)
                {
                    pallette[j] = OpenedFile[BaseOffset + width * height + j];
                    j += 1;
                }

                if (CI4But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI4, N64IMode.AlphaCopyIntensity); 
                }
                else if (CI8But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI8, N64IMode.AlphaCopyIntensity);
                }

                PicBox.Image = bitmap;
                UpdateDebugBox(string.Format("Img offset : {0}, palette offset : {1}", ToHex(BaseOffset), ToHex(BaseOffset + width * height)));

            }
        }

        private void PosFixBut_Click(object sender, EventArgs e)
        {
            BaseOffset += 4;
            if (BaseOffset != 0)
            {

                int width = (int)NumericImgWidth.Value;
                int height = (int)HeightNumericUpDown.Value;

                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap);

                byte[] graf = new byte[width * height + 1];

                for (int i = 0; i <= width * height; i++)
                {
                    graf[i] = OpenedFile[BaseOffset + i];
                }

                byte[] pallette = new byte[5000];

                int j = 0;

                pallength = TrimEnd(pallette).Length;

                while (Read4Bytes(OpenedFile, (UInt32)j) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 4)) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 8)) != 0)
                {
                    pallette[j] = OpenedFile[BaseOffset + width * height + j];
                    j += 1;
                }

                if (CI4But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI4, N64IMode.AlphaCopyIntensity); ;
                }
                else if (CI8But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI8, N64IMode.AlphaCopyIntensity);
                }
                PicBox.Image = bitmap;
                UpdateDebugBox(string.Format("Img offset : {0}, palette offset : {1}", ToHex(BaseOffset), ToHex(BaseOffset + width * height)));

            }
        }

        private void LeftShifter_Click(object sender, EventArgs e)
        {
            BaseOffset -= 4;
            if (BaseOffset != 0)
            {

                int width = (int)NumericImgWidth.Value;
                int height = (int)HeightNumericUpDown.Value;

                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap);

                byte[] graf = new byte[width * height + 1];

                for (int i = 0; i <= width * height; i++)
                {
                    graf[i] = OpenedFile[BaseOffset + i];
                }

                byte[] pallette = new byte[5000];

                int j = 0;

                while (Read4Bytes(OpenedFile, (UInt32)j) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 4)) != 0 || Read4Bytes(OpenedFile, (UInt32)(j + 8)) != 0)
                {
                    pallette[j] = OpenedFile[BaseOffset + width * height + j];
                    j += 1;
                }

                if (CI4But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI4, N64IMode.AlphaCopyIntensity); ;
                }
                else if (CI8But.Checked)
                {
                    N64Graphics.RenderTexture(g, graf, pallette, 0, width, height, 1, N64Codec.CI8, N64IMode.AlphaCopyIntensity);
                }
                PicBox.Image = bitmap;
                UpdateDebugBox(string.Format("Img offset : {0}, palette offset : {1}", ToHex(BaseOffset), ToHex(BaseOffset + width * height)));
            }
        }

        private void NextImgBut_Click(object sender, EventArgs e)
        {
            int width = (int)NumericImgWidth.Value;
            int height = (int)HeightNumericUpDown.Value;

            int i = BaseOffset + width * height + pallength;
            while (Read4Bytes(OpenedFile, (UInt32)i) != 0 || Read4Bytes(OpenedFile, (UInt32)(i + 4)) != 0 || Read4Bytes(OpenedFile, (UInt32)(i + 8)) != 0)
            {
                i += 12;
            }


            BaseOffset = i;

            UpdateDebugBox(string.Format("Img offset : {0}, palette offset : {1}", int.Parse(BaseOffset.ToString(), System.Globalization.NumberStyles.HexNumber), int.Parse((BaseOffset + width * height).ToString(), System.Globalization.NumberStyles.HexNumber)));
        }

        public static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        private string ToHex(int value)
        {
            return String.Format("0x{0:X}", value);
        }

        void UpdateDebugBox(string text)
        {
            DebugText.Text = text;
        }

        private void PicBox_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bitmap = new Bitmap(PicBox.Image);
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            save.Title = "Save image";
            DialogResult dResult = save.ShowDialog();

            if (dResult == DialogResult.OK)
            {
                switch (save.FilterIndex)
                {
                    case 1: bitmap.Save(save.FileName, ImageFormat.Png); break;
                    case 2: bitmap.Save(save.FileName, ImageFormat.Jpeg); break;
                    case 3: bitmap.Save(save.FileName, ImageFormat.Bmp); break;
                }
            }
        }
    }
}
