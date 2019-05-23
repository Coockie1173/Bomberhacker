using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZSS0_1KVarDecompressor
{
    public partial class MidiPlayer : Form
    {
        public int[] Offsets = 
        {
            0x003EB5C0, 0x00407977
        };
        
        public MidiPlayer()
        {
            InitializeComponent();
        }

        private void MidiPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}
