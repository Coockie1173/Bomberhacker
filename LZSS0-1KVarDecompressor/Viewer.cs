using OpenGL;
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
    public partial class Viewer : Form
    {
        byte[] modelfile;
        List<float> Points = new List<float>();

        Offsets[] offset = new Offsets[50];
        PointInSpace[] points = new PointInSpace[5000];

        public Viewer()
        {
            InitializeComponent();
        }

        public struct Offsets
        {
            public UInt32 Identifier;
            public float FloatType;
            public UInt32 Offsetter;
        }

        public struct PointInSpace
        {
            public float X;
            public float Y;
            public float Z;
        }

        static float ToFloat(byte[] input)
        {
            byte[] newArray = new[] { input[2], input[3], input[0], input[1] };
            return BitConverter.ToSingle(newArray, 0);
        }

        private void glControl1_ContextCreated(object sender, OpenGL.GlControlEventArgs e)
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();            
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;

            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            Gl.Begin(PrimitiveType.Triangles);
            Gl.Color3(1.0f, 0.0f, 0.0f); Gl.Vertex2(0.0f, 0.0f);
            Gl.Color3(0.0f, 1.0f, 0.0f); Gl.Vertex2(0.5f, 1.0f);
            Gl.Color3(0.0f, 0.0f, 1.0f); Gl.Vertex2(1.0f, 0.0f);
            Gl.End();
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult res = new DialogResult();
            res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                modelfile = File.ReadAllBytes(open.FileName);
                int i = 0;
                int j = 0;
                while(Read4Bytes(modelfile, (UInt32)i) != 0 || Read4Bytes(modelfile, (UInt32)(i + 4)) != 0 || Read4Bytes(modelfile, (UInt32)(i + 8)) != 0)
                {
                    offset[j].Identifier = Read4Bytes(modelfile, (UInt32)i);
                    byte[] InFloat = new byte[4];
                    offset[j].FloatType = BitConverter.ToSingle(modelfile, i+4);
                    offset[j].Offsetter = Read4Bytes(modelfile, (UInt32)(i + 8));
                    j++;
                    i += 12;
                }
                foreach(Offsets o in offset)
                {
                    OffsetBox.Items.Add(o.Offsetter);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public UInt32 Read4Bytes(byte[] buf, UInt32 index)
        {
            return ((uint)(buf[index + 0] << 24 | buf[index + 1] << 16 | buf[index + 2] << 8 | buf[index + 3]));
        }

        private void ReadDataButton_Click(object sender, EventArgs e)
        {
            int Length = (int)(offset[OffsetBox.SelectedIndex + 1].Offsetter - offset[OffsetBox.SelectedIndex].Offsetter);
            int Index = 0;
            int i = (int)offset[OffsetBox.SelectedIndex].Offsetter;
            int j = 0;

            while(Index < Length - 8)
            {
                points[j].X = Read4Bytes(modelfile, (UInt32)i);
                points[j].Y = Read4Bytes(modelfile, (UInt32)i + 4);
                points[j].Z = Read4Bytes(modelfile, (UInt32)i + 8);
                i += 12;
                Index += 12;
                j++;
            }
                       

            Gl.Clear(ClearBufferMask.ColorBufferBit);
            
            i = 0;
            foreach (PointInSpace p in points)
            {
                if (i == 3)
                {
                    i = 0;
                    Gl.End();
                }
                if (i == 0)
                {
                    Gl.Begin(PrimitiveType.Triangles);
                    Gl.Vertex3(p.X, p.Y, p.Z);
                    i++;
                }
                else
                {
                    Gl.Vertex3(p.X, p.Y, p.Z);
                    i++;
                }              

            }

            glControl1.Invalidate();
        }
    }
}
