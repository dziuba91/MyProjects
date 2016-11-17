using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment5_2
{
    class Flight
    {
        private string flightID;
        private string origin;
        private string destination;
        private int day, month, year;
        private int hour, min;

        public Flight(string flightID, string origin, string destination, int day, int month, int year, int hour, int min)
        {
            this.flightID = flightID;
            this.origin = origin;
            this.destination = destination;
            
            this.day = day;
            this.month = month; 
            this.year = year;

            this.hour = hour;
            this.min = min;
        }

        public override string ToString()
        {
            return "Flight ID : " + this.flightID + "\n" +
                "Origin : " + this.origin + "\n" + 
                "Destination : " + this.destination + "\n" +
                "Date : " + this.day + "/" + this.month + "/" + this.year + "\n" + 
                "Time : " + this.hour + ":" + this.min;
        }
    }
}
