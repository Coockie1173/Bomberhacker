using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZSS0_1KVarDecompressor
{
    public partial class ReadHeader : Form
    {
        //this can be useful for some files, not all though

        string FilePath = "";
        string[] Files;        

        public ReadHeader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Globals.RomFolder + "\\decompressed\\Table 0_000.bin"))
            {
                FileSelector.Items.Clear();
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
        }

        private void HeaderReader(byte[] file)
        {
            string OutPut = "";
            UInt32 Length = Read4Bytes(file, (UInt32)0x4);
            OutPut += string.Format("Header length: {0}{1}",Length, Environment.NewLine + Environment.NewLine);

            int Inpos = 0xC;

            for (int i = 0; i < Length; i++)
            {
                string InHex = Inpos.ToString("X");

                OutPut += string.Format("Data offset in file: {0}", InHex + Environment.NewLine);
                UInt32 OffsetterInt = Read4Bytes(file, (UInt32)(Inpos));
                OutPut += string.Format("{2}: Unknown Int: {0}{1}", OffsetterInt, Environment.NewLine, i);

                byte[] Floater = new byte[4];
                for (int j = 0; j != 4; j++)
                {
                    Floater[j] = file[Inpos + j + 4];
                }
                Array.Reverse(Floater);
                float UnknownFloat = BitConverter.ToSingle(Floater, 0);
                OutPut += string.Format("{2}: Unknown Float: {0}{1}", UnknownFloat, Environment.NewLine, i);

                UInt32 UnknownInt = Read4Bytes(file, (UInt32)(Inpos + 8));

                string hexValue = UnknownInt.ToString("X");

                OutPut += string.Format("{2}: Offset: {0}{1}", hexValue, Environment.NewLine + Environment.NewLine, i);

                Inpos += 0xC;
            }

            OffsetBox.Text = OutPut;
        }

        public UInt32 Read4Bytes(byte[] buf, UInt32 index)
        {
            return ((uint)(buf[index + 0] << 24 | buf[index + 1] << 16 | buf[index + 2] << 8 | buf[index + 3]));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] HeaderFiler = File.ReadAllBytes(Files[FileSelector.SelectedIndex]);

            HeaderReader(HeaderFiler);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult res = open.ShowDialog();

            if (res == DialogResult.OK)
            {
                byte[] HeaderFiler = File.ReadAllBytes(open.FileName);
                HeaderReader(HeaderFiler);
            }
        }
    }
}
