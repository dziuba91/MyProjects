
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Assignment7
{
    public partial class DrawingShapes : Form
    {
        int mainX;
        int mainY;
        int mainHeight;
        int mainWidth;

        int dimension = 3;

        string filePathName;
        Bitmap bitMap = null;

        Color drawingColor = Color.Black;

        Graphics drawingGraphics;

        bool dimensionSetting = false;
        bool colorSetting = false;
        bool motionSetting = true;

        int mode = 0;

        Brush color;

        // draving lines
        bool firstPointSelected = false;
        Point firstPoint;

        Point areaPosition;
        int positionChange = 10;

        public DrawingShapes()
        {
            InitializeComponent();

            mainPanel.Controls.Add(this.mainPictureBox);

            mainX = 0;
            mainY = 0;
            mainHeight = mainPictureBox.Height;
            mainWidth = mainPictureBox.Width;

            bitMap = new Bitmap(mainWidth, mainHeight);
            drawingGraphics = Graphics.FromImage(bitMap);

            drawingGraphics.Clear(Color.White);


            changeDimensionSetting(dimensionSetting);
            changeColorSetting(colorSetting);

            if (motionSetting) setMotionToolStripMenuItem.Checked = true;
            else setMotionToolStripMenuItem.Checked = false;

            freelyToolStripMenuItem.Checked = true;


            color = new SolidBrush(drawingColor);

            // lines
            firstPoint = Point.Empty;

            areaPosition = new Point(this.ClientRectangle.Left, this.ClientRectangle.Top);


            DisplayInfo();
        }

        private void SaveFile()
        {
            mainPictureBox.DrawToBitmap(bitMap, new Rectangle(0, 0, mainPictureBox.Width, mainPictureBox.Height));
            
            bitMap.Save(filePathName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = filePathName;

            saveFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                filePathName = saveFileDialog.FileName;

                SaveFile();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create a new document...");

            mainPictureBox.Width = mainWidth;
            mainPictureBox.Height = mainHeight;

            mainPictureBox.Image = null;
            
            areaPosition = Point.Empty;

            bitMap = new Bitmap(mainWidth, mainHeight);
            drawingGraphics = Graphics.FromImage(bitMap);

            drawingGraphics.Clear(Color.White);

            drawingGraphics.Dispose();

            Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                filePathName = openFileDialog.FileName;
                bitMap = new Bitmap(filePathName);
                //drawingGraphics = Graphics.FromImage(bitMap);

                mainPictureBox.Image = bitMap;
            }

            Refresh();
        }

        private void freelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            elipseToolStripMenuItem.Checked = false;
            rectangleToolStripMenuItem.Checked = false;
            lineToolStripMenuItem.Checked = false;

            freelyToolStripMenuItem.Checked = true;

            mode = 0;


            if (firstPointSelected)
            {
                firstPointSelected = false;
                
                Refresh();
            }

            DisplayInfo();
        }

        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            freelyToolStripMenuItem.Checked = false;
            rectangleToolStripMenuItem.Checked = false;
            lineToolStripMenuItem.Checked = false;

            elipseToolStripMenuItem.Checked = true;

            mode = 1;


            if (firstPointSelected)
            {
                firstPointSelected = false;

                Refresh();
            }

            DisplayInfo();
        }

        private void rectangleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            freelyToolStripMenuItem.Checked = false;
            elipseToolStripMenuItem.Checked = false;
            lineToolStripMenuItem.Checked = false;

            rectangleToolStripMenuItem.Checked = true;

            mode = 2;


            if (firstPointSelected)
            {
                firstPointSelected = false;

                Refresh();
            }

            DisplayInfo();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            freelyToolStripMenuItem.Checked = false;
            elipseToolStripMenuItem.Checked = false;
            rectangleToolStripMenuItem.Checked = false;

            lineToolStripMenuItem.Checked = true;

            mode = 3;

            DisplayInfo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || mode != 0)
                return;

            drawingGraphics = Graphics.FromImage(bitMap);

            Rectangle r = new Rectangle(e.X - dimension/2, e.Y - dimension/2, dimension, dimension);

            //Pen drawingPen = new Pen(drawingColor, 2.4f);


            drawingGraphics.FillEllipse(color, r);


            drawingGraphics.Dispose();

            Refresh();
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (bitMap != null)
            {
                e.Graphics.DrawImage(bitMap, areaPosition);
            }
        }

        private void dimensionsGRandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dimensionSetting) dimensionSetting = false;
            else dimensionSetting = true;

            changeDimensionSetting(dimensionSetting);

            DisplayInfo();
        }

        private void dimensionsSetManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDimension f = new SetDimension(dimension);
            f.ShowDialog();

            if (f.OK)
                dimension = f.value;

            DisplayInfo();
        }

        private void colorsGRandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (colorSetting) colorSetting = false;
            else colorSetting = true;

            changeColorSetting(colorSetting);

            DisplayInfo();
        }

        private void colorsSetManualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            drawingColor = colorDialog.Color;
            
            color = new SolidBrush(drawingColor);

            DisplayInfo(); 
        }


        private void changeDimensionSetting(bool dimensionSetting)
        {
            if (dimensionSetting)
            {
                dimensionsGRandToolStripMenuItem.Checked = false;
                dimensionsSetManualToolStripMenuItem.Enabled = true;
            }
            else
            {
                dimensionsGRandToolStripMenuItem.Checked = true;
                dimensionsSetManualToolStripMenuItem.Enabled = false;
            }
        }

        private void changeColorSetting(bool colorSetting)
        {
            if (colorSetting)
            {
                colorsGRandToolStripMenuItem.Checked = false;
                colorsSetManualToolStripMenuItem.Enabled = true;
            }
            else
            {
                colorsGRandToolStripMenuItem.Checked = true;
                colorsSetManualToolStripMenuItem.Enabled = false;
            }
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (motionSetting) motionSetting = false;
            else motionSetting = true;

            if (motionSetting) setMotionToolStripMenuItem.Checked = true;
            else setMotionToolStripMenuItem.Checked = false;

            DisplayInfo();
        }

        private void mainPictureBox_Click(object sender, EventArgs e)
        {
            if (!dimensionSetting)
            {
                Random r = new Random();
                dimension = r.Next(1, 100);
            }
            else
            {  }

            if (!colorSetting)
            {
                Random c = new Random();
                drawingColor = Color.FromArgb(c.Next(0, 255), c.Next(0, 255), c.Next(0, 255));
                color = new SolidBrush(drawingColor);
            }
            else
            {  }
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || mode == 0)
                return;
            else if (mode == 1)
            {
                drawingGraphics = Graphics.FromImage(bitMap);

                Rectangle r = new Rectangle(e.X - dimension / 2, e.Y - dimension / 2, dimension, dimension);

                drawingGraphics.FillEllipse(color, r);

                drawingGraphics.Dispose();

                Refresh();
            }
            else if (mode == 2)
            {
                drawingGraphics = Graphics.FromImage(bitMap);

                Rectangle r = new Rectangle(e.X - dimension / 2, e.Y - dimension / 2, dimension, dimension);

                drawingGraphics.FillRectangle(color, r);

                drawingGraphics.Dispose();

                Refresh();
            }
            else if (mode == 3)
            {
                if (firstPointSelected)
                {
                    drawingGraphics = Graphics.FromImage(bitMap);

                    drawingGraphics.DrawLine(new Pen(color, dimension), firstPoint, new Point(e.X, e.Y));
                    
                    drawingGraphics.Dispose();

                    Refresh();

                    firstPointSelected = false;
                }
                else
                {
                    drawingGraphics = mainPictureBox.CreateGraphics();

                    int elipseDimension = 16;
                    Rectangle r = new Rectangle(e.X - elipseDimension / 2, e.Y - elipseDimension / 2, elipseDimension, elipseDimension);

                    drawingGraphics.FillEllipse(color, r);

                    drawingGraphics.Dispose();


                    firstPoint = new Point(e.X, e.Y);

                    firstPointSelected = true;
                }
            }
        }

        private void DrawingShapes_KeyDown(object sender, KeyEventArgs e)
        {
            if (motionSetting)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        areaPosition.X -= positionChange;
                        break;

                    case Keys.Up:
                        areaPosition.Y -= positionChange;
                        break;

                    case Keys.Right:
                        areaPosition.X += positionChange;
                        break;

                    case Keys.Down:
                        areaPosition.Y += positionChange;
                        break;
                }

                Refresh();

                mainPictureBox.DrawToBitmap(bitMap, new Rectangle(0, 0, mainPictureBox.Width, mainPictureBox.Height));
                areaPosition = Point.Empty;
            }
        }

        private void DrawingShapes_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:

                case Keys.Up:

                case Keys.Down:

                case Keys.Right:

                   e.IsInputKey = true;

                    break;
            }
        }

        private void DisplayInfo()
        {
            string info = "";

            info += "Drawing mode: ";
            if (mode == 0) info += "freely; ";
            else if (mode == 1) info += "elipse; ";
            else if (mode == 2) info += "rectangle; ";
            else if (mode == 3) info += "line; ";

            info += "Dimension: ";
            if (!dimensionSetting) info += "randomly; ";
            else info += dimension + "; ";

            info += "Color: ";
            if (!colorSetting) info += "randomly; ";
            else info += "R=" + drawingColor.R + ", G=" + drawingColor.G + ", B=" + drawingColor.B + "; ";

            info += "Motion: ";
            if (motionSetting) info += "active";
            else info += "not active";


            toolStripStatusLabel.Text = info;
        }
    }
}
