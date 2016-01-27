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
    public partial class Form1 : Form
    {
        private int formID = 0;
        private int buttonID = 0;
        private int textBoxID = 0;
        private FlowLayoutPanel flowPanel;

        public Form1()
        {
            InitializeComponent();

            this.flowPanel = new FlowLayoutPanel();
            this.flowPanel.FlowDirection = FlowDirection.TopDown;
            this.flowPanel.Dock = DockStyle.Fill;

            this.Controls.Add(flowPanel);
        }

        private void myButton_Click(object sender, EventArgs e)
        {
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;
            
            Random r = new Random();

            this.Location = new Point(r.Next(width - this.Bounds.Width), r.Next(height - this.Bounds.Height));

            myTextBox.Text = "X=" + this.Location.X + "; Y=" + this.Location.Y;

            this.BackColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

        private void duplicateFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            this.formID++;
            f1.formID = this.formID;

            f1.Text = "copy_" + f1.formID;
            f1.Name = f1.formID + "";

            f1.Show();
        }

        private void changeSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            this.myButton.Size = new Size(r.Next(20, this.Bounds.Width - this.myButton.Location.X - this.myButton.Bounds.Width),
                r.Next(20, this.Bounds.Height - this.myButton.Location.Y - this.myButton.Bounds.Height));
        }

        private void changeBackgroundCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            this.myButton.BackColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

        private void setSizeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setSizeColorForm f1 = new setSizeColorForm(this.myButton.Size.Width, this.myButton.Size.Height, 
                    this.myButton.BackColor.R, this.myButton.BackColor.G, this.myButton.BackColor.B);

            f1.ShowDialog();

            if (f1.OK)
            {
                this.myButton.Size = new Size(f1.height, f1.width);

                this.myButton.BackColor = Color.FromArgb(f1.R, f1.G, f1.B);
            }
        }

        private void buttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonID++;

            Button button = new Button();
            button.ContextMenuStrip = this.contextMenuStrip2;
            button.Name = "myButton" + buttonID;
            button.Size = new System.Drawing.Size(75, 23);
            //button.Location = new Point(0, 0 + (buttonID-1) * 23);
            button.Text = "BUTTON" + buttonID;
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(myClickHandler);

            button.ContextMenuStrip = this.contextMenuStrip1;

            this.flowPanel.Controls.Add(button);
        }

        private void myClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("You Click the Button! \nButton Name: " + ((Button)sender).Name, "Click Button Action!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxID++;

            TextBox textBox = new TextBox();
            //textBox.Location = new System.Drawing.Point(90, 33);
            textBox.Name = "myTextBox" + textBoxID;
            textBox.Size = new System.Drawing.Size(75, 20);
            textBox.Text = "TEXT BOX " + textBoxID;

            this.flowPanel.Controls.Add(textBox);
        }
    }
}
