using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Assignment_63
{
    public partial class MainForm : Form
    {
        private bool selectedText = false;

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

        private void toolStripFindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedText) selectedText = false;

            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (!string.IsNullOrEmpty(toolStripFindTextBox.Text))
                {
                    richTextBox.SelectAll();
                    richTextBox.SelectionBackColor = Color.White;

                    int index = 0;
                    int res = 0;
                    while (index <= richTextBox.Text.LastIndexOf(toolStripFindTextBox.Text))
                    {
                        int location =richTextBox.Find(toolStripFindTextBox.Text, index, richTextBox.TextLength, RichTextBoxFinds.None);
                        if (location < 0) continue;
                        richTextBox.SelectionBackColor = Color.Yellow;
                        index = richTextBox.Text.IndexOf(toolStripFindTextBox.Text, index) + 1;

                        res++;
                        selectedText = true;
                    }

                    if (res == 0)
                    {
                        MessageBox.Show("Text not founded!", "Find result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Text Founded : " + res + " times.", "Find result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Text is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripReplaceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (!string.IsNullOrEmpty(toolStripReplaceTextBox.Text))
                {
                    if (!selectedText)
                    {
                        MessageBox.Show("First you must find text to replace.\nUse FIND function.", "Replace", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    int index = 0;
                    while (index <= richTextBox.Text.LastIndexOf(toolStripFindTextBox.Text))
                    {
                        int location = richTextBox.Find(toolStripFindTextBox.Text, index, richTextBox.TextLength, RichTextBoxFinds.None);
                        if (location < 0) location = 0;
                        
                        richTextBox.Select(location, toolStripFindTextBox.Text.Length);
                        richTextBox.SelectedText = toolStripReplaceTextBox.Text;
                        index = location + toolStripReplaceTextBox.Text.Length + 1;
                    }

                    toolStripFindTextBox.Text = toolStripReplaceTextBox.Text;
                }
            }
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedText)
            {
                int start = richTextBox.SelectionStart;
                richTextBox.SelectAll();
                richTextBox.SelectionBackColor = Color.White;
                richTextBox.DeselectAll();
                richTextBox.Select(start, 0);

                toolStripFindTextBox.Text = "";
                toolStripReplaceTextBox.Text = "";

                selectedText = false;
            }
            else selectedText = false;
        }
    }
}
