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
    public partial class HandlingText : Form
    {
        string filePathName;

        public HandlingText()
        {
            InitializeComponent();
        }

        private bool LoadFile()
        {
            try
            {
                StreamReader sr = File.OpenText(filePathName);
                mainRichTextBox.Text = sr.ReadToEnd();
                sr.Close();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Cannot open the file: " + filePathName + "\nError notification: \n" + e.ToString(), "File Not Found Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("File: " + filePathName + "\nError: " + e.ToString(), "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void SaveFile()
        {
            StreamWriter sw = File.CreateText(filePathName);
            sw.WriteLine(mainRichTextBox.Text);
            sw.Close();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create a new document...");

            mainRichTextBox.ResetText();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "txt";

            //openFileDialog.InitialDirectory = toolStripFilepathTextBox.Text;

            //Here we make it possible for the user to select .txt or .xml

            //or all files.
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                filePathName = openFileDialog.FileName;
                LoadFile();
            }

            //UpdateRecentFileList();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox .Show( "Saving the file..." );

            saveFileDialog.InitialDirectory = filePathName;

            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                filePathName = saveFileDialog.FileName;
                //toolStripFilepathTextBox.Text = filePathName;

                SaveFile();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^(z)");

            mainRichTextBox.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(mainRichTextBox.SelectedText);
                mainRichTextBox.SelectedText = "";
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
                Clipboard.SetText(mainRichTextBox.SelectedText);
            }
            catch
            {
                MessageBox.Show("No Text selected...");
            }
        }

        private void paseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainRichTextBox.SelectedText = Clipboard.GetText();
        }


        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();

            mainRichTextBox.SelectionColor = colorDialog.Color;
        }

        private void textFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.ShowDialog();

            mainRichTextBox.SelectionFont = fontDialog.Font; 
        }
    }
}
