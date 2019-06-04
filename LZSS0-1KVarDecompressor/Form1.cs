using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Copyright (c) 2019, Coockie1173
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL STOLEN BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/


namespace LZSS0_1KVarDecompressor
{
    public partial class Form1 : Form
    {

        String fileName;
        String directory;

        //File Table Offsets
        UInt32[] TabOffsets = new UInt32[14]
            {
                0x00120000, 0x00140000, 0x00160000,
                0x00180000, 0x001C0000, 0x001E0000,
                0x00200000, 0x00240000, 0x00260000,
                0x00280000, 0x002A0000, 0x002C0000,
                0X002E0000, 0x00300000
            };

        public static string RemoteVersionURL = "https://raw.githubusercontent.com/Coockie1173/Bomberhacker/master/Version.txt";
        public static string ClientVersion = "0.66";

        public static int N = 1024;
        public static int F = 16;

        int advancePixels = 0;

        byte[] CurConvertedImg;
        byte[] CurConvertedPal;

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            FilePathBox.Text = open.FileName;
        }

        
        public byte[] Decode(byte[] data, UInt32 ComSize, UInt32 OutSize, UInt32 Pos)
        {
            byte[] OutPut = new byte[OutSize];
            byte[] fbuffer = new byte[N + F - 1];

            UInt32 flag;
            UInt32 buf_1;
            UInt32 inpos;
            UInt32 outpos;

            UInt64 val;
            UInt64 size;
            UInt64 back;

            buf_1 = 0;
            flag = 0;
            inpos = Pos;
            val = 0;
            size = 0;
            outpos = 0;
            back = 0;


            for (UInt32 i = 0; i < 0x400; i++) //1KB buffer
            {
                fbuffer[i] = 0x00;
            }
            buf_1 = 0x3BE;

            while (inpos <= ComSize - 1)
            {
                flag >>= 1;
                if (flag < 2)
                {
                    flag = (Convert.ToUInt32(data[inpos++]) & 0xFF) | 0x100;
                    if (flag < 0)
                    {
                        break;
                    }
                }
                if ((flag & 1) != 0)
                {
                    val = Convert.ToUInt64(data[inpos++]) & 0xFF;
                    if (val < 0)
                    {
                        break;
                    }
                    OutPut[outpos++] = Convert.ToByte(val);
                    fbuffer[buf_1] = Convert.ToByte(val);
                    buf_1 += 1;
                    buf_1 &= 0x3FF;
                }
                else if (inpos <= ComSize - 1)
                {
                    back = data[inpos++];
                    size = data[inpos++];

                    back |= ((size << 2) & 0x300);
                    size &= 0x3F;

                    for (UInt64 i = 0; i < size + 0x3; i++)
                    {
                        val = fbuffer[back];
                        OutPut[outpos++] = Convert.ToByte(val);
                        fbuffer[buf_1] = Convert.ToByte(val);

                        back += 1;
                        back &= 0x3FF;

                        buf_1 += 1;
                        buf_1 &= 0x3FF;
                    }
                }
            }
            return OutPut;
        }

        private byte[] Compress01(byte[] Data)
        {
            //level 0 compression, new compressor has been made but has not been implemented yet
            int CodeWordCount = (int)Math.Ceiling((float)(Data.Length / 8));
            int CompSize = (int)(Data.Length + CodeWordCount);
            byte[] CompBuf = new byte[CompSize + 1];
            byte[] dst = CompBuf;

            int DestPlace = 0;
            int SrcPlace = 0;

            while (DestPlace < CompSize)
            {
                if (DestPlace == 0 || DestPlace % 9 == 0)
                {
                    dst[DestPlace] = 0xFF;
                }
                else
                {
                    dst[DestPlace] = Data[SrcPlace];
                    SrcPlace++;
                }
                DestPlace++;
            }           

            return dst;
        }

        private void DecompressBut_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes(FilePathBox.Text);
            byte[] outData = Decode(data, (uint)data.Length, (uint)data.Length * 3, 0);
            byte[] trimmedData = TrimEnd(outData);
            SaveFileDialog save = new SaveFileDialog();
            save.ShowDialog();
            File.WriteAllBytes(save.FileName, trimmedData);
        }

        public static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        private void ImageInsert(Bitmap bm, N64Codec Codec, TextBox DataBox, TextBox PalBox)
        {
            //converts image into something the N64 can read
            byte[] ImgData = null, paletteData = null;

            N64Graphics.Convert(ref ImgData, ref paletteData, Codec, bm);

            DataBox.Text = BitConverter.ToString(ImgData).Replace("-", " ");
            if (!RGBAButton.Checked)
            {
                paletteData = TrimEnd(paletteData);
                PalBox.Text = BitConverter.ToString(paletteData).Replace("-", " ");
            }

            CurConvertedImg = ImgData;
            CurConvertedPal = paletteData;
        }

        private void CompButton_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes(FilePathBox.Text);
            byte[] outData = Compress01(data);
            SaveFileDialog save = new SaveFileDialog();
            save.ShowDialog();
            File.WriteAllBytes(save.FileName, outData);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap bm = new Bitmap(ImgPathBox.Text);
            if (CI4Button.Checked)
            {
                ImageInsert(bm, N64Codec.CI4, ImgDataBox, PalDataBox);
            }
            else if (CI8Button.Checked)
            {
                ImageInsert(bm, N64Codec.CI8, ImgDataBox, PalDataBox);
            }
            else
            {
                ImageInsert(bm, N64Codec.RGBA32, ImgDataBox, PalDataBox);
            }
            InsertButton.Enabled = true;    

        }

        private void OpenImgButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image files (*.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.FilterIndex = 1;

            DialogResult dresult = ofd.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                ImgPathBox.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //inserts image into .bin file
            OpenFileDialog Open = new OpenFileDialog();
            DialogResult dialogResult = Open.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string Path = Open.FileName;
                byte[] OpenedFile = File.ReadAllBytes(Path);
                int OffsetImg = Convert.ToInt32(numericUpDown1.Value);
                int OffsetPal = Convert.ToInt32(numericUpDown2.Value);

                Array.Copy(CurConvertedImg, 0, OpenedFile, OffsetImg, CurConvertedImg.Length);
                if (!RGBAButton.Checked)
                {
                    Array.Copy(CurConvertedPal, 0, OpenedFile, OffsetPal, CurConvertedPal.Length);
                }                

                SaveFileDialog save = new SaveFileDialog();
                dialogResult = save.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    File.WriteAllBytes(save.FileName, OpenedFile);
                }
            }
        }

        public UInt32 Read4Bytes(byte[] buf, UInt32 index)
        {
            return ((uint)(buf[index + 0] << 24 | buf[index + 1] << 16 | buf[index + 2] << 8 | buf[index + 3]));
        }

        private void OpenRomBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult res = new DialogResult();
            res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                //ROMFilePath.Text = open.FileName;
            }
        }

        UInt32 GetTableOffset(int TableID)
        {
            return TabOffsets[TableID];
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //injection method A -- working properly
            int TableOffset = (int)GetTableOffset((int)TableIDValues.Value);
            int index = 0x10;
            long[] Offsets = new long[992];
            byte[] ROM = Globals.ROM;

            DialogResult res = MessageBox.Show("Use custom ROM?", "Custom", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Z64 Rom|*.z64|Rom Files|*.rom";
                res = openFile.ShowDialog();
                
                if (res == DialogResult.OK)
                {
                    ROM = File.ReadAllBytes(openFile.FileName);
                    byte[] Check = new byte[0xC];
                    for (int i = 0; i < 0xC; i++)
                    {
                        Check[i] = ROM[0x20 + i];
                    }
                    string Checker = System.Text.Encoding.UTF8.GetString(Check);
                    if (Checker != "BOMBERMAN64U")
                    {
                        MessageBox.Show("ERROR! NOT A BOMBERMAN ROM! USING NORMAL ROM.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ROM = Globals.ROM;
                    }
                }
            }

            //read all offsets from table
            for (int i = 0; i < 992; i++)
            {
                
                Offsets[i] = Read4Bytes(ROM, (UInt32)(TableOffset + index));
                if (Offsets[i] == 4294967295)
                {
                    Offsets[i] = 0;
                }
                index += 0x08;
            }

            //find furthest offset
            long MaxOffset = Offsets.Max();
            int maxIndex = Offsets.ToList().IndexOf(MaxOffset);

            //grab Compressed Size
            int CsizeMaxIndex = (int)Read4Bytes(ROM, (UInt32)((maxIndex * 0x08) + 0x10 + 0x4 + TableOffset));

            //calculate index to write the new data at
            long WriteIndex = CsizeMaxIndex + MaxOffset + 0x2008 + TableOffset;

            byte[] CompressedBytes;
            byte[] UnCompressedBytes;

            long CompSize;
            long UnCompSize;

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Bin File|*.bin";
            DialogResult dialog;
            dialog = open.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                if (CompressedCheck.Checked)
                {
                    //if our file is precompressed we can grab the size for compressed, have to decompress for the decomp size
                    CompressedBytes = File.ReadAllBytes(open.FileName);
                    CompSize = CompressedBytes.Length;
                    UnCompSize = TrimEnd(Decode(CompressedBytes, (UInt32)CompressedBytes.Length, (UInt32)CompressedBytes.Length, 0)).Length;
                }
                else
                {
                    //have to compress first, then read the length from both the uncompressed and compressed file
                    UnCompressedBytes = File.ReadAllBytes(open.FileName);
                    CompressedBytes = Compress01(UnCompressedBytes);
                    CompSize = CompressedBytes.Length;
                    UnCompSize = UnCompressedBytes.Length;
                }

                //extend ROM if needed
                if (TrimEnd(ROM).Length + CompressedBytes.Length + 0x4 > ROM.Length)
                {
                    List<byte> ExROM = new List<byte>();
                    ExROM.AddRange(ROM.ToList());
                    for (int i = 0; i <= 4000000; i++)
                    {
                        ExROM.Add(00);
                    }
                    ROM = ExROM.ToArray();
                }

                byte[] Buffer = BitConverter.GetBytes(UnCompSize);
                //write 3 2 1 0

                int j = 0;
                for (int i = 3; i >= 0; i--)
                {                    
                    ROM[WriteIndex + j] = Buffer[i];
                    j++;
                }

                //this was at the end of the file table so I'm keeping it there
                Buffer = new byte[8]
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x09, 0x00
                };

                //plop data into ROM
                Array.Copy(CompressedBytes, 0, ROM, WriteIndex + 0x4, CompressedBytes.Length);
                Array.Copy(Buffer, 0, ROM, WriteIndex + 0x4 + CompressedBytes.Length, 8);
                                
                //update the position (table 13)
                WriteIndex = WriteIndex - 0x2008 - TableOffset - 0x4;

                Buffer = BitConverter.GetBytes(WriteIndex);
                Array.Reverse(Buffer);              Buffer[7] += 0x4;
                Array.Copy(Buffer, 4, ROM, (int)FileIDNumeric.Value * 0x8 + TableOffset + 0x10, 4);

                Buffer = BitConverter.GetBytes(CompSize);
                Array.Reverse(Buffer);
                Array.Copy(Buffer, 4, ROM, (int)FileIDNumeric.Value * 0x8 + TableOffset + 0x10 + 0x4, 4);


                //save the file
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Z64 Rom|*.z64|Rom Files|*.rom";
                DialogResult = save.ShowDialog();                
                if (DialogResult == DialogResult.OK)
                {
                    File.WriteAllBytes(save.FileName, ROM);
                }
            }




        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void FileGrabberButton_Click(object sender, EventArgs e)
        {
            Ripper ripper = new Ripper();
            ripper.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ViewerButton_Click(object sender, EventArgs e)
        {
            Viewer view = new Viewer();
            view.ShowDialog();
        }

        private void ViewImageButton_Click(object sender, EventArgs e)
        {
            ImageViewer img = new ImageViewer();
            img.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //messy code, have to fix
            string ExePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            bool moved = false;
            File.WriteAllText(ExePath.Substring(0, ExePath.LastIndexOf("\\")) + "\\version.txt", ClientVersion.ToString());

            if (File.Exists("Config.cfg"))
            {
                try
                {
                    Globals.ROM = File.ReadAllBytes(File.ReadLines("Config.cfg").First());
                    Globals.ConfPath = System.Reflection.Assembly.GetEntryAssembly().Location + "\\Config.cfg";
                    Globals.RomPath = File.ReadLines("Config.cfg").First();
                    Globals.RomFolder = Globals.RomPath.Substring(0, Globals.RomPath.LastIndexOf("\\"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ROM MOVED/REMOVED!");
                    moved = true;
                }
            }
            else
            {
                OpenRom();
            }
            if (moved)
            {
                MessageBox.Show("Please select a ROM");
                OpenFileDialog open = new OpenFileDialog();
                DialogResult res = open.ShowDialog();
                if (res == DialogResult.OK)
                {
                    OpenRom();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            //checking
            byte[] Check = new byte[0xC];
            for (int i = 0; i < 0xC; i++)
            {
                Check[i] = Globals.ROM[0x20 + i];
            }
            string Checker = System.Text.Encoding.UTF8.GetString(Check);

            if (Checker != "BOMBERMAN64U")
            {
                MessageBox.Show("NOT A BOMBERMAN ROM", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.WriteAllText("Config.cfg", "");
                Environment.Exit(0);
            }
        }

        private void OpenRom()
        {
            //opens rom
            MessageBox.Show("Please select a ROM");
            OpenFileDialog open = new OpenFileDialog();
            DialogResult res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                File.WriteAllText("Config.cfg", open.FileName);
                Globals.ROM = File.ReadAllBytes(File.ReadLines("Config.cfg").First());
                Globals.ConfPath = System.Reflection.Assembly.GetEntryAssembly().Location + "\\Config.cfg";
                Globals.RomPath = File.ReadLines("Config.cfg").First();
                Globals.RomFolder = Globals.RomPath.Substring(0, Globals.RomPath.LastIndexOf("\\"));
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void ReadHeaderBut_Click(object sender, EventArgs e)
        {
            ReadHeader readHeader = new ReadHeader();
            readHeader.ShowDialog();
        }

        private byte[] AddFourMB(byte[] Dat, int OutLength)
        {
            byte[] OutPut = new byte[OutLength];
            Array.Copy(Dat, OutPut, Dat.Length);
            return OutPut;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int TableOffset = 0x300000;
            int index = 0x10;
            long[] Offsets = new long[992];
            byte[] ROM = Globals.ROM;

            DialogResult res = MessageBox.Show("Use custom ROM?", "Custom", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Z64 Rom|*.z64|Rom Files|*.rom";
                res = openFile.ShowDialog();

                if (res == DialogResult.OK)
                {
                    ROM = File.ReadAllBytes(openFile.FileName);
                    byte[] Check = new byte[0xC];
                    for (int i = 0; i < 0xC; i++)
                    {
                        Check[i] = ROM[0x20 + i];
                    }
                    string Checker = System.Text.Encoding.UTF8.GetString(Check);
                    if (Checker != "BOMBERMAN64U")
                    {
                        MessageBox.Show("ERROR! NOT A BOMBERMAN ROM! USING NORMAL ROM.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ROM = Globals.ROM;
                    }
                }
            }

            for (int i = 0; i < 992; i++)
            {

                Offsets[i] = Read4Bytes(ROM, (UInt32)(TableOffset + index));
                if (Offsets[i] == 4294967295)
                {
                    Offsets[i] = 0;
                }
                index += 0x08;
            }

            int ItemPos = Convert.ToInt32(FileIDNumeric.Value);

            List<Byte> RomLst = new List<byte>();
            RomLst = ROM.ToList();

            int CSize = 0;
            int UCSize = 0;

            byte[] CompressedBytes;
            byte[] UnCompressedBytes;

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Bin File|*.bin";
            DialogResult dialog;
            dialog = open.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                if (CompressedCheck.Checked)
                {
                    CompressedBytes = File.ReadAllBytes(open.FileName);
                    CSize = CompressedBytes.Length;
                    UCSize = TrimEnd(Decode(CompressedBytes, (UInt32)CompressedBytes.Length, (UInt32)CompressedBytes.Length, 0)).Length;
                }
                else
                {
                    UnCompressedBytes = File.ReadAllBytes(open.FileName);
                    CompressedBytes = Compress01(UnCompressedBytes);
                    CSize = CompressedBytes.Length;
                    UCSize = UnCompressedBytes.Length;
                }


                int OldCsize = (int)Read4Bytes(RomLst.ToArray(), (UInt32)(ItemPos * 0x8 + TableOffset + 0x4 + 0x10));

                index = ItemPos * 0x8 + 0x10 + 0x8 + TableOffset;
                byte[] AddedOffset = new byte[4];
                /*AddedOffset = BitConverter.GetBytes(CSize - OldCsize);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(AddedOffset);*/

                for (int i = ItemPos + 1; i < 992; i++)
                {
                    if (Read4Bytes(RomLst.ToArray(), (UInt32)index) == 0xFFFFFFFF)
                    {
                        break;
                    }

                    int Offset = (int)Read4Bytes(RomLst.ToArray(), (UInt32)index) + 4;
                    Offset += CSize - OldCsize;
                    AddedOffset = BitConverter.GetBytes(Offset);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(AddedOffset);

                    RomLst.RemoveRange(index, 4);
                    RomLst.InsertRange(index, AddedOffset);

                    /*
                     RomLst[index] += AddedOffset[0];
                     RomLst[index + 1] += AddedOffset[1];
                     RomLst[index + 2] += AddedOffset[2];
                     RomLst[index + 3] += AddedOffset[3];
                     */

                    index += 0x8;
                }



                int WritePos = (int)Offsets[ItemPos];

                RomLst.RemoveRange(WritePos + TableOffset + 0x2008 + 0x4, OldCsize);

                AddedOffset = BitConverter.GetBytes(UCSize);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(AddedOffset);

                WritePos += TableOffset + 0x2008;

                RomLst[WritePos] = AddedOffset[0];
                RomLst[WritePos + 1] = AddedOffset[1];
                RomLst[WritePos + 2] = AddedOffset[2];
                RomLst[WritePos + 3] = AddedOffset[3];

                WritePos += 0x4;

                RomLst.InsertRange(WritePos, CompressedBytes);

                AddedOffset = BitConverter.GetBytes(CSize);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(AddedOffset);

                RomLst[TableOffset + ItemPos * 0x8 + 0x10 + 0x4] = AddedOffset[0]; 
                RomLst[TableOffset + ItemPos * 0x8 + 0x10 + 0x5] = AddedOffset[1];
                RomLst[TableOffset + ItemPos * 0x8 + 0x10 + 0x6] = AddedOffset[2];
                RomLst[TableOffset + ItemPos * 0x8 + 0x10 + 0x7] = AddedOffset[3];

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Z64 Rom|*.z64|Rom Files|*.rom";
                DialogResult = save.ShowDialog();



                if (DialogResult == DialogResult.OK)
                {
                    BinaryWriter writer = null;
                    string SaveLoc = save.FileName;

                    try
                    {
                        byte[] SaveStuff = AddFourMB(RomLst.ToArray(), ROM.Length);
                        File.WriteAllBytes(SaveLoc, SaveStuff);
                    }
                    catch
                    {
                        byte[] SaveStuff = AddFourMB(RomLst.ToArray(), ROM.Length + 4 * 0x100000);
                        File.WriteAllBytes(SaveLoc, SaveStuff);
                    }
                }
            }
        }
    }
}
