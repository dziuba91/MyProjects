using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace Assignment9_3
{
    public partial class MainForm : Form
    {
        DateTime time;

        Thread tr = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        { }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            textBox.Text = dateTimePicker.Value.ToLocalTime().AddHours(-2).ToString();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                bool valid = DateTime.TryParseExact(textBox.Text,
                                                    "yyyy-MM-dd HH:mm:ss",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None,
                                                    out time);

                if (!valid)
                {
                    MessageBox.Show("Date have wrong format!");

                    return;
                }

                textBox.Visible = false;

                this.WindowState = FormWindowState.Minimized;


                tr = new Thread(thread);
                tr.Start();
            }
        }

        private void thread()
        {
            for (; ; )
            {
                DateTime now = DateTime.Now;

                if ((now.Day == time.Day) && (now.Month == time.Month) && (now.Year == time.Year)
                    && (now.Second == time.Second) && (now.Minute == time.Minute) && (now.Hour == time.Hour))
                {
                    textBox.Visible = true;

                    this.WindowState = FormWindowState.Normal;

                    System.Media.SoundPlayer reminderSounbd = null;

                    try
                    {
                        reminderSounbd = new System.Media.SoundPlayer(@"folder_path\alarm.wav");
                        reminderSounbd.Play();
                    }
                    catch
                    {
                        MessageBox.Show("Path of sound not exist!");
                    }

                    MessageBox.Show("Now it's:\n" + textBox.Text, "Alarm!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        reminderSounbd.Stop();
                    }
                    catch { }

                    break;
                }

                Thread.Sleep(500);
            }

            tr.Abort();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tr != null)
                if (tr.IsAlive)
                    tr.Abort();
        }
    }
}
