using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam1
{
    class SportsClub
    {
        private string name;
        private string address;
        private FitnessProgram [] programs;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
        }

        public FitnessProgram [] Programs
        {
            get
            {
                return this.programs;
            }
        }

        // get info
        public string SportsClubInfo
        {
            get
            {
                string info = "";

                info = "Name: " + name + "\n" + 
                    "Address: " + address + "\n" + 
                    "Fitness Program: \n";

                for (int i = 0; i < programs.Length; i++)
                {
                    info += "Program Name: " + programs[i].Name + "\n" +
                        "Program Duration: " + programs[i].Duration + "\n" +
                        "Program Timetable: \n";

                    for (int j = 0; j < programs[i].Timetable.Length; j++)
                    {
                        info += "Day: " + programs[i].Timetable[j].Day + "\n" +
                            "Time: " + programs[i].Timetable[j].Time + "\n" +
                            "Place: " + programs[i].Timetable[j].Place + "\n";
                    }
                }

                return info;
            }
        }

        
        public SportsClub(string name, string address, FitnessProgram [] programs)
        {
            this.name = name;
            this.address = address;
            this.programs = programs;
        }

        public string getFitnessProgram (string name)
        {
            for (int i = 0; i < programs.Length; i++)
            {
                if (programs[i].Name.Equals(name))
                {
                    string timetableInfo = "";

                    timetableInfo += programs[i].Name + "\n";
                    timetableInfo += programs[i].Duration + "\n";

                    for (int j = 0; j < programs[i].Timetable.Length; j++)
                    {
                        timetableInfo += programs[i].Timetable[j].Day + "\n";
                        timetableInfo += programs[i].Timetable[j].Time + "\n";
                        timetableInfo += programs[i].Timetable[j].Place + "\n";
                    }
                    
                    return timetableInfo;    
                }
            }

            return "Fitness Program = " + name + " not found!";
        }
    }
}
