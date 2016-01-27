using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Exam2
{
    public partial class MainForm : Form
    {
        Thread tr = null;
        int mode = 0;

        bool inc = true;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (mode == 1 || mode == 0)
                e.Graphics.FillEllipse(Brushes.Green, new Rectangle(100, 30, 70, 70));
            else
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(100, 30, 70, 70));

            if (mode == 2 || mode == 0)
                e.Graphics.FillEllipse(Brushes.Yellow, new Rectangle(100, 120, 70, 70));
            else
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(100, 120, 70, 70));

            if (mode == 3 || mode == 0)
                e.Graphics.FillEllipse(Brushes.Red, new Rectangle(100, 210, 70, 70));
            else
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(100, 210, 70, 70));
        }

        private void thread()
        {
            for (; ; )
            {
                Refresh();

                showStatus();

                if (inc) mode++;
                else mode--;

                if (inc && mode == 4)
                {
                    inc = false;

                    mode = 2;
                }

                if (!inc && mode == 0)
                {
                    inc = true;

                    mode = 2;
                }

                Thread.Sleep(5000);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (tr != null)
                if (tr.IsAlive)
                    tr.Abort();

            Application.Exit();
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (startStopButton.Text.Equals("Start"))
            {
                mode = 1;

                tr = new Thread(thread);
                tr.Start();

                startStopButton.Text = "Stop";
            }
            else
            {
                mode = 0;

                Refresh();

                tr.Abort();

                startStopButton.Text = "Start";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tr != null)
                if (tr.IsAlive)
                    tr.Abort();
        }

        private void showStatus()
        {
            if (mode == 0)
            {
                statusLabel.Text = "Click START!";
            }
            else if (mode == 1)
            {
                statusLabel.Text = "GREEN light is ON!";
            }
            else if (mode == 2)
            {
                statusLabel.Text = "YELLOW light is ON!";
            }
            else if (mode == 3)
            {
                statusLabel.Text = "RED light is ON!";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            showStatus();
        }
    }
}
