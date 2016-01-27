using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam1
{
    class Program
    {
        static void Main(string[] args)
        {
            // initilaize data
            FitnessProgram []programs = new FitnessProgram[2];

            Timetable []timetable1 = new Timetable[2];
            timetable1[0] = new Timetable("Wednesday", "13-15", "R0001");
            timetable1[1] = new Timetable("Friday", "16-18", "R0011");

            programs[0] = new FitnessProgram("sport1", "2 hours", timetable1);


            Timetable[] timetable2 = new Timetable[3];
            timetable2[0] = new Timetable("Monday", "18-21", "R0014");
            timetable2[1] = new Timetable("Friday", "19-22", "R0002");
            timetable2[2] = new Timetable("Sunday", "16-18", "R0001");

            programs[1] = new FitnessProgram("sport2", "3 hours", timetable2);

            SportsClub club = new SportsClub("Fitness Club", "Mikołajczyka 32, Opole 45-543, Poland", programs);


            // display
            Console.WriteLine(club.SportsClubInfo);

            // found
            Console.WriteLine();

            string findProgram = "sport1";
            Console.WriteLine("Looking for program = " + findProgram + "...");
            Console.WriteLine(club.getFitnessProgram(findProgram));
        }
    }
}
