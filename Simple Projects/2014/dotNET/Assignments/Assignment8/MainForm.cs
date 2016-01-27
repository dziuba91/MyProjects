using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Assignment8
{
    public partial class MainForm : Form
    {
        ArrayList lst;

        public MainForm()
        {
            InitializeComponent();
        }

        private void showNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showNameCheckBox.Checked)
            {
                if (showIdCheckBox.Checked || showPriceCheckBox.Checked)
                {
                    showIdCheckBox.Checked = false;
                    showPriceCheckBox.Checked = false;
                }

                Article.idCompare = false;
                Article.priceCompare = false;

                listBox2.DataSource = null;

                listBox2.DisplayMember = "Title";

                listBox2.ValueMember = "Title";

                listBox2.Items.Clear();

                SortList();

                listBox2.DataSource = lst;
            }
            else
            {
                listBox2.DataSource = null;

                listBox2.Items.Clear();
            }
        }

        private void showIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showIdCheckBox.Checked)
            {
                if (showNameCheckBox.Checked || showPriceCheckBox.Checked)
                {
                    showNameCheckBox.Checked = false;
                    showPriceCheckBox.Checked = false;
                }

                Article.idCompare = true;
                Article.priceCompare = false;

                listBox2.DataSource = null;

                listBox2.DisplayMember = "ID";

                listBox2.ValueMember = "ID";

                listBox2.Items.Clear();

                SortList();

                listBox2.DataSource = lst;
            }
            else
            {
                listBox2.DataSource = null;

                listBox2.Items.Clear();
            }
        }

        private void showPriceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPriceCheckBox.Checked)
            {
                if (showNameCheckBox.Checked || showIdCheckBox.Checked)
                {
                    showNameCheckBox.Checked = false;
                    showIdCheckBox.Checked = false;
                }

                Article.idCompare = false;
                Article.priceCompare = true;

                listBox2.DataSource = null;

                listBox2.DisplayMember = "Price";

                listBox2.ValueMember = "Price";

                listBox2.Items.Clear();

                SortList();

                listBox2.DataSource = lst;
            }
            else
            {
                listBox2.DataSource = null;

                listBox2.Items.Clear();
            }
        }

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            string value1 = ((Article)e.ListItem).ID.ToString();
            string value2 = ((Article)e.ListItem).Title.ToString();
            string value3 = ((Article)e.ListItem).Price.ToString();

            e.Value = "ID: " + value1 + "; TITLE: " + value2 + "; PRICE: " + value3;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = null;

            lst = new ArrayList();

            lst.Add(new Article("On GAs", 34567, 2.34));

            lst.Add(new Article("Evolutionary Computation", 80567, 15.89));

            lst.Add(new Article("Fuzzy Logic", 91567, 23.56));

            //listBox1.DisplayMember = "Title";

            listBox1.ValueMember = "ID";

            listBox1.Items.Clear();

            listBox1.DataSource = lst;


            //
            ascendingSortRadioButton.Checked = true;
        }

        private void SortList()
        {
            if (ascendingSortRadioButton.Checked)
                lst.Sort();

            else if (descendingSortRadioButton.Checked)
            {
                lst.Sort();
                lst.Reverse();
            }
        }
    }
}
