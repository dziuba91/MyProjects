using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assignment5_3
{
    public partial class setSizeColorForm : Form
    {
        public int height;
        public int width;

        public int R;
        public int G;
        public int B;

        public bool OK = false;


        public setSizeColorForm()
        {
            InitializeComponent();
        }

        public setSizeColorForm(int height, int width, int R, int G, int B)
        {
            InitializeComponent();

            this.R = R;
            textBoxR.Text = "" + R;
            this.G = G;
            textBoxG.Text = "" + G;
            this.B = B;
            textBoxB.Text = "" + B;

            this.height = height;
            textBoxX.Text = "" + height;
            this.width = width;
            textBoxY.Text = "" + width;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                R = Convert.ToInt32(this.textBoxR.Text);
                if (R < 0) R = 0;
                if (R > 255) R = 255; 
                G = Convert.ToInt32(this.textBoxG.Text);
                if (G < 0) G = 0;
                if (G > 255) G = 255; 
                B = Convert.ToInt32(this.textBoxB.Text);
                if (B < 0) B = 0;
                if (B > 255) B = 255;

                height = Convert.ToInt32(this.textBoxX.Text);
                if (height < 20) height = 20;
                width = Convert.ToInt32(this.textBoxY.Text);
                if (width < 20) width = 20;
            }
            catch
            {
                MessageBox.Show("Parameters setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.OK = true;

            Close();
        }
    }
}
