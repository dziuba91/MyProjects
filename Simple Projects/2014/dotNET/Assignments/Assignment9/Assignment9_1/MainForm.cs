using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Assignment9_1
{
    public partial class MainForm : Form
    {
        CultureInfo ci;
        string language;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();

            listBox.Items.Clear();

            listBox.DataSource = imageList.Images;

            setLanguage();

            timer_Tick(sender, e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            this.Text = date.ToString("D", ci) + " " + date.ToString("T", ci) + " [" + ci.NativeName + "]";
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox.Image = (Image)listBox.Items[listBox.SelectedIndex];
            //pictureBox.Image = imageList.Images[listBox.SelectedIndex];

            setLanguage();

            timer_Tick(sender, e);
        }

        private void listBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string value1 = imageList.Images.Keys[((ListBox)sender).Items.IndexOf(e.ListItem)].ToString();

            e.Value = value1;
        }

        private void setLanguage()
        {
            string language_now = imageList.Images.Keys[listBox.SelectedIndex].ToString();
            if (language_now.Equals("Polish"))
                ci = new CultureInfo("pl-PL");
            else if (language_now.Equals("French"))
                ci = new CultureInfo("fr-FR");
            else
                ci = new CultureInfo("en-GB");
        }
    }
}
