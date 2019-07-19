using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZSS0_1KVarDecompressor
{
    public partial class Ripper : Form
    {
        public Ripper()
        {
            InitializeComponent();
        }

        //multithreading support

        public static int N = 1024;
        public static int F = 16;
        public static byte[] RomBuffer;

        public const int TAmm = 14;

        public static readonly int[] ftable_arr = new int[TAmm]
        {
        0x00120000, 0x00140000, 0x00160000,
        0x00180000, 0x001C0000, 0x001E0000,
        0x00200000, 0x00240000, 0x00260000,
        0x00280000, 0x002A0000, 0x002C0000,
        0X002E0000, 0x00300000
        };


        public struct ftable_t
        {
            public UInt32 Table_Entry;
            public UInt32 Offset_Start_Data;
            public UInt32 Table_Index;
            public UInt32 Table_Offset;
            public UInt32 Fentry_Csize;
            public UInt32 Fentry_Ucsize;
            public UInt16 Table_Entry_Count;
            public byte[] fentry_data;
        }

        public static ftable_t[] tables = new ftable_t[TAmm];

        private async void DecompBut_ClickAsync(object sender, EventArgs e)
        {
            byte[] ROM;
            if (PathBox.Text != "")
            {
                ROM = File.ReadAllBytes(PathBox.Text);
                RomBuffer = ROM;
            }
            else
            {
                return;
            }

            string FolderF = PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1);
            Directory.CreateDirectory(FolderF + "Compressed");
            Directory.CreateDirectory(FolderF + "DeCompressed");

            init_table_data();

            List<Thread> ThreadList = new List<Thread>();

            //seperate case for Table 13, as that one is the biggest
            for (int i = 0; i < TAmm; i++)
            {
                Thread th = null;
                th = new Thread(() => parse_table(tables[i], (UInt32)i));
                th.SetApartmentState(ApartmentState.STA);
                th.IsBackground = true;
                ThreadList.Add(th);
                th.Start();                
                Thread.Sleep(100);
            }


            //Table 13
            /*int AmmFilesThirteen = 0;
            UInt32 IndexFiles = 0x10;
            UInt32 TabOffset = 0x00300000;
            while(Read4Bytes(ROM, (UInt32)(TabOffset + IndexFiles)) != 0xFFFFFFFF)
            {
                IndexFiles += 0x8;
                AmmFilesThirteen += 1;
            }

            int SplitAmm = 5;*/


            /*while(AmmFilesThirteen % SplitAmm != 0)
            {
                SplitAmm++;
            }

            int AmmFilesToRead = AmmFilesThirteen / SplitAmm;


            List<Task> tasks = new List<Task>();*/

            /*for (int i = 0; i < SplitAmm; i++)
            {
                Thread th2 = null;
                th2 = new Thread(() => RipFilesThirteen(AmmFilesToRead * i, AmmFilesToRead - 1, ROM));
                th2.SetApartmentState(ApartmentState.STA);
                th2.IsBackground = true;
                th2.Start();
                ThreadList.Add(th2);
                Thread.Sleep(100);
            }*/

            MessageBox.Show("Files ripped succesfully.");
        }

        private void RipFilesThirteen(int Index, int Length, byte[] ROM)
        {
            //this is all the same logic as in the parse table, just hardcoded for table 13
            UInt32 TabOffset = 0x00300000;
            UInt32 Indexer = 0x10 + (UInt32)Index * 0x08;


            for (int i = 0; i < Length; i++)
            {
                //so our thread doesn't stop
                try
                {
                    //Math for data
                    UInt32 CompSize = Read4Bytes(ROM, (UInt32)(TabOffset + Indexer + 0x4));
                    UInt32 UnCompSize = Read4Bytes(ROM, Read4Bytes(ROM, (UInt32)(TabOffset + Indexer)) + 0x2008 + TabOffset);
                    UInt32 Offset = Read4Bytes(ROM, (UInt32)(TabOffset + Indexer));

                    //makes a byte array
                    byte[] DataB = new byte[sizeof(byte) + CompSize];
                    Array.Copy(ROM, Read4Bytes(ROM, (UInt32)(TabOffset + Indexer)) + 0x4, DataB, 0, CompSize);
                    for (UInt32 pos = 0; pos < CompSize; pos++)
                    {
                        //copies the data over
                        DataB[pos] = RomBuffer[((TabOffset + 0x2008) + Offset) + pos];
                    }
                    //decodes, starts at 0x4 since the first four bytes is the decompressed size
                    byte[] DecompData = TrimEnd(Decode(DataB, CompSize, 307200, 0x4));

                    string FileName = (PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "DeCompressed" + "\\" + "Table " + 13 + "_" + (i + Index).ToString().PadLeft(3, '0') + ".bin");

                    FileStream file = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    file.Write(DecompData, 0, DecompData.Length);
                    file.Close();

                    FileName = (PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "Compressed" + "\\" + "Table " + 13 + "_" + (i + Index).ToString().PadLeft(3, '0') + ".bin");

                    file = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    file.Write(DataB, 0, DataB.Length);
                    file.Close();                    
                }
                catch
                {
                    
                }

                Indexer += 0x08;
            }
        }

        public UInt32 Read4Bytes(byte[] buf, UInt32 index)
        {
            return ((uint)(buf[index + 0] << 24 | buf[index + 1] << 16 | buf[index + 2] << 8 | buf[index + 3]));
        }

        public void parse_table(ftable_t tab, UInt32 num)
        {
            tab.Table_Entry_Count = Convert.ToUInt16((num == 13) ? 0x0368 : RomBuffer[tab.Table_Entry + tab.Offset_Start_Data]);
            if (num == 0xD)
            {
                tab.Table_Entry_Count = 1000;
            }
            int temp = tab.Table_Entry_Count;
            UInt32 Table_Index = 0x10;
            for (int i = 0; i <= tab.Table_Entry_Count; i++)
            {
                //if (num == 0xD && i == 47)
                //{
                //    break;
                //}

                tab.Table_Offset = Read4Bytes(RomBuffer, tab.Table_Entry + Table_Index);
                if (tab.Table_Offset == 0xFFFFFFFF)
                {
                    //DebugBox.Text += "Error trying to read another entry " + tab.Table_Offset.ToString() + Environment.NewLine;
                    break;
                }

                tab.Fentry_Csize = Read4Bytes(RomBuffer, (tab.Table_Entry + Table_Index) + 0x4);

                if (i == 35)
                {
                    //DebugBox.Text += "Huge Entry + " + num + "_" + i + Environment.NewLine;
                }

                if (tab.Fentry_Csize > 0x00010000 && i != 34)
                {
                    //DebugBox.Text += "Next entry is huge. Uncompressed file, moving along..." + Environment.NewLine;
                    //DebugBox.Text += "Huge Entry + " + num + "_" + i + Environment.NewLine;
                    /*if (tab.Fentry_Csize < 2147483648)
                    {
                        
                        tab.fentry_data = new byte[sizeof(byte) + tab.Fentry_Csize];
                        Array.Copy(RomBuffer, tab.Table_Index, tab.fentry_data, 0, tab.Fentry_Csize);
                        File.WriteAllBytes(PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "DeCompressed" + "\\" + "Table " + num + "_" + i, tab.fentry_data);
                    }
                    if (i == 0x22 && num == 0xD)
                    {
                        tab.Fentry_Ucsize = Read4Bytes(RomBuffer, (tab.Table_Entry + tab.Offset_Start_Data) + tab.Table_Offset);
                        //unsafe, not sure if correct
                        tab.fentry_data = new byte[sizeof(byte) + tab.Fentry_Csize];
                        Array.Copy(RomBuffer, tab.Table_Index, tab.fentry_data, 0, tab.Fentry_Csize);
                        File.WriteAllBytes(PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "Compressed" + "\\" + "Table " + num + "_" + i, tab.fentry_data);


                        string tmp = new string(new char[30]);


                        byte[] Decomp = new byte[307200];
                        byte[] Decompressed = new byte[307200];

                        tmp = string.Format("decompressed//FT_{0:X2}_F_{1:X2}.bin", num, i);

                        string tmp_decomp = new string(new char[307200]);


                        Decompressed = Decode(tab.fentry_data, tab.Fentry_Csize, tab.Fentry_Ucsize * 4, 0x4);

                        byte[] DecompOut = new byte[TrimEnd(Decompressed).Length];

                        DecompOut = TrimEnd(Decompressed);


                        File.WriteAllBytes(PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "DeCompressed" + "\\" + "Table " + num + "_" + i, DecompOut);
                        
                    }*/
                }
                else
                {

                    tab.Fentry_Ucsize = Read4Bytes(RomBuffer, (tab.Table_Entry + tab.Offset_Start_Data) + tab.Table_Offset);
                    //unsafe, not sure if correct
                    tab.fentry_data = new byte[sizeof(byte) + tab.Fentry_Csize];
                    Array.Copy(RomBuffer, Table_Index, tab.fentry_data, 0, tab.Fentry_Csize);
                    for (UInt32 pos = 0; pos < tab.Fentry_Csize; pos++)
                    {
                        tab.fentry_data[pos] = RomBuffer[((tab.Table_Entry + tab.Offset_Start_Data) + tab.Table_Offset) + pos];
                    }

                    string tmp = new string(new char[30]);


                    byte[] Decomp = new byte[307200];
                    byte[] Decompressed = new byte[307200];

                    tmp = string.Format("decompressed//FT_{0:X2}_F_{1:X2}.bin", num, i);

                    string tmp_decomp = new string(new char[307200]);


                    Decompressed = Decode(tab.fentry_data, tab.Fentry_Csize, 307200, 0x4);

                    //List<byte> ActDecomp = new List<byte>();
                    //ActDecomp = Decompressed.ToList();

                    byte[] DecompOut = new byte[TrimEnd(Decompressed).Length];

                    DecompOut = TrimEnd(Decompressed);
                    /*
                    if (DecompOut.Length - 1 > tab.Fentry_Ucsize)
                    {
                        DecompOut = DecompOut.ToList().GetRange(0, (int)tab.Fentry_Ucsize).ToArray();
                    }
                    */
                    
                    //this can be done in an easier fashion
                    string tempo = i.ToString();

                    if (tempo.Length == 1)
                    {
                        tempo = "00" + tempo;
                    }
                    else if (tempo.Length == 2)
                    {
                        tempo = "0" + tempo;
                    }

                    //File.WriteAllBytes(PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "DeCompressed" + "\\" + "Table " + num + "_" + tempo + ".bin", DecompOut);
                    //File.WriteAllBytes(PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "Compressed" + "\\" + "Table " + num + "_" + i + ".bin", tab.fentry_data);

                    string FileName = (PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "DeCompressed" + "\\" + "Table " + num + "_" + tempo + ".bin");

                    FileStream file = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    file.Write(DecompOut, 0, DecompOut.Length);
                    file.Close();

                    FileName = (PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1) + "Compressed" + "\\" + "Table " + num + "_" + i + ".bin");

                    file = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    file.Write(tab.fentry_data, 0, tab.fentry_data.Length);
                    file.Close();                    

                    //debug stuff
                    string newline = Environment.NewLine;
                    //DebugBox.Text += string.Format("[TABLE {0} {1}]{2}Table Offset {3}{2}Compressed size {4}{2}" +
                    //   "Decompressed size {5}{2}Table Index {6} {2}Entry Count {7}{2}{8}{2}{2}", num, tab.Table_Entry, newline, tab.Table_Offset, tab.Fentry_Csize, tab.Fentry_Ucsize, i, tab.Table_Entry_Count, Table_Index);
                }
                Table_Index += 0x08;
            }

            Thread.EndThreadAffinity();
        }

        public void init_table_data()
        {
            for (int i = 0; i < TAmm; i++)
            {
                tables[i].Table_Entry = (UInt32)ftable_arr[i];
                tables[i].Offset_Start_Data = 0x2008;
                tables[i].Table_Index = 0;
                tables[i].Table_Entry_Count = 0;
                tables[i].Table_Offset = 0;
                tables[i].Fentry_Csize = 0;
                tables[i].Fentry_Ucsize = 0;
            }
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Z64 Rom|*.z64|V64 Rom|*.v64|N64 Rom|*.n64";
            open.ShowDialog();

            PathBox.Text = open.FileName.ToString();
        }

        byte[] fbuffer = new byte[N + F - 1];
        public byte[] Decode(byte[] data, UInt32 ComSize, UInt32 OutSize, UInt32 Pos)
        {
            byte[] OutPut = new byte[OutSize];

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


            for (UInt32 i = 0; i < 0x400; i++)
            {
                fbuffer[i] = 0x00;
            }
            buf_1 = 0x3BE;

            while (inpos <= ComSize - 1)
            {
                flag >>= 1;
                if (flag < 2)
                {
                    flag = Convert.ToUInt32((data[inpos++] & 0xFF) | 0x100);
                    if (flag < 0)
                    {
                        break;
                    }
                }
                if ((flag & 1) != 0)
                {
                    val = Convert.ToUInt64(data[inpos++] & 0xFF);
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

        public void LZSS_dec(byte[] data, UInt32 CompSize, byte[] OutPut, UInt32 OutSize, UInt32 pos, byte mode)
        {
            UInt32 InputPos = pos;
            UInt32 OutputPosition = 0;

            byte[] Buffer = new byte[0x1000];

            int b_idx = 0;
            ulong bits = 0;

            if (mode == 1)
            {
                for (int i = 0; i < 0x1000; i++)
                {
                    Buffer[i] = 0;
                }
                b_idx = 1;
            }
            else if (mode == 2 || mode == 3)
            {
                b_idx = 0xFEE;

                int bCounter = 0;

                for (int i = 0; i < b_idx; i++)
                {
                    if (mode != 2)
                    {
                        Buffer[bCounter++] = 0;
                    }
                    else
                    {
                        Buffer[bCounter++] = 32;
                    }
                }

                for (int i = 0; i < (0x1000 - b_idx); i++)
                {
                    Buffer[bCounter++] = 0;
                }

            }
            while (InputPos < CompSize)
            {
                if (bits < 0x100)
                {
                    bits = Convert.ToUInt64(data[InputPos]) | 0xFF00;
                    InputPos += 1;
                }
                if ((bits & 1) == 0)
                {
                    if (mode != 1)
                    {
                        Buffer[b_idx] = data[InputPos];
                        b_idx = (b_idx + 1) & 0xFFF;
                    }
                    OutPut[OutputPosition++] = data[InputPos];
                    InputPos += 1;
                }
                else
                {
                    ulong val;
                    ulong size;

                    //# size = data[inputPosition + (mode=="LZSS")]
                    //# val = data[inputPosition + (mode!="LZSS")]
                    if (mode == 2 || mode == 3)
                    {
                        val = data[InputPos];
                        size = data[InputPos + 1];
                    }
                    else
                    {
                        size = data[InputPos];
                        val = data[InputPos + 1];
                        if ((size + val == 0))
                        {
                            break;
                        }
                    }
                    InputPos += 2;

                    ulong back = size & 0xF0;
                    back *= 0x10;
                    back |= val;
                    back &= 0xFFF;
                    size = (size & 0xF) + 2 + Convert.ToUInt32((mode == 2) || mode == 3);
                    for (UInt64 i = 0; i < size; i++)
                    {
                        if (mode != 1)
                        {
                            Buffer[b_idx] = Buffer[back];
                            OutPut[OutputPosition++] = (Buffer[back]);
                            b_idx = (b_idx + 1) & 0xFFF;
                            back = (back + 1) & 0xFFF;
                        }
                        else
                        {
                            OutPut[OutputPosition++] = OutPut[OutputPosition - back];
                        }
                    }
                }
                bits >>= 1;
            }
        }

        public static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        private void Ripper_Load(object sender, EventArgs e)
        {
            PathBox.Text = Globals.RomPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this table doesn't rip properly for some reason
            //will fix this along with the cleanup soon


            UInt32 Offset = 0x120000;
            int AmmFiles = 4;

            byte[] ROM;

            ROM = File.ReadAllBytes(PathBox.Text);
            RomBuffer = ROM;

            string FolderF = PathBox.Text.Substring(0, PathBox.Text.LastIndexOf("\\") + 1);
            Directory.CreateDirectory(FolderF + "Compressed");
            Directory.CreateDirectory(FolderF + "DeCompressed");

            int Index = 0x10;

            for (int i = 0; i < AmmFiles; i++)
            {
                UInt32 CompSize = (UInt32)Offset + (UInt32)Index + 0x4;
                UInt32 UnCompLength = Read4Bytes(ROM, 0x2008 + Offset + (UInt32)Read4Bytes(ROM, (UInt32)Index + Offset));
                List<byte> Dat = new List<byte>();
                byte[] buf = new byte[CompSize];
                Array.Copy(ROM, (UInt32)Read4Bytes(ROM, (UInt32)Offset + (UInt32)Index) + Offset + 0x2008, buf, 0, UnCompLength);

                Dat.AddRange(buf);
                Decode(Dat.ToArray(), CompSize, CompSize * 2, 0);
            }
        }
    }
}
