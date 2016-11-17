using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assignment6_4
{
    public partial class ReadProductIdForm : Form
    {
        public bool OK = false;
        private int ID;

        public ReadProductIdForm()
        {
            InitializeComponent();
        }

        private void ReadProductIdForm_Load(object sender, EventArgs e)
        {
            
        }

        private void getIdButton_Click(object sender, EventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(this.idTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Parameters setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.OK = true;

            Close();
        }

        public int getID()
        {
            return this.ID;
        }
    }
}
