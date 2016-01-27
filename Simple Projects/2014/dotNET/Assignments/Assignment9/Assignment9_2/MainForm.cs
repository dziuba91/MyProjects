using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Assignment9_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox2.SelectionMode = SelectionMode.MultiExtended;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ArrayList items = new ArrayList();

            items.Add("item1");
            items.Add("item2");
            items.Add("item3");
            items.Add("item4");
            items.Add("item5");

            listBox1.DataSource = null;

            listBox1.Items.Clear();

            listBox1.DataSource = items;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            foreach (object o in listBox1.SelectedItems)
                listBox2.Items.Add(o);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            while (listBox2.SelectedIndex >= 0)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            listBox1.SelectedItems.Clear();
            listBox2.SelectedItems.Clear();

            // list1
            int index = -1;
            int previousIndex = -1;
            int count1 = 0;
            bool textFound1 = false;
            string textFoundIndex1 = "";
            while ((index = listBox1.FindStringExact(searchItemNameTextBox.Text, index)) > previousIndex)
            {
                if (!textFound1)
                {
                    textFound1 = true;

                    textFoundIndex1 = index.ToString();
                }
                else
                    textFoundIndex1 += ", " + index;

                listBox1.SetSelected(index, true);

                previousIndex = index;
                count1++;
            }

            //searchResultLabel.Text = textFoundIndex1;


            // list2
            index = -1;
            previousIndex = -1;
            int count2 = 0;
            bool textFound2 = false;
            string textFoundIndex2 = "";
            while ((index = listBox2.FindStringExact(searchItemNameTextBox.Text, index)) > previousIndex)
            {
                if (!textFound2)
                {
                    textFound2 = true;

                    textFoundIndex2 = index.ToString();
                }
                else
                    textFoundIndex2 += ", " + index;

                listBox2.SetSelected(index, true);

                previousIndex = index;
                count2++;
            }


            // display search result
            if (textFound1)
                searchResultLabel.Text = "Text: '" + searchItemNameTextBox.Text + "' found in 'Text Box 1' = " + count1 +
                    " times on index: " + textFoundIndex1;
            else
                searchResultLabel.Text = "Text: '" + searchItemNameTextBox.Text + "' not found in 'Text Box 1'";

            searchResultLabel.Text += "\n";

            if (textFound2)
                searchResultLabel.Text += "Text: '" + searchItemNameTextBox.Text + "' found in 'Text Box 2' = " + count2 +
                    " times on index: " + textFoundIndex2;
            else
                searchResultLabel.Text += "Text: '" + searchItemNameTextBox.Text + "' not found in 'Text Box 2'";
        }
    }
}
