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
    public partial class MainForm : Form
    {
        int i1 = 0, i2 = 0;
        DrawingShapes newDrawingShapes;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainMenuStrip.MdiWindowListItem = windowToolStripMenuItem;

            //
            HandlingText newHandlingText = new HandlingText();
            i2++;

            newHandlingText.Text += " " + i2;
            newHandlingText.MdiParent = this;

            newHandlingText.Show();


            //
            newDrawingShapes = new DrawingShapes();
            i1++;

            newDrawingShapes.Text += " " + i1;
            newDrawingShapes.MdiParent = this;

            newDrawingShapes.Show();
        }

        private void handlingTextFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandlingText newHandlingText = new HandlingText();
            i2++;

            newHandlingText.Text += " " + i2;
            newHandlingText.MdiParent = this;

            newHandlingText.Show();
        }

        private void drawingShapesFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingShapes newDrawingShapes = new DrawingShapes();
            i1++;

            newDrawingShapes.Text += " " + i1;
            newDrawingShapes.MdiParent = this;

            newDrawingShapes.Show();
        }
    }
}
