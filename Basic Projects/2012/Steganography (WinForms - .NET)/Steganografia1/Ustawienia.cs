using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Steganografia
{
    public partial class Ustawienia : Form
    {
        public int R;
        public int G;
        public int B;

        public Ustawienia(int R, int G, int B)
        {
            InitializeComponent();

            this.R = R;
            this.G = G;
            this.B = B;

            trackBar1.Value = R;
            trackBar2.Value = G;
            trackBar3.Value = B;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            R = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            G = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            B = trackBar3.Value;
        }
    }
}
