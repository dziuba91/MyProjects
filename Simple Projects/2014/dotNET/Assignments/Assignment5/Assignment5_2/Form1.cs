using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Assignment5_2
{
    public partial class Form1 : Form
    {
        ArrayList flights;

        public Form1()
        {
            InitializeComponent();

            flights = new ArrayList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string flightID;
            string origin;
            string destination;
            int day, month, year;
            int hour, min;
            
            try
            {
                if (this.flightID.Text == "")
                {
                    MessageBox.Show("Flight ID is not set correctly!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                flightID = this.flightID.Text;
            }
            catch
            {
                MessageBox.Show("Flight ID setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                origin = this.origin.Text;
            }
            catch
            {
                MessageBox.Show("Origin setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                destination = this.destination.Text;
            }
            catch
            {
                MessageBox.Show("Destination setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                day = Convert.ToInt32(this.day.Text);
                month = Convert.ToInt32(this.month.Text);
                year = Convert.ToInt32(this.year.Text);
            }
            catch
            {
                MessageBox.Show("Date setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                hour = Convert.ToInt32(this.hour.Text);
                min = Convert.ToInt32(this.min.Text);
            }
            catch
            {
                MessageBox.Show("Time setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            flights.Add(new Flight(flightID, origin, destination, day, month, year, hour, min));

            if (flights.Count > 1)
                MessageBox.Show("RECENT ADDED DATA : \n" + ((Flight)flights[flights.Count - 1]).ToString()
                    + "\n\nPREVIOUS ADDED DATA : \n" + ((Flight)flights[flights.Count - 2]).ToString(), "Added Data Complete", MessageBoxButtons.OK);
            else
                MessageBox.Show("RECENT ADDED DATA : \n" + ((Flight)flights[flights.Count - 1]).ToString()
                    + "\n\nPREVIOUS ADDED DATA : \n(none)\n", "Added Data Complete", MessageBoxButtons.OK);


            this.flightID.Text = "";
            this.origin.Text = "";
            this.destination.Text = "";
            
            this.day.Text = "dd";
            this.month.Text = "mm";
            this.year.Text = "yyyy";
            
            this.hour.Text = "hh";
            this.min.Text = "mm";
        }
    }
}
