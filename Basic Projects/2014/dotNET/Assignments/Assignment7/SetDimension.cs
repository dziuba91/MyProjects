using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assignment7
{
    public partial class SetDimension : Form
    {
        public int value;
        public bool OK = false;

        public SetDimension(int dimension)
        {
            InitializeComponent();

            valueTrackBar.Value = dimension;
            valueTextBox.Text = valueTrackBar.Value.ToString();
        }

        private void valueTrackBar_ValueChanged(object sender, EventArgs e)
        {
            valueTextBox.Text = valueTrackBar.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.value = Convert.ToInt32(valueTextBox.Text);
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
