using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Example_51_
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox1.Text += toolStripComboBox1.SelectedItem.ToString() + Environment.NewLine;

            textBox1.Text += ((ToolStripComboBox)sender).SelectedItem.ToString() + Environment.NewLine;
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ToolStripTextBox tstbx = (ToolStripTextBox)sender;

            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (!string.IsNullOrEmpty(tstbx.Text))
                    textBox1.Text += tstbx.Text + Environment.NewLine;
                else
                    MessageBox.Show("Text is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeTextBox()
        {
            textBox1.Size = new Size(Size.Width - 16, Size.Height - 8);

            textBox1.Location = new Point(0, 40);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeTextBox(); 
        }

        private bool SaveFile(string filePathName)
        {
            try
            {
                StreamWriter sw = File.CreateText(filePathName);
                sw.WriteLine(textBox1.Text);
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot save the file: " + filePathName + "\nError notification: \n" + e.ToString(), "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult dr = MessageBox.Show("In the main window is still content loaded. \nDo you want to save the content before exit?", "Content is still loaded!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    //saveFileDialog.InitialDirectory = filePathName;

                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All files (*.*)|*.*";

                    if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        string filePathName = saveFileDialog.FileName;

                        if (SaveFile(filePathName))
                        {
                            MessageBox.Show("You saved new file: " + filePathName + "\nFile saving process complete!", "File Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}
