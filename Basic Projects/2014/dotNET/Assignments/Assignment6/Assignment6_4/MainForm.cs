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
    public partial class MainForm : Form
    {
        ObjectSerialization os;

        int i1 = 0, i2 = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            os = new ObjectSerialization();
            //os.clearFile(os.filePath);
            
            mainMenuStrip.MdiWindowListItem = windowToolStripMenuItem;

            //
            DisplayProductForm newDisplayProductForm = new DisplayProductForm();
            i2++;

            newDisplayProductForm.Text += " " + i2;
            newDisplayProductForm.MdiParent = this;

            newDisplayProductForm.Show();

            
            EnterProductForm newEnterProductForm = new EnterProductForm();
            i1++;

            newEnterProductForm.Text += " " + i1;
            newEnterProductForm.MdiParent = this;

            newEnterProductForm.Show();
        }

        private void enterProductFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterProductForm newEnterProductForm = new EnterProductForm();
            i1++;

            newEnterProductForm.Text += " " + i1;
            newEnterProductForm.MdiParent = this;

            newEnterProductForm.Show();
        }

        private void displayProductFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayProductForm newDisplayProductForm = new DisplayProductForm();
            i2++;

            newDisplayProductForm.Text += " " + i2;
            newDisplayProductForm.MdiParent = this;

            newDisplayProductForm.Show();
        }
    }
}
