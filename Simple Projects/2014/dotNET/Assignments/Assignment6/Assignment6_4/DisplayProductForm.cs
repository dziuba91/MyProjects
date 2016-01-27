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
    public partial class DisplayProductForm : Form
    {
        ObjectSerialization os;

        public DisplayProductForm()
        {
            InitializeComponent();
        }

        private void DisplayProductForm_Load(object sender, EventArgs e)
        {
            //MdiParent.LayoutMdi(MdiLayout.Cascade); 

            os = new ObjectSerialization();
        }

        private void readProductIdButton_Click(object sender, EventArgs e)
        {
            ReadProductIdForm f1 = new ReadProductIdForm();

            f1.ShowDialog();

            int ID = 0;
            if (f1.OK)
            {
                ID = f1.getID();
            }
            else return;

            f1.Dispose();
            f1 = null;

            Product product = null;
            if (os.findObject(ref product, ID))
            {
                displayRichTextBox.Text = product.ToString();
            }
            else
            {
                MessageBox.Show("Product ID = " + ID + " not found!", "Product Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                displayRichTextBox.Text = "Product ID = " + ID + " not found!";
            }
        }
    }
}
