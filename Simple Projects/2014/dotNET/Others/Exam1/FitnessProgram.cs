using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam1
{
    class FitnessProgram
    {
        private string name;
        private string duration;
        private Timetable [] timetable;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Duration
        {
            get
            {
                return this.duration;
            }
        }

        public Timetable [] Timetable
        {
            get
            {
                return this.timetable;
            }
        }

        public FitnessProgram(string name, string duration, Timetable[] timetable)
        {
            this.name = name;
            this.duration = duration;
            this.timetable = timetable;
        }
    }
}
