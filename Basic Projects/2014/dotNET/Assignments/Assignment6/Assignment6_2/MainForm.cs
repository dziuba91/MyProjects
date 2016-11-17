using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Assignment_62
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

            richTextBox.Text += ((ToolStripComboBox)sender).SelectedItem.ToString() + Environment.NewLine;
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ToolStripTextBox tstbx = (ToolStripTextBox)sender;

            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (!string.IsNullOrEmpty(tstbx.Text))
                    richTextBox.Text += tstbx.Text + Environment.NewLine;
                else
                    MessageBox.Show("Text is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeTextBox()
        {
            richTextBox.Size = new Size(Size.Width - 16, Size.Height - 8);

            richTextBox.Location = new Point(0, 40);
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
                sw.WriteLine(richTextBox.Text);
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
            if (richTextBox.Text != "")
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

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox.SelectedText);
                richTextBox.SelectedText = "";
            }
            catch
            {
                MessageBox.Show("No Text selected...");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox.SelectedText);
            }
            catch
            {
                MessageBox.Show("No Text selected...");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectedText = Clipboard.GetText();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^(z)");

            richTextBox.Undo();
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();

            richTextBox.SelectionColor = colorDialog.Color;
        }

        private void textFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.ShowDialog();

            richTextBox.SelectionFont = fontDialog.Font; 
        }
    }
}
