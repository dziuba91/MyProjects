using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam1
{
    class Timetable
    {
        private string day;
        private string time;
        private string place;

        public string Day
        {
            get
            {
                return this.day;
            }
        }

        public string Time
        {
            get
            {
                return this.time;
            }
        }

        public string Place
        {
            get
            {
                return this.place;
            }
        }


        public Timetable(string day, string time, string place)
        {
            this.day = day;
            this.time = time;
            this.place = place;
        }
    }
}
